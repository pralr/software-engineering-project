using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VendaDeLanches.Context;
using VendaDeLanches.Models;
using VendaDeLanches.Repositories;
using VendaDeLanches.Repositories.Interfaces;
using VendaDeLanches.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<ILancheRepository, LancheRepository>();
builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();
builder.Services.AddScoped(sp => CarrinhoCompra.GetCarrinho(sp));
// addscoped - se 2 clientes solicitarem o obj carrinho ao mesmo tempo vao obter instancias diferentes, pq sao instancias diferentes
// entao usam requests diferentes

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin",
        politica =>
        {
            politica.RequireRole("Admin");
        });
});

builder.Services.AddMemoryCache();
builder.Services.AddSession();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

// chama o metodo depois do roteamento
CriarPerfisUsuarios(app);


app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "categoriaFiltro",
    pattern: "Lanche/{action}/{categoria?}",
    defaults: new { Controller = "Lanche", action = "List" });

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
    );
});

app.Run();


// acima registrei o serviço, mas como vou obter uma instancia desse serviço se nao tenho um metodo pra injetar? 
// usar a classe serviceProvider! Oferece um IServiceProvider padrão, que é responsável por resolver instancias
// de tipo em tempo de execução. 
// Tem um metodo de extensao chamado de GetService que vai permitir que obtenha uma instancia de um serviço
// que ta registrado em um container

/*seedUserRoleInitial.SeedRoles();
seedUserRoleInitial.SeedUsers();*/

void CriarPerfisUsuarios(WebApplication app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<ISeedUserRoleInitial>();
        service.SeedRoles();
        service.SeedUsers();
    }
}

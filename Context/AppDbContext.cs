using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VendaDeLanches.Models;

namespace VendaDeLanches.Context
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<CategoriaModel> Categorias { get; set; }
        public DbSet<LancheModel> Lanches { get; set; }
        public DbSet<CarrinhoCompraItemModel> CarrinhoCompraItens { get; set; }

        public DbSet<PedidoModel> Pedidos { get; set; }
        public DbSet<PedidoDetalheModel> PedidoDetalhes { get; set; }
    }
}

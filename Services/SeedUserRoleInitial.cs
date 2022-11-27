using Microsoft.AspNetCore.Identity;

namespace VendaDeLanches.Services
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedUserRoleInitial(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // metodo para criar os perfis
        public void SeedRoles()
        {
            // perfil que quero criar já existe?
            // como o metodo eh async usa result
            if(!_roleManager.RoleExistsAsync("Member").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Member";
                role.NormalizedName = "MEMBER";
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
            }

            if (!_roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                role.NormalizedName = "ADMIN";
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
            }
        }

        // criar os usuarios e atribuir os usuarios aos perfis criados 
        public void SeedUsers()
        {
            // tenta localizar o usuario, se nao encontrar, retorna null e pode usar
            if(_userManager.FindByEmailAsync("usuario@localhost").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "usuario@localhost";
                user.Email = "usuario@localhost";
                user.NormalizedEmail = "USUARIO@LOCALHOST";
                user.NormalizedUserName = "USUARIO@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();


                IdentityResult result = _userManager.CreateAsync(user, "Numsey#2022").Result;

                if(result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Member").Wait();
                }
            }


            // tenta localizar o usuario, se nao encontrar, retorna null e pode usar
            if (_userManager.FindByEmailAsync("admin@localhost").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "admin@localhost";
                user.Email = "admin@localhost";
                user.NormalizedEmail = "ADMIN@LOCALHOST";
                user.NormalizedUserName = "ADMIN@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();


                IdentityResult result = _userManager.CreateAsync(user, "Numsey#2022").Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}

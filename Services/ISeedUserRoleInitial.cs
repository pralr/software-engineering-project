namespace VendaDeLanches.Services
{
    public interface ISeedUserRoleInitial
    {
        // implementa a criacao de 2 perfil
        void SeedRoles();
        // cria os usuarios e atribui a um dos perfils
        void SeedUsers();
    }
}

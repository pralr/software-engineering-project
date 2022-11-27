using VendaDeLanches.Models;

namespace VendaDeLanches.Repositories.Interfaces
{
    public interface ICategoriaRepository
    {
        IEnumerable<CategoriaModel> Categorias { get; }
    }
}

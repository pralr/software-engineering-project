using VendaDeLanches.Models;

namespace VendaDeLanches.ViewModels
{
    public class LancheListViewModel
    {
        public IEnumerable<LancheModel> Lanches { get; set; }
        public string CategoriaAtual { get; set; }
    }
}

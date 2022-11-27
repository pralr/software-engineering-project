using VendaDeLanches.Models;

namespace VendaDeLanches.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<LancheModel> LanchesPreferidos { get; set; }
    }
}

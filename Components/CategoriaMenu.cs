using Microsoft.AspNetCore.Mvc;
using VendaDeLanches.Repositories.Interfaces;

namespace VendaDeLanches.Components
{
    public class CategoriaMenu : ViewComponent
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaMenu(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public IViewComponentResult Invoke()
        {
            var categorias = _categoriaRepository.Categorias.OrderBy(categoria => categoria.Nome);
            return View(categorias);
        }
    }
}

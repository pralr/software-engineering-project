using Microsoft.AspNetCore.Mvc;
using VendaDeLanches.Models;
using VendaDeLanches.Repositories.Interfaces;
using VendaDeLanches.ViewModels;

namespace VendaDeLanches.Controllers
{
    public class LancheController : Controller
    {
        // tenho que injetar uma instancia do repositorio
        // da para fazer isso porque ja referenciei no program.cs como um serviço
        // logo, da pra usar o container de injecao de dependencia

        private readonly ILancheRepository _lancheRepository;

        public LancheController(ILancheRepository lancheRepository)
        {
            _lancheRepository = lancheRepository;
        }

      /* public IActionResult List()
        {
              ViewData["Titulo"] = "Todos os Lanches";
              ViewData["Data"] = DateTime.Now;

              var lanches = _lancheRepository.Lanches;
              var totalLanches = lanches.Count();

              ViewBag.Total = "Total de Lanches : ";
              ViewBag.TotalLanches = totalLanches; 

              return View(lanches);

            var lanchesListViewModel = new LancheListViewModel();
            lanchesListViewModel.Lanches = _lancheRepository.Lanches;
            lanchesListViewModel.CategoriaAtual = "Ct. Atual";

            return View(lanchesListViewModel);

        }*/

        public IActionResult List(string categoria)
        {
            IEnumerable<LancheModel> lanches;

            string categoriaAtual = string.Empty;

            if(string.IsNullOrEmpty(categoria))
            {
                // nao foi passada nenhuma categoria
                // passa todos os lanches
                lanches = _lancheRepository.Lanches.OrderBy(l => l.Id);
                categoriaAtual = "Todos os lanches";
;            } else
            {
              
                   lanches = _lancheRepository.Lanches.Where(lanche => lanche.Categoria.Nome.Equals(categoria))
                    .OrderBy(categoria => categoria.Nome);

                   categoriaAtual = categoria;
            }

            var lanchesListViewModel = new LancheListViewModel
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual

            };

            return View(lanchesListViewModel);


            
        }



        public IActionResult Details(int Id)
        {
            var lanche = _lancheRepository.Lanches.FirstOrDefault(lanche => lanche.Id == Id);
            return View(lanche);
        }

        // se colocar o parametro diferente de searchString,  quando você postar os dados no formulario vai
        // vir como null, tem que ser exatamente como estar no "name" la
        public ViewResult Search(string searchString)
        {
            IEnumerable<LancheModel> lanches;
            string categoriaAtual = string.Empty;

            if(string.IsNullOrEmpty(searchString))
            {
                lanches = _lancheRepository.Lanches.OrderBy(p => p.Id);
                categoriaAtual = "Todos os Lanches";
            } else {
                lanches = _lancheRepository.Lanches
                    .Where(p => p.Nome.ToLower().Contains(searchString.ToLower()));

                if(lanches.Any())
                {
                    categoriaAtual = "Lanches";
                } else
                {
                    categoriaAtual = "Nenhum lanche foi encontrado";
                }

            }

            return View("~/Views/Lanche/List.cshtml", new LancheListViewModel
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual
            });

        }
    }
}

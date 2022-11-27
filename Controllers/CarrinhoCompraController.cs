using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VendaDeLanches.Models;
using VendaDeLanches.Repositories.Interfaces;
using VendaDeLanches.ViewModels;

namespace VendaDeLanches.Controllers
{
    public class CarrinhoCompraController : Controller
    {

        private readonly ILancheRepository _lancheRepository;
        private readonly CarrinhoCompra _carrinhoCompra;


        public CarrinhoCompraController(ILancheRepository lancheRepository, CarrinhoCompra carrinhoCompra)
        {
            _lancheRepository = lancheRepository;
            _carrinhoCompra = carrinhoCompra;
        }

        public IActionResult Index()
        {
            var itens = _carrinhoCompra.GetCarrinhoCompraItens();
            _carrinhoCompra.CarrinhoCompraItems = itens;

            var carrinhoCompraVM = new CarrinhoCompraViewModel
            {
                CarrinhoCompra = _carrinhoCompra,
                CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal()

            };
            return View(carrinhoCompraVM);
        }

        [Authorize]
        public IActionResult AdicionarItemNoCarrinhoCompra(int Id)
        {
            var lancheSelecionado = _lancheRepository.Lanches.FirstOrDefault(p => p.Id == Id);

            if(lancheSelecionado != null)
            {
                _carrinhoCompra.AdicionarAoCarrinho(lancheSelecionado);
            }

            return RedirectToAction("Index");
        }
        [Authorize]
        public IActionResult RemoverItemDoCarrinhoCompra(int Id)
        {
            var lancheSelecionado = _lancheRepository.Lanches.FirstOrDefault(p => p.Id == Id);

            if (lancheSelecionado != null)
            {
                _carrinhoCompra.RemoverDoCarrinho(lancheSelecionado);
            }

            return RedirectToAction("Index");
        }


    }

}
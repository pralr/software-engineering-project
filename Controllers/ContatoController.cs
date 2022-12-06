using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VendaDeLanches.Models;
using VendaDeLanches.Repositories.Interfaces;

namespace VendaDeLanches.Controllers
{
    public class ContatoController : Controller
    {
        private readonly IContatoRepository _contatoRepository;


        public ContatoController(IContatoRepository contatoRepository)
        {
            _contatoRepository = contatoRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(ContatoModel contato)
        {
            if(ModelState.IsValid)
            {
                _contatoRepository.EnviarMensagem(contato);
                ViewData["MensagemSucesso"] = "Mensagem enviada com sucesso! Em breve entraremos em contato.";
            }

            return View();
        }
    }
}

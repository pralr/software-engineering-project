using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VendaDeLanches.Models;
using VendaDeLanches.Repositories.Interfaces;

namespace VendaDeLanches.Controllers
{
    public class PedidoController : Controller
    {
        // definir a injecao das instancias de pedido repositorio e carrinho de compra no construtor
        // desse controlador 

        private readonly IPedidoRepository _pedidoRepository;
        private readonly CarrinhoCompra _carrinhoCompra;
        public PedidoController(IPedidoRepository pedidoRepository, CarrinhoCompra carrinhoCompra)
        {
            _pedidoRepository = pedidoRepository;
            _carrinhoCompra = carrinhoCompra;

        }

        // quando nao é colocado nada, é como se eu estivesse inferindo "HttpGet", já o post é preciso colocar
        [Authorize]
        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Checkout(PedidoModel pedido)
        {
            int totalItensPedido = 0;
            decimal precoTotalPedido = 0.0m;

            // obtem itens do carrinho de compra do cliente

            List<CarrinhoCompraItemModel> itens = _carrinhoCompra.GetCarrinhoCompraItens();

            _carrinhoCompra.CarrinhoCompraItems = itens;

            // existe itens de pedidos que foram retornado?

            if(_carrinhoCompra.CarrinhoCompraItems.Count == 0)
            {
                // n tenho itens no carrinho
                ModelState.AddModelError("", "Seu carrinho está vazio, que tal incluir um lanche?");
                // se nao houver item (inclui erro no model state)
                // embaixo, em modelstate.isvalid vai retornar false (estado invalido)
            }

            // calcula o total de itens e o total de pedido
            foreach(var item in itens)
            {
                totalItensPedido += item.Quantidade;
                precoTotalPedido += (item.Lanche.Preco * item.Quantidade); 
            }

            // atribui os valores obtidos ao pedido
            pedido.TotalItensPedido = totalItensPedido;
            pedido.PedidoTotal = precoTotalPedido;

            // validar os dados do pedido
            if(ModelState.IsValid)
            {
                // qualquer estado invalido vai me retornar false
                // so passa se todas as validacoes forem feitas e tudo que precisava informar for informado
                // como foi adicionado um model erro para caso o carrinho nao tenha itens
                // ja é outra coisa para validar

                // se for valido, cria o pedido e detalhes
                _pedidoRepository.CriarPedido(pedido);

            // define mensagens ao cliente
            // como to num controlador, toda vez que eu quiser passar dados entre controler e view ou vice-versa
            // uso ou view bag ou view data
                  ViewBag.CheckoutCompletoMensagem = "Obrigada por comprar conosco";
                  ViewBag.TotalPedido = _carrinhoCompra.GetCarrinhoCompraTotal();

                // limpa carrinho do cliente
                 _carrinhoCompra.LimparCarrinho();

                // exibe view com dados do cliente e do pedido
                return View("~/Views/Pedido/CheckoutCompleto.cshtml", pedido);
            }
            // se for invalido, retorna a view com os dados do pedido
            // e se tiver erro, mostra ao usuario
            return View(pedido);
        }


    }
}

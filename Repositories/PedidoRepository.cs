using VendaDeLanches.Models;
using VendaDeLanches.Context;
using VendaDeLanches.Repositories.Interfaces;

namespace VendaDeLanches.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoRepository(AppDbContext appDbContext, CarrinhoCompra carrinhoCompra)
        {
            _appDbContext = appDbContext;
            _carrinhoCompra = carrinhoCompra;
        }

        public void CriarPedido(PedidoModel pedido)
        {
            // persiste o pedido
            pedido.PedidoEnviado = DateTime.Now;
            _appDbContext.Pedidos.Add(pedido);
            _appDbContext.SaveChanges();
            // nesse momento, é criado o Id do pedido
            // agora vamos para os detalhes do pedido, que é criado a partir dos itens do carrinho

            // persiste detalhes do pedido
            var carrinhoCompraItens = _carrinhoCompra.CarrinhoCompraItems;

            foreach (var carrinhoItem in carrinhoCompraItens)
            {
                var pedidoDetail = new PedidoDetalheModel()
                {
                    Quantidade = carrinhoItem.Quantidade,
                    LancheId = carrinhoItem.Lanche.Id,
                    PedidoId = pedido.Id,
                    Preco = carrinhoItem.Lanche.Preco
                };

                _appDbContext.PedidoDetalhes.Add(pedidoDetail);
            }
            _appDbContext.SaveChanges();
        }
    }
}

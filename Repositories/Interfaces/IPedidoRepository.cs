using VendaDeLanches.Models;

namespace VendaDeLanches.Repositories.Interfaces
{
    public interface IPedidoRepository
    {
        void CriarPedido(PedidoModel pedido);

    }
}

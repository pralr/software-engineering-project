using VendaDeLanches.Models;

namespace VendaDeLanches.Repositories.Interfaces
{
    public interface IContatoRepository
    {
        void EnviarMensagem(ContatoModel contato);
    }
}

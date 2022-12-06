using VendaDeLanches.Context;
using VendaDeLanches.Models;
using VendaDeLanches.Repositories.Interfaces;

namespace VendaDeLanches.Repositories
{
    public class ContatoRepository : IContatoRepository
    {
        private readonly AppDbContext _context;


        public ContatoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void EnviarMensagem(ContatoModel contato)
        {
            contato.DataEnvioDaMensagem = DateTime.Now;
            _context.Mensagens.Add(contato);
            _context.SaveChanges();
        }

    }
}

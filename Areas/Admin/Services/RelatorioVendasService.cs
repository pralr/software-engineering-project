using Microsoft.EntityFrameworkCore;
using VendaDeLanches.Context;
using VendaDeLanches.Models;

namespace VendaDeLanches.Areas.Admin.Services
{
    public class RelatorioVendasService
    {
        private readonly AppDbContext _context;

        public RelatorioVendasService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<PedidoModel>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var resultado = from obj in _context.Pedidos select obj;

            // def dessa forma pois o resultado eh queryable
            // sempre que quero trabalhar c dados na memória e dps vou poder expandir a consulta
            // usa o queryble

            if(minDate.HasValue)
            {
                resultado = resultado.Where(x => x.PedidoEnviado >= minDate.Value);
            }

            if (maxDate.HasValue)
            {
                resultado = resultado.Where(x => x.PedidoEnviado <= maxDate.Value);
            }

            return await resultado
                .Include(l => l.PedidoItens)
                .ThenInclude(l => l.Lanche)
                .OrderByDescending(x => x.PedidoEnviado)
                .ToListAsync();

        }
    }
}

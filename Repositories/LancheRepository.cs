using Microsoft.EntityFrameworkCore;
using VendaDeLanches.Context;
using VendaDeLanches.Models;
using VendaDeLanches.Repositories.Interfaces;

namespace VendaDeLanches.Repositories
{
    public class LancheRepository : ILancheRepository
    {
        private readonly AppDbContext _context;
        public LancheRepository(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<LancheModel> Lanches => _context.Lanches.Include(c => c.Categoria);

        public IEnumerable<LancheModel> LanchesPreferidos => _context.Lanches.Where(l => l.IsLanchePreferido).Include(c => c.Categoria);

        public LancheModel GetLancheById(int lancheId)
        {
            return _context.Lanches.FirstOrDefault(l => l.Id == lancheId);
        }
    }
}

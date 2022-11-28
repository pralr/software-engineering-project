using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using VendaDeLanches.Context;
using VendaDeLanches.Models;

namespace VendaDeLanches.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminLancheController : Controller
    {
        private readonly AppDbContext _context;

        public AdminLancheController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminLanche
      /*  public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Lanches.Include(l => l.Categoria);
            return View(await appDbContext.ToListAsync());
        }*/

        public async Task<IActionResult> Index(string filter, int pageindex=1, string sort = "Nome")
        {
            // define consulta 

            var resultado = _context.Lanches.Include(l => l.Categoria).AsQueryable();

            if(!string.IsNullOrWhiteSpace(filter))
            {
                resultado = resultado.Where(p => p.Nome.Contains(filter));
            }

            var model = await PagingList.CreateAsync(resultado, 5, pageindex, sort, "Nome");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };

            return View(model);

        }

        // GET: Admin/AdminLanche/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Lanches == null)
            {
                return NotFound();
            }

            var lancheModel = await _context.Lanches
                .Include(l => l.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lancheModel == null)
            {
                return NotFound();
            }

            return View(lancheModel);
        }

        // GET: Admin/AdminLanche/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descricao");
            return View();
        }

        // POST: Admin/AdminLanche/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,DescricaoCurta,DescricacaoDetalhada,Preco,ImagemUrl,ImagemThumbnailUrl,IsLanchePreferido,EmEstoque,CategoriaId")] LancheModel lancheModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lancheModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descricao", lancheModel.CategoriaId);
            return View(lancheModel);
        }

        // GET: Admin/AdminLanche/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Lanches == null)
            {
                return NotFound();
            }

            var lancheModel = await _context.Lanches.FindAsync(id);
            if (lancheModel == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descricao", lancheModel.CategoriaId);
            return View(lancheModel);
        }

        // POST: Admin/AdminLanche/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,DescricaoCurta,DescricacaoDetalhada,Preco,ImagemUrl,ImagemThumbnailUrl,IsLanchePreferido,EmEstoque,CategoriaId")] LancheModel lancheModel)
        {
            if (id != lancheModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lancheModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LancheModelExists(lancheModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descricao", lancheModel.CategoriaId);
            return View(lancheModel);
        }

        // GET: Admin/AdminLanche/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Lanches == null)
            {
                return NotFound();
            }

            var lancheModel = await _context.Lanches
                .Include(l => l.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lancheModel == null)
            {
                return NotFound();
            }

            return View(lancheModel);
        }

        // POST: Admin/AdminLanche/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Lanches == null)
            {
                return Problem("Entity set 'AppDbContext.Lanches'  is null.");
            }
            var lancheModel = await _context.Lanches.FindAsync(id);
            if (lancheModel != null)
            {
                _context.Lanches.Remove(lancheModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LancheModelExists(int id)
        {
          return _context.Lanches.Any(e => e.Id == id);
        }
    }
}

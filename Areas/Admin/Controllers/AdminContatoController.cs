using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VendaDeLanches.Context;
using VendaDeLanches.Models;

namespace VendaDeLanches.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminContatoController : Controller
    {
        private readonly AppDbContext _context;

        public AdminContatoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminContato
        public async Task<IActionResult> Index()
        {
              return View(await _context.Mensagens.ToListAsync());
        }

        // GET: Admin/AdminContato/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Mensagens == null)
            {
                return NotFound();
            }

            var contatoModel = await _context.Mensagens
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contatoModel == null)
            {
                return NotFound();
            }

            return View(contatoModel);
        }

        // GET: Admin/AdminContato/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminContato/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DataEnvioDaMensagem,EmailParaContato,Mensagem")] ContatoModel contatoModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contatoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contatoModel);
        }

        // GET: Admin/AdminContato/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Mensagens == null)
            {
                return NotFound();
            }

            var contatoModel = await _context.Mensagens.FindAsync(id);
            if (contatoModel == null)
            {
                return NotFound();
            }
            return View(contatoModel);
        }

        // POST: Admin/AdminContato/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DataEnvioDaMensagem,EmailParaContato,Mensagem")] ContatoModel contatoModel)
        {
            if (id != contatoModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contatoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContatoModelExists(contatoModel.Id))
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
            return View(contatoModel);
        }

        // GET: Admin/AdminContato/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Mensagens == null)
            {
                return NotFound();
            }

            var contatoModel = await _context.Mensagens
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contatoModel == null)
            {
                return NotFound();
            }

            return View(contatoModel);
        }

        // POST: Admin/AdminContato/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Mensagens == null)
            {
                return Problem("Entity set 'AppDbContext.Mensagens'  is null.");
            }
            var contatoModel = await _context.Mensagens.FindAsync(id);
            if (contatoModel != null)
            {
                _context.Mensagens.Remove(contatoModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContatoModelExists(int id)
        {
          return _context.Mensagens.Any(e => e.Id == id);
        }
    }
}

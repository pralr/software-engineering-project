using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VendaDeLanches.Context;
using VendaDeLanches.Models;

namespace VendaDeLanches
{
    public class AdminPedidoController : Controller
    {
        private readonly AppDbContext _context;

        public AdminPedidoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: AdminPedido
        public async Task<IActionResult> Index()
        {
              return View(await _context.Pedidos.ToListAsync());
        }

        // GET: AdminPedido/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pedidos == null)
            {
                return NotFound();
            }

            var pedidoModel = await _context.Pedidos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedidoModel == null)
            {
                return NotFound();
            }

            return View(pedidoModel);
        }

        // GET: AdminPedido/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminPedido/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Sobrenome,Endereco1,Endereco2,Cep,Estado,Cidade,Telefone,Email,PedidoEnviado,PedidoEntregueEm")] PedidoModel pedidoModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pedidoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pedidoModel);
        }

        // GET: AdminPedido/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pedidos == null)
            {
                return NotFound();
            }

            var pedidoModel = await _context.Pedidos.FindAsync(id);
            if (pedidoModel == null)
            {
                return NotFound();
            }
            return View(pedidoModel);
        }

        // POST: AdminPedido/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Sobrenome,Endereco1,Endereco2,Cep,Estado,Cidade,Telefone,Email,PedidoEnviado,PedidoEntregueEm")] PedidoModel pedidoModel)
        {
            if (id != pedidoModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedidoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoModelExists(pedidoModel.Id))
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
            return View(pedidoModel);
        }

        // GET: AdminPedido/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pedidos == null)
            {
                return NotFound();
            }

            var pedidoModel = await _context.Pedidos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedidoModel == null)
            {
                return NotFound();
            }

            return View(pedidoModel);
        }

        // POST: AdminPedido/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pedidos == null)
            {
                return Problem("Entity set 'AppDbContext.Pedidos'  is null.");
            }
            var pedidoModel = await _context.Pedidos.FindAsync(id);
            if (pedidoModel != null)
            {
                _context.Pedidos.Remove(pedidoModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoModelExists(int id)
        {
          return _context.Pedidos.Any(e => e.Id == id);
        }
    }
}

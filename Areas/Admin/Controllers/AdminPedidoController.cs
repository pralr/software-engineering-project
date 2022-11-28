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
using VendaDeLanches.ViewModels;

namespace VendaDeLanches.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminPedidoController : Controller
    {
        private readonly AppDbContext _context;

        public AdminPedidoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminPedido
      /*  public async Task<IActionResult> Index()
        {
              return View(await _context.Pedidos.ToListAsync());
        }*/

        public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "Nome")
        {
            var resultado = _context.Pedidos.AsNoTracking().AsQueryable();
            // asnotracking as entidades não são rastreadas pelo contexto, assim o ef core
            // nao realiza nenhum processamento adicional nas entidades
            //asqueryable vai converter a colecao resultado em um tipo iqueryble, e usando 
            // ele os dados so vao ser obtidos do banco de dados qnd a fonte for avaliado 
            // aqui estou montando a consulta e nao obtendo os dados ainda 

            if(!string.IsNullOrEmpty(filter))
            {
                resultado = resultado.Where(p => p.Nome.Contains(filter));
            }


            // definir o objeto de paginacao (model ai)

            var model = await PagingList.CreateAsync(resultado, 5, pageindex, sort, "Nome");

            // incluir uma rota para o filtro que vai usar, exigencia do componente

            model.RouteValue = new RouteValueDictionary { { "filter", filter } };

            return View(model);
        }

        // GET: Admin/AdminPedido/Details/5
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

        // GET: Admin/AdminPedido/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminPedido/Create
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

        // GET: Admin/AdminPedido/Edit/5
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

        // POST: Admin/AdminPedido/Edit/5
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

        // GET: Admin/AdminPedido/Delete/5
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

        // POST: Admin/AdminPedido/Delete/5
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

        public IActionResult PedidoLanches(int? id)
        {
            var pedido = _context.Pedidos.Include(pd => pd.PedidoItens)
                .ThenInclude(l => l.Lanche)
                .FirstOrDefault(p => p.Id == id);

            if(pedido == null)
            {
                Response.StatusCode = 404;
                return View("PedidoNotFound", id.Value);
            }

            PedidoLancheViewModel pedidoLanches = new PedidoLancheViewModel()
            {
                Pedido = pedido,
                PedidoDetalhes = pedido.PedidoItens
            };

            return View(pedidoLanches);
        }

    }
}

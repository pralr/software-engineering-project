using Microsoft.EntityFrameworkCore;
using VendaDeLanches.Context;

namespace VendaDeLanches.Models
{
    public class CarrinhoCompra
    {
        private readonly AppDbContext _context;

        public CarrinhoCompra(AppDbContext context)
        {
            _context = context;
        }

        public string Id { get; set; }
        public List<CarrinhoCompraItemModel> CarrinhoCompraItems { get; set; }

        // get carrinho foi definido como estatico para poder invocar o metodo sem ter uma instancia da classe
        // e assim ja obter a partir da sessao um carrinho de compra com um contexto e lista de itens
        // na program.cs, registro este serviço
        public static CarrinhoCompra GetCarrinho(IServiceProvider services)
        {
            // define uma sessao
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            // obtem um serviço do tipo do nosso contexto
            var context = services.GetService<AppDbContext>();

            // obtem ou gera ro Id do carrinho
            string Id = session.GetString("Id") ?? Guid.NewGuid().ToString();

            // atribui o id do carrinho na sessao
            session.SetString("Id", Id);

            // retorna o carrinho com o tcontexto e o Id atribuido ou obtido

            return new CarrinhoCompra(context)
            {
                Id = Id
            };
        }


        public void AdicionarAoCarrinho(LancheModel lanche)
        {
            var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(
                s => s.Lanche.Id == lanche.Id && s.CarrinhoCompraId == Id);

            if (carrinhoCompraItem == null)
            {
                carrinhoCompraItem = new CarrinhoCompraItemModel
                {
                    CarrinhoCompraId = Id,
                    Lanche = lanche,
                    Quantidade = 1
                };

                _context.CarrinhoCompraItens.Add(carrinhoCompraItem);
            }
            else
            {
                carrinhoCompraItem.Quantidade++;
            }
            _context.SaveChanges();
        }




        public int RemoverDoCarrinho(LancheModel lanche)
        {

            var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(
                s => s.Lanche.Id == lanche.Id &&
                s.CarrinhoCompraId == Id);

            var quantidadeLocal = 0;

            if (carrinhoCompraItem != null)
            {
                if (carrinhoCompraItem.Quantidade > 1)
                {
                    carrinhoCompraItem.Quantidade--;
                    quantidadeLocal = carrinhoCompraItem.Quantidade;
                } else
                {
                    _context.CarrinhoCompraItens.Remove(carrinhoCompraItem);
                }
            }

            _context.SaveChanges();
            return quantidadeLocal;
        }

        public List<CarrinhoCompraItemModel> GetCarrinhoCompraItens()
        {
            // retorna uma instancia se ela ja existir, se nao, ela vai criar 
            return CarrinhoCompraItems ?? (CarrinhoCompraItems =
                _context.CarrinhoCompraItens
                .Where(c => c.CarrinhoCompraId == Id)
                .Include(s => s.Lanche)
                .ToList());
        }

        public void LimparCarrinho()
        {
            var carrinhoItens = _context.CarrinhoCompraItens
                .Where(carrinho => carrinho.CarrinhoCompraId == Id);

            _context.CarrinhoCompraItens.RemoveRange(carrinhoItens);

            _context.SaveChanges();
        }

        public decimal GetCarrinhoCompraTotal()
        {
            var total = _context.CarrinhoCompraItens.Where(c => c.CarrinhoCompraId == Id)
                        .Select(c => c.Lanche.Preco * c.Quantidade).Sum();

            return total;

            // pega um carrinho especifico e para este pega cada lanche e soma 
        }

    }

}

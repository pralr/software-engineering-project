using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendaDeLanches.Models
{
    public class CarrinhoCompraItemModel
    {
        public int Id { get; set; }
        public LancheModel Lanche { get; set; } // ef vai saber que tem que mapear uma chave estrangeira relacionado a tabela que vai gerar com a tabela lanches já gerada
        public int Quantidade { get; set; }
        [StringLength(200)] // pra nao gerar com nvachar max 
        public string CarrinhoCompraId { get; set; }


    }
}

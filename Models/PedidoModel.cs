using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace VendaDeLanches.Models
{
    public class PedidoModel
    {
        [Key]
        public int Id { get; set; }


        [StringLength(50)]
        [Required(ErrorMessage = "Informe o nome")]
        public string Nome { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Informe o sobrenome")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "Informe o endereço.")]
        [StringLength(100)]
        [Display(Name = "Endereço")]
        public string Endereco1 { get; set; }

        [Required(ErrorMessage = "Informe o complemento.")]
        [StringLength(100)]
        [Display(Name = "Complemento")]
        public string Endereco2 { get; set; }

        [Required(ErrorMessage = "Informe o seu CEP")]
        [Display(Name = "CEP")]
  
        public string Cep { get; set; }

        [Required(ErrorMessage = "Informe o seu Estado.")]
        [StringLength(10)]
        public string Estado { get; set; }

        [Required(ErrorMessage = "Informe sua cidade.")]
        [StringLength(50)]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "Informe o seu telefone")]
        [StringLength(25)]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Informe o seu email")]
        [StringLength(50)]

        public string Email { get; set; }

        [ScaffoldColumn(false)]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Total do Pedido")]
        public decimal PedidoTotal { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Itens do Pedido")]
        public int TotalItensPedido { get; set; }

        [Display(Name = "Data de Envio")]
        [DataType(DataType.Text)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyy hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime PedidoEnviado { get; set; }

        [Display(Name = "Data de Entrega")]
        [DataType(DataType.Text)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyy hh:mm}", ApplyFormatInEditMode =  true)]
        public DateTime? PedidoEntregueEm { get; set; }

        public List<PedidoDetalheModel> PedidoItens { get; set; }
    }
}

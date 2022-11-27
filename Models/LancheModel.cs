using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendaDeLanches.Models
{
    [Table("Lanches")]
    public class LancheModel
    {
        [Key]
        public int Id { get; set; }
        [StringLength(80, MinimumLength = 5, ErrorMessage = "O tamanho deve ter no mínimo 5 e no máximo 8.")]
        [Required(ErrorMessage = "O nome do lanche deve ser informado.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe a descrição do lanche.")]
        [Display(Name = "Descrição do lanche")]
        [MinLength(20, ErrorMessage = "Descrição deve ter no mínimo 20 caracteres")]
        [MaxLength(200, ErrorMessage = "Descrição deve ter no máximo 200 caracteres")]
        public string DescricaoCurta { get; set; }
        [Required(ErrorMessage = "Informe a descrição do lanche.")]
        [Display(Name = "Descrição detalhada do lanche")]
        [MinLength(50, ErrorMessage = "Descrição detalhada deve ter no mínimo 50 caracteres")]
        [MaxLength(400, ErrorMessage = "Descrição detalhada deve ter no máximo 400 caracteres")]
        public string DescricacaoDetalhada { get; set; }
        [Required(ErrorMessage = "Informe o preço do lanche")]
        [Display(Name = "Preço")]
        [Column(TypeName = "decimal(10,2)")]
        [Range(1, 999.99, ErrorMessage = "O preço deve está entre 1 e 999,99")]
        public decimal Preco { get; set; }
        [Display(Name = "Caminho Imagem Normal")]
        [StringLength(200)]
        public string ImagemUrl { get; set; }
        [Display(Name = "Caminho Imagem Miniatura")]
        [StringLength(200)]
        public string ImagemThumbnailUrl { get; set; }
        [Display(Name = "Preferido?")]
        public bool IsLanchePreferido { get; set; }
        [Display(Name = "Estoque")]
        public bool EmEstoque { get; set; }

        public int CategoriaId { get; set; }
        public virtual CategoriaModel Categoria { get; set; }
    }
}

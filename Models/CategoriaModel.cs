using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendaDeLanches.Models
{
    [Table("Categorias")]
    public class CategoriaModel
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100, ErrorMessage = "O tamanho máximo permitido é 100 caracteres.")]
        [Required(ErrorMessage = "Informe o nome da categoria.")]
        [Display(Name = "Nome")]

        public string Nome { get; set; }
        [StringLength(300, ErrorMessage = "O tamanho máximo permitido é 300 caracteres.")]
        [Required(ErrorMessage = "Informe a descrição da categoria.")]
        [Display(Name = "Nome")]
        public string Descricao { get; set; }

        public List<LancheModel> Lanches { get; set; }
    }
}

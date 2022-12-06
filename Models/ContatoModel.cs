using System.ComponentModel.DataAnnotations;

namespace VendaDeLanches.Models
{
    public class ContatoModel
    {
        public int Id { get; set; }
        [Display(Name ="Data de envio")]
        public DateTime DataEnvioDaMensagem { get; set; }
        [Display(Name = "Email para contato")]
        public string EmailParaContato { get; set; }
        public string Mensagem { get; set; }
    }
}

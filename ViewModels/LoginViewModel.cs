using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using Xunit.Abstractions;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace VendaDeLanches.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Informe o nome")]
        [Display(Name = "Usuário")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        /*
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [Display(Name = "Complemento")]
        public string Complemento { get; set; }
    
        [Display(Name = "Estado")]
        public string Estado { get; set; }

        [Display(Name = "Estado")]
        public string Cidade { get; set; }
        */

        public string ReturnUrl { get; set; }

        // objetivo: enviar automaticamente o usuario de volta a pagina onde ele estava tentando
        // acessar antes de ser autenticado

    }
}

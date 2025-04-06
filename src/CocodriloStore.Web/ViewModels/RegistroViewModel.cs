using System.ComponentModel.DataAnnotations;

namespace CocodriloStore.Web.ViewModels
{
    public class RegistroViewModel
    {
        [Required]
        [Display(Name = "Nome completo")]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmação de Senha")]
        [Compare("Password", ErrorMessage = "A senha e a confirmação devem ser iguais.")]
        public string ConfirmPassword { get; set; }
    }
}
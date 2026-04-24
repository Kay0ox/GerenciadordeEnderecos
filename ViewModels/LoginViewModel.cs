using System.ComponentModel.DataAnnotations;
// tela de login e senha
namespace MeuSiteEmMVC.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Digite o seu Usuario")]
        public string Login { get; set; } = string.Empty;


        [Required(ErrorMessage = "Digite a sua senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; } = string.Empty;

    }
}

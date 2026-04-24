using System.ComponentModel.DataAnnotations;

namespace MeuSiteEmMVC.ViewModels
{
    public class RegistroViewModel
    {
        [Required(ErrorMessage = "Informe o nome")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe o usuário")]
        public string Login { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe a senha")]
        [MinLength(6, ErrorMessage = "Senha deve ter no mínimo 6 caracteres")]
        [DataType(DataType.Password)]
        public string Senha { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirme a senha")]
        [DataType(DataType.Password)]
        [Compare("Senha", ErrorMessage = "As senhas não coincidem")]
        public string ConfirmarSenha { get; set; } = string.Empty;
    }
}
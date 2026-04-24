using System.ComponentModel.DataAnnotations;

namespace MeuSiteEmMVC.Models
{
    public class Usuario
    {
        public int ID { get; set; }


        [Required]
        [Display(Name = "Nome Completo")]
        public string Nome { get; set; }


        [Required]
        [Display(Name = "Usuário")]
        public string Login { get; set; }


        [Required]
        [Display(Name = "Senha")]
        public string Senha { get; set; }



    }
}

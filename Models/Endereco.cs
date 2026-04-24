using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeuSiteEmMVC.Models
{
    public class Endereco
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O CEP É Obrigatorio")]
        [Display(Name = "CEP")]
        public string Cep { get; set; }

        [Required(ErrorMessage = "O Logradouro É Obrigatorio")]
        [Display(Name = "Logradouro")]
        public string Logradouro { get; set; }

        [Display(Name = "O Complemento")]
        public string? Complemento { get; set; }

        [Required(ErrorMessage = "O Bairro É Obrigatorio")]
        [Display(Name = "Bairro")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "A Cidade É obrigatoria")]
        [Display(Name = "Cidade")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "A UF É obrigatoria")]
        [Display(Name = "UF")]
        [MaxLength(2)]
        public string Uf { get; set; }

        [Required(ErrorMessage = "O Número É Obrigatorio")]
        [Display(Name = "Número")]
        public string Numero { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuario? UsuarioNav { get; set; }
    }
}
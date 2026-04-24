using System.ComponentModel.DataAnnotations;

namespace MeuSiteEmMVC.ViewModels
{
    public class EnderecoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O CEP É Obrigatorio")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "CEP deve ter 8 ou 9 caracteres")]
        public string Cep { get; set; } = string.Empty;

        [Required(ErrorMessage = "O logradouro é Obrigatorio")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Logradouro deve ter entre 3 e 200 caracteres")]
        public string Logradouro { get; set; } = string.Empty;

        public string? Complemento { get; set; }

        [Required(ErrorMessage = "O Bairro é Obrigatorio")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Bairro deve ter entre 3 e 100 caracteres")]
        public string Bairro { get; set; } = string.Empty;

        [Required(ErrorMessage = "A Cidade é Obrigatoria")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Cidade deve ter entre 3 e 100 caracteres")]
        public string Cidade { get; set; } = string.Empty;

        [Required(ErrorMessage = "A UF é Obrigatoria")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "UF deve ter 2 caracteres")]
        public string Uf { get; set; } = string.Empty;

        [Required(ErrorMessage = "O Número é Obrigatorio")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Número deve ter no máximo 20 caracteres")]
        public string Numero { get; set; } = string.Empty;


    }
}

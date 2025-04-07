using System.ComponentModel.DataAnnotations;
using PlataformaAPI.Models;

namespace PlataformaAPI.Data.Dtos
{
    public class CreateUsuarioDto
    {
        [Required]
        public string NomeCompleto { get; set; }

        [Required]
        public DateTime DataNascimento { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve ter 11 dígitos.")]
        public string CPF { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
        public string Password { get; set; }

        [Required]
        public TipoUsuario TipoUsuario { get; set; }
    }
}

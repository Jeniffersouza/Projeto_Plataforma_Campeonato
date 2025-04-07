using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PlataformaAPI.Models
{
    // Enum para tipos de usuário
    public enum TipoUsuario
    {
        Atleta = 1,
        Administrador = 2,
        
    }

    public class Usuario : IdentityUser
    {
        [Required(ErrorMessage = "O nome completo é obrigatório.")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve ter 11 dígitos.")]
        public string CPF { get; set; }

   
        // Definindo um tipo de usuário como enum para garantir valores válidos
        [Required]
        public TipoUsuario TipoUsuario { get; set; }
    }
}

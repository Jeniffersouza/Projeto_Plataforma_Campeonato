using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PlataformaAPI.Models
{
    // Enum para tipos de usuário
    public enum TipoUsuario
    {
        Atleta = 1,
        Administrador = 2,
        AtletaAdministrador = 3 // Caso precise de um usuário que tenha ambos os papéis
    }

    public class Usuario : IdentityUser
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public DateTime DataNascimento { get; set; }

        // Definindo um tipo de usuário como enum para garantir valores válidos
        [Required]
        public TipoUsuario TipoUsuario { get; set; }
    }
}

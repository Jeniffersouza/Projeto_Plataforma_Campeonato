using System.ComponentModel.DataAnnotations;

namespace PlataformaJiujitsu.Data.Dtos
{
    public class CreateProfessorDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; }

        public int? AcademiaId { get; set; } // Agora é opcional
    }
}

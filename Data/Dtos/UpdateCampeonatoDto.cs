using System.ComponentModel.DataAnnotations;

namespace PlataformaAPI.Data.Dtos
{
    public class UpdateCampeonatoDto
    {
        [Required(ErrorMessage = "O nome do campeonato é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A data de início é obrigatória.")]
        public DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "A data de fim é obrigatória.")]
        [DataType(DataType.Date)]
        public DateTime DataFim { get; set; }

        [Required(ErrorMessage = "A descrição do evento é obrigatória.")]
        [StringLength(500, ErrorMessage = "A descrição não pode ter mais de 500 caracteres.")]
        public string SobreEvento { get; set; }

        [Required(ErrorMessage = "O local do evento é obrigatório.")]
        public string LocalEvento { get; set; }

    }
}

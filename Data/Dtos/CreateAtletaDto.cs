using PlataformaJiujitsu.Models;
using System.ComponentModel.DataAnnotations;

namespace PlataformaJiujitsu.Data.Dtos;

public class CreateAtletaDto
{
    [Required(ErrorMessage = "O sexo é obrigatório.")]
    public SexoEnum Sexo { get; set; }

    [Required(ErrorMessage = "A graduação é obrigatória.")]
    public int GraduacaoId { get; set; }

    [Required(ErrorMessage = "O peso é obrigatório.")]
    public decimal Peso { get; set; }

    [Required(ErrorMessage = "O esporte é obrigatório.")]
    public int EsporteId { get; set; }

    public int? AcademiaId { get; set; }

    public int? ProfessorId { get; set; }

}

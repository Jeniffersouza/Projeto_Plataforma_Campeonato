using System.ComponentModel.DataAnnotations;

namespace PlataformaJiujitsu.Models;

public class Professor
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Nome { get; set; }

    [Required]
    public int? AcademiaId { get; set; }
    public Academia Academia { get; set; }
}

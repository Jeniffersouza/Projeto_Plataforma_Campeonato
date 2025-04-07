using System.ComponentModel.DataAnnotations;

namespace PlataformaJiujitsu.Models;

public class Esporte
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Nome { get; set; }
}

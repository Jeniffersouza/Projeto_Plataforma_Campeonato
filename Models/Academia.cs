using System.ComponentModel.DataAnnotations;

namespace PlataformaJiujitsu.Models;

public class Academia
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Nome { get; set; }

    public ICollection<Professor> Professores { get; set; }
}



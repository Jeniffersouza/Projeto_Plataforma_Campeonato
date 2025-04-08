using PlataformaJiujitsu.Models;
using System.ComponentModel.DataAnnotations;

namespace PlataformaAPI.Models;

public class Campeonato
{
    [Key]
    [Required]
    public int Id { get; set; }

    public string Nome { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    public DateTime DataInicio { get; set; }

    [Required(ErrorMessage = "A data é obrigatória")]
    public DateTime DataFim { get; set; }

    [Required(ErrorMessage = "A descrição do evento é obrigatória")]
    public string SobreEvento { get; set; }

    [Required(ErrorMessage = "O local do evento é obrigatório")]
    public string LocalEvento { get; set; }

    // Relacionamento com Inscrição
    public ICollection<Inscricao> Inscricoes { get; set; }
}


using PlataformaJiujitsu.Models;
using System.ComponentModel.DataAnnotations;

namespace PlataformaAPI.Models;

    public enum StatusCampeonato
{
    Pendente,
    EmAndamento,
    Encerrado
}

public class Campeonato
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome do campeonato é obrigatório.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "A data de início é obrigatória.")]
    public DateTime DataInicio { get; set; }

    [Required(ErrorMessage = "A data de fim é obrigatória.")]
    public DateTime DataFim { get; set; }

    [Required(ErrorMessage = "A descrição do evento é obrigatória.")]
    [StringLength(500, ErrorMessage = "A descrição não pode ter mais de 500 caracteres.")]
    public string SobreEvento { get; set; }

    [Required(ErrorMessage = "O local do evento é obrigatório.")]
    public string LocalEvento { get; set; }

    // Status do campeonato
    public StatusCampeonato Status { get; set; } = StatusCampeonato.Pendente;

    // Taxa de inscrição
    [Range(0, double.MaxValue, ErrorMessage = "A taxa de inscrição não pode ser negativa.")]
    public decimal TaxaInscricao { get; set; } = 0;

    // Premiação
    public string Premiacoes { get; set; }

    // Limite de inscritos
    [Range(1, int.MaxValue, ErrorMessage = "O número máximo de inscritos deve ser pelo menos 1.")]
    public int MaxInscritos { get; set; }

    // Faixa etária permitida
    public int IdadeMinima { get; set; }
    public int IdadeMaxima { get; set; }

    // Link para regulamento e inscrição
    public string LinkRegulamento { get; set; }
    public string LinkInscricao { get; set; }

    // Relacionamento com categorias
    public ICollection<Categoria> Categorias { get; set; } = new List<Categoria>();
}


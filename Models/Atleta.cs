using PlataformaAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace PlataformaJiujitsu.Models;
public enum SexoEnum
{
    Masculino = 1,
    Feminino = 2
}

public class Atleta
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O UsuarioId é obrigatório.")]
    public string UsuarioId { get; set; }
    public Usuario Usuario { get; set; }

    [Required(ErrorMessage = "O sexo é obrigatório.")]
    public SexoEnum Sexo { get; set; }  // Masculino ou Feminino

    [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
    public DateTime DataNascimento { get; set; }


    // Graduação (de acordo com o esporte)
    [Required(ErrorMessage = "A graduação é obrigatória.")]
    public int GraduacaoId { get; set; }
    public Graduacao Graduacao { get; set; }
    [Required(ErrorMessage = "O peso é obrigatório.")]
    public decimal Peso { get; set; } // Peso em kg

    
    // Relacionamento com esporte
    [Required(ErrorMessage = "O esporte é obrigatório.")]
    public int EsporteId { get; set; }
    public Esporte Esporte { get; set; }

    [Required(ErrorMessage = "A academia é obrigatória.")]
    public int? AcademiaId { get; set; }
    public Academia Academia { get; set; }
    // Relacionamento com professor
    public int? ProfessorId { get; set; }
    public Professor Professor { get; set; }

    // Relacionamento com Inscrição
    public ICollection<Inscricao> Inscricoes { get; set; }
}


using PlataformaAPI.Models;
using PlataformaJiujitsu.Models;
using System.ComponentModel.DataAnnotations;

public class Categoria
{
    public int Id { get; set; }

    [Required]
    public string Nome { get; set; }

    [Required]
    public decimal PesoMinimo { get; set; }

    [Required]
    public decimal PesoMaximo { get; set; }

    [Required]
    public string FaixaEtaria { get; set; } // Ex: "Adulto", "Juvenil", etc

    [Required]
    public SexoEnum Sexo { get; set; }

    [Required]
    public int GraduacaoId { get; set; }
    public Graduacao Graduacao { get; set; }

    public int CampeonatoId { get; set; }
    public Campeonato Campeonato { get; set; }

    public ICollection<Inscricao> Inscricoes { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace PlataformaJiujitsu.Models;

public class Graduacao
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Nome { get; set; } // Exemplo: "Faixa Azul"

    [Required]
    public int EsporteId { get; set; }
    public Esporte Esporte { get; set; }
}

using PlataformaAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace PlataformaJiujitsu.Models;

public class Categoria
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Nome { get; set; } // Exemplo: "Adulto - Leve (até 76kg)"

    [Required]
    [RegularExpression("Masculino|Feminino", ErrorMessage = "O sexo deve ser 'Masculino' ou 'Feminino'.")]
    public string Sexo { get; set; } // Adicionando o campo Sexo
    [Required]
    public string Faixa { get; set; } // Exemplo: "Faixa Azul"

    [Required]
    public decimal PesoMaximo { get; set; } // Exemplo: 76.0 kg

    [Required]
    public int CampeonatoId { get; set; }
    public Campeonato Campeonato { get; set; }
}

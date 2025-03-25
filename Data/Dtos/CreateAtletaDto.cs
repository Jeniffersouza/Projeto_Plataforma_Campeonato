using PlataformaJiujitsu.Models;

namespace PlataformaJiujitsu.Data.Dtos;

public class CreateAtletaDto
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Graduacao { get; set; }
    public decimal Peso { get; set; }
    public int? AcademiaId { get; set; }

}

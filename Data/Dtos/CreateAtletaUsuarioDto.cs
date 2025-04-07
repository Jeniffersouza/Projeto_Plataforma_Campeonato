using PlataformaJiujitsu.Models;

namespace PlataformaJiujitsu.Data.Dtos;

public class CreateAtletaUsuarioDto
{
    // Dados do usuário
    public string NomeCompleto { get; set; }
    public DateTime DataNascimento { get; set; }
    public string CPF { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    // Dados do atleta
    public SexoEnum Sexo { get; set; }
    public int GraduacaoId { get; set; }
    public decimal Peso { get; set; }
    public int EsporteId { get; set; }
    public int? AcademiaId { get; set; }
    public int? ProfessorId { get; set; }
}


namespace PlataformaJiujitsu.Data.Dtos;

public class CategoriaDto
{
    public string Nome { get; set; }
    public string Sexo { get; set; } // "Masculino" ou "Feminino"
    public string FaixaEtaria { get; set; } // Corrigido
    public decimal PesoMinimo { get; set; }
    public decimal PesoMaximo { get; set; }
    public int CampeonatoId { get; set; }
    public int GraduacaoId { get; set; } // Adicionado
}

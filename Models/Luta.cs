using PlataformaJiujitsu.Models;

public class Luta
{
    public int Id { get; set; }
    public int ChaveId { get; set; }
    public Chave Chave { get; set; }

    public int Atleta1Id { get; set; }
    public Atleta Atleta1 { get; set; }

    public int Atleta2Id { get; set; }
    public Atleta Atleta2 { get; set; }

    public int? PlacarAtleta1 { get; set; }
    public int? PlacarAtleta2 { get; set; }
    
}

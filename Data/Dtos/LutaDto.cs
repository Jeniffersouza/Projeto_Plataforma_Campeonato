public class LutaDto
{
    public int Id { get; set; }
    public int Atleta1Id { get; set; }
    public string Atleta1Nome { get; set; }
    public int Atleta2Id { get; set; }
    public string Atleta2Nome { get; set; }
    public int? PlacarAtleta1 { get; set; }
    public int? PlacarAtleta2 { get; set; }
    public string Resultado { get; set; }
}

public class ChaveDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Status { get; set; }
    public int CategoriaId { get; set; }
    public List<LutaDto> Lutas { get; set; }
}

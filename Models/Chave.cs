using PlataformaJiujitsu.Models;

public class Chave
{

    public int Id { get; set; }
    public string Nome { get; set; }
    
    public int CategoriaId { get; set; }
    public Categoria Categoria { get; set; }
    public ICollection<Luta> Lutas { get; set; }
    public ICollection<Inscricao> Inscricoes { get; set; }
}

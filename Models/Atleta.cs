using PlataformaAPI.Models;

namespace PlataformaJiujitsu.Models;

public class Atleta
{
    public int Id { get; set; }
    public string UsuarioId { get; set; }
    public Usuario Usuario { get; set; }

    public string Graduacao { get; set; }
    public decimal Peso { get; set; }
    public int? AcademiaId { get; set; }
    public Academia? Academia { get; set; }

    // Relacionamento com Inscrição
    public ICollection<Inscricao> Inscricoes { get; set; }
}


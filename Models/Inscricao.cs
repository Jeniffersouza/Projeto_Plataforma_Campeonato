using PlataformaAPI.Models;

namespace PlataformaJiujitsu.Models;

public class Inscricao
{
    public int Id { get; set; }
    public int AtletaId { get; set; }
    public int CampeonatoId { get; set; }
    public DateTime DataInscricao { get; set; }

    public Atleta Atleta { get; set; }
    public Campeonato Campeonato { get; set; }
}


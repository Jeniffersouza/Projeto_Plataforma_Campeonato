using System.ComponentModel.DataAnnotations;

namespace PlataformaAPI.Data.Dtos
{
    public class ReadCampeonatoDto
    {
        public string Nome { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string SobreEvento { get; set; }
        public string LocalEvento { get; set; }
        public DateTime HoraDaConsulta { get; set; } = DateTime.Now;
    }
}

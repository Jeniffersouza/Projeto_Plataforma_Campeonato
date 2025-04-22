﻿using System.ComponentModel.DataAnnotations;

namespace PlataformaAPI.Data.Dtos
{
    public class UpdateCampeonatoDto
    {
        [Required(ErrorMessage = "O nome do campeonato é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A data de início é obrigatória.")]
        public DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "A data de fim é obrigatória.")]
        public DateTime DataFim { get; set; }

        [Required(ErrorMessage = "A descrição do evento é obrigatória.")]
        [StringLength(500, ErrorMessage = "A descrição não pode ter mais de 500 caracteres.")]
        public string SobreEvento { get; set; }

        [Required(ErrorMessage = "O local do evento é obrigatório.")]
        public string LocalEvento { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "A taxa de inscrição não pode ser negativa.")]
        public decimal TaxaInscricao { get; set; }

        public string Premiacoes { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "O número máximo de inscritos deve ser pelo menos 1.")]
        public int MaxInscritos { get; set; }

        public int IdadeMinima { get; set; }
        public int IdadeMaxima { get; set; }

        public string LinkRegulamento { get; set; }
        public string LinkInscricao { get; set; }
    }
}

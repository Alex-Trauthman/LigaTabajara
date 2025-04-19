using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SistemaTabajara.Models
{
    public class Partida
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DataHora { get; set; }

        [Required]
        public int Rodada { get; set; }

        [Required, MaxLength(100)]
        public string Estadio { get; set; }

        [ForeignKey(nameof(Mandante))]
        public int MandanteId { get; set; }
        public Time Mandante { get; set; }

        [ForeignKey(nameof(Visitante))]
        public int VisitanteId { get; set; }
        public Time Visitante { get; set; }

        [Required]
        public int LigaId { get; set; }
        [ForeignKey("LigaId")]
        public Liga Liga { get; set; }

        [NotMapped]
        public int GolsMandante => Estatisticas.SelectMany(e => e.Gols).Count(g => g.Jogador.TimeId == MandanteId);

        [NotMapped]
        public int GolsVisitante => Estatisticas.SelectMany(e => e.Gols).Count(g => g.Jogador.TimeId == VisitanteId);

        public virtual ICollection<EstatisticaPartida> Estatisticas { get; set; } = new List<EstatisticaPartida>();

        [NotMapped]
        public bool IsValid => MandanteId != VisitanteId;
    }

}
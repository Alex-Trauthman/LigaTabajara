using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaTabajara.Models
{
    public class Participacao
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Time))]
        public int TimeId { get; set; }
        public Time Time { get; set; }

        [ForeignKey(nameof(Liga))]
        public int LigaId { get; set; }
        public Liga Liga { get; set; }

        public int Pontos { get; set; }
        public int Jogos { get; set; }
        public int Vitorias { get; set; }
        public int Empates { get; set; }
        public int Derrotas { get; set; }
        public int GolsPro { get; set; }
        public int GolsContra { get; set; }

        [NotMapped]
        public int SaldoGols => GolsPro - GolsContra;
    }

}

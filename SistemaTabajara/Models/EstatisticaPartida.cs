using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaTabajara.Models
{
    public class EstatisticaPartida
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Jogador))]
        public int JogadorId { get; set; }
        public Jogador Jogador { get; set; }

        [ForeignKey(nameof(Partida))]
        public int PartidaId { get; set; }
        public Partida Partida { get; set; }

        public int MinutosJogados { get; set; }
        public int Assistencias { get; set; }

        public virtual ICollection<Gol> Gols { get; set; } = new List<Gol>();
    }


}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaTabajara.Models
{
    public enum TipoGol
    {
        Normal,
        Penalti,
        ContraGol
    }

    public class Gol
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PartidaId { get; set; }
        [ForeignKey("PartidaId")]
        public Partida Partida { get; set; }

        [Required]
        public int JogadorId { get; set; }
        [ForeignKey("JogadorId")]
        public Jogador Jogador { get; set; }

        [Required, Range(1, 120)]
        public int Minuto { get; set; }

        [Required]
        public TipoGol TipoGol { get; set; }
    }




}
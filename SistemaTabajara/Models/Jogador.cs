using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaTabajara.Models
{
    public class Jogador
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Nome { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        [Required, MaxLength(50)]
        public string Nacionalidade { get; set; }

        [Required]
        public Posicao Posicao { get; set; }

        [Required]
        public int Camisa { get; set; }

        [Required]
        public float Altura { get; set; } // in meters

        [Required]
        public float Peso { get; set; } // in kilograms

        [Required]
        public PeDominante PeDominante { get; set; }

        [ForeignKey(nameof(Time))]
        public int TimeId { get; set; }
        public Time Time { get; set; }

        public virtual ICollection<Gol> Gols { get; set; } = new List<Gol>();
        public virtual ICollection<Cartao> Cartoes { get; set; } = new List<Cartao>();
    }


    public enum Posicao 
	{
		Goleiro,
		LateralDireito,
		Zagueiro,
		LateralEsquerdo,
		MeioCampista,
		MeioAtacante,
		Ponta,
		CentroAvante
	}
	public enum PeDominante
	{
		Esquerdo,
		Direito,
		Ambidestro,
		//Devido à qualidade dos jogadores, se faz necessária mais uma opção
		Nenhum
	}

}
	using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaTabajara.Models
{
    public enum StatusTime {
        Apto,
        Inapto,
        
    }

    public enum Estado
    {
        Acre, Alagoas, Amapa,Amazonas,Bahia, Ceara,DistritoFederal, EspiritoSanto,Goias, 
        Maranhao, MatoGrosso,MatoGrossoSul,MinasGerais,Para,Paraiba,Parana,Pernambuco,
        RioDeJaneiro,RioGrandeDoNorte,RioGrandeDoSul,Rondonia,Roraima,
        SantaCatarina,SaoPaulo,Sergipe,Tocantins
    }
    public class Time
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100), Index(IsUnique = true)]
        public string Nome { get; set; }

        [Required]
        public DateTime DataFundacao { get; set; }

        [Required, MaxLength(100)]
        public string Estadio { get; set; }

        [Required, MaxLength(100)]
        public string Cidade { get; set; }

        [Required]
        public Estado Estado { get; set; }

        [Required]
        public int CapacidadeEstadio { get; set; }

        [Required, MaxLength(50)]
        public string CorPrincipal { get; set; }

        [Required, MaxLength(50)]
        public string CorSecundaria { get; set; }

        [NotMapped]
        public StatusTime Status => IsApto() ? StatusTime.Apto : StatusTime.Inapto;

        public virtual ICollection<Jogador> Jogadores { get; set; } = new List<Jogador>();
        public virtual ICollection<ComissaoTecnica> ComissaoTecnica { get; set; } = new List<ComissaoTecnica>();
        public virtual ICollection<Partida> PartidasCasa { get; set; } = new List<Partida>();
        public virtual ICollection<Partida> PartidaFora { get; set; } = new List<Partida>();
        public virtual ICollection<Participacao> Participacoes { get; set; } = new List<Participacao>();

        private bool IsApto()
        {
            return Jogadores.Count >= 17 &&
                   ComissaoTecnica.Count >= 5 &&
                   ComissaoTecnica.GroupBy(c => c.Cargo).All(g => g.Count() == 1) &&
                   !string.IsNullOrEmpty(Nome) &&
                   !string.IsNullOrEmpty(Estadio) &&
                   !string.IsNullOrEmpty(Cidade) &&
                   CapacidadeEstadio > 0;
        }
    }


}
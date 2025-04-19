using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace SistemaTabajara.Models
{
    // O atributo de gol já existe em estatística da partida, está que está dentro de partidas, por isso, será obtido em uma consulta.
    public class Liga
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

        [Required, MaxLength(100), Index(IsUnique = true)]
        public string Nome { get; set; }

        [NotMapped]
        public StatusLiga Status => IsApta() ? StatusLiga.Apta : StatusLiga.Inapta;

        public virtual ICollection<Participacao> Participacoes { get; set; } = new List<Participacao>();

        private bool IsApta()
        {
            return Participacoes.Count == 20 &&
                   Participacoes.All(p => p.Time.Status == StatusTime.Apto);
        }
    }
    public enum StatusLiga {
        Apta,
        Inapta
    }

}

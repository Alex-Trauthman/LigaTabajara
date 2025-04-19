using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaTabajara.Models
{
    public enum Cargo
    {
        Treinador,
        Auxiliar,
        PreparadorFisico,
        Fisiologista,
        TreinadorGoleiros,
        Fisioterapeuta
    }

    public class ComissaoTecnica
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Nome { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        [Required]
        public Cargo Cargo { get; set; }

        [ForeignKey(nameof(Time))]
        public int TimeId { get; set; }
        public Time Time { get; set; }
    }
}

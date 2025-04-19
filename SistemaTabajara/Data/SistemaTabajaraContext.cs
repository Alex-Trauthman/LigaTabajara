using System.Data.Entity;
using SistemaTabajara.Models;

namespace SistemaTabajara.Data
{
    public class SistemaTabajaraContext : DbContext
    {
        public DbSet<Time> Times { get; set; }
        public DbSet<Jogador> Jogadores { get; set; }
        public DbSet<ComissaoTecnica> ComissaoTecnicas { get; set; }
        public DbSet<Liga> Ligas { get; set; }
        public DbSet<Participacao> Participacoes { get; set; }
        public DbSet<Partida> Partidas { get; set; }
        public DbSet<EstatisticaPartida> EstatisticaPartidas { get; set; }
        public DbSet<Gol> Gols { get; set; }
        public DbSet<Cartao> Cartoes { get; set; }

        public SistemaTabajaraContext() : base("name=LigaTabajaraContext") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Time>()
                .HasIndex(t => t.Nome)
                .IsUnique();

            modelBuilder.Entity<Liga>()
                .HasIndex(l => l.Nome)
                .IsUnique();

            modelBuilder.Entity<Jogador>()
                .HasIndex(j => new { j.TimeId, j.Camisa })
                .IsUnique();

            modelBuilder.Entity<ComissaoTecnica>()
                .HasIndex(c => new { c.TimeId, c.Cargo })
                .IsUnique();

            modelBuilder.Entity<Jogador>()
                .HasRequired(j => j.Time)
                .WithMany(t => t.Jogadores)
                .HasForeignKey(j => j.TimeId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<ComissaoTecnica>()
                .HasRequired(c => c.Time)
                .WithMany(t => t.ComissaoTecnica)
                .HasForeignKey(c => c.TimeId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Participacao>()
                .HasRequired(p => p.Time)
                .WithMany(t => t.Participacoes)
                .HasForeignKey(p => p.TimeId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Participacao>()
                .HasRequired(p => p.Liga)
                .WithMany(l => l.Participacoes)
                .HasForeignKey(p => p.LigaId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Partida>()
                .HasRequired(p => p.Mandante)
                .WithMany(t => t.PartidasCasa)
                .HasForeignKey(p => p.MandanteId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Partida>()
                .HasRequired(p => p.Visitante)
                .WithMany(t => t.PartidaFora)
                .HasForeignKey(p => p.VisitanteId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Partida>()
                .HasRequired(p => p.Liga)
                .WithMany()
                .HasForeignKey(p => p.LigaId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<EstatisticaPartida>()
                .HasRequired(e => e.Jogador)
                .WithMany()
                .HasForeignKey(e => e.JogadorId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<EstatisticaPartida>()
                .HasRequired(e => e.Partida)
                .WithMany(p => p.Estatisticas)
                .HasForeignKey(e => e.PartidaId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Gol>()
                .HasRequired(g => g.Jogador)
                .WithMany(j => j.Gols)
                .HasForeignKey(g => g.JogadorId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Gol>()
                .HasRequired(g => g.Partida)
                .WithMany()
                .HasForeignKey(g => g.PartidaId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Cartao>()
                .HasRequired(c => c.Jogador)
                .WithMany(j => j.Cartoes)
                .HasForeignKey(c => c.JogadorId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Cartao>()
                .HasRequired(c => c.Partida)
                .WithMany()
                .HasForeignKey(c => c.PartidaId)
                .WillCascadeOnDelete(true);
        }
    }
}
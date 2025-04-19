using System.Data.Entity.Migrations;

public partial class AddInitialData : DbMigration
{
    public override void Up()
    {
        Sql("INSERT INTO Ligas (Nome, DataInicio, DataFim) VALUES ('Liga Tabajara 2024', '2024-01-01', '2024-12-31')");

        for (int i = 1; i <= 20; i++)
        {
            Sql($"INSERT INTO Times (Nome, DataFundacao, Estadio, Cidade, Estado, CapacidadeEstadio, CorPrincipal, CorSecundaria) " +
                $"VALUES ('Time {i}', '2000-01-01', 'Estádio {i}', 'Cidade {i}', 0, 50000, 'Azul', 'Branco')");
        }

        for (int i = 1; i <= 20; i++)
        {
            Sql($"INSERT INTO Participacoes (TimeId, LigaId, Pontos, Jogos, Vitorias, Empates, Derrotas, GolsPro, GolsContra) " +
                $"VALUES ({i}, 1, 0, 0, 0, 0, 0, 0, 0)");
        }
    }

    public override void Down()
    {
        Sql("DELETE FROM Participacoes WHERE LigaId = 1");
        Sql("DELETE FROM Times WHERE Nome LIKE 'Time %'");
        Sql("DELETE FROM Ligas WHERE Nome = 'Liga Tabajara 2024'");
    }
}
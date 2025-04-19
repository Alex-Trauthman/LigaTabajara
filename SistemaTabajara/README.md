# Objetivo do Projeto

Desenvolver uma aplica��o web utilizando .NET Framework, SQL Server e Migrations para gerenciar a Liga Tabajara de Futebol. O sistema dever� permitir o cadastro e gerenciamento de times, jogadores, comiss�o t�cnica, partidas, estat�sticas e classifica��o geral do campeonato, obedecendo regras espec�ficas de funcionamento da liga.

Desta forma, voc� deve ter uma p�gina inicial que contenha a apresenta��o de sua liga e liste todos os times que fazem parte da liga. Cada time deve ter sua p�gina que apresente suas caracter�sticas.

 

Desta forma, o sistema web deve permitir o cadastro dos:

Time
Jogadores
Comiss�o t�cnica
Partidas
Estat�sticas dos jogos
Dessa forma, os times pertencem a liga e os jogadores e a comiss�o t�cnica estar�o vinculados com um time. As partidas ter�o seu agendamento marcado, e as estat�sticas dos jogadores estar�o vinculadas a cada uma das partidas.

Detalhadamente, teremos:

Time
nome
cidade
estado
ano funda��o
est�dio
capacidade do est�dio
Cores do Uniforme (primaria e secund�ria)
status

Jogador
nome
data de nascimento
nacionalidade
posi��o (enum: goleiro, zagueiro, volante, meia, atacante etc)
n�mero da camisa
altura
peso
p� preferido (enum: esquerdo, direito, ambidestro)
Time

Comiss�o T�cnica
nome
cargo (enum: treinador, auxiliar, preparador f�sico, fisiologista, treinador de goleiros e fisioterapeuta)
data nascimento
Time
Tabela

Lista dos jogos todos contra todos, com um jogo em casa e outro fora.
O registro dos resultados dos jogos.
Quando registrar o resultado se houver gols, deve-se informar qual do jogador fez cada um dos gols.
Deve-se ter a lista dos artilheiros do campeonato
Cada jogo deve gerar um dos resultados (vit�ria e/ou empate).
Para vit�ria ser�o computados 3 pontos para o time vitorioso e 1 ponto para ambos os times em caso de empate.
Deve-se computar o saldo de gols do time a cada jogo realizado.
A estrutura de execu��o se dar� por rodadas, cada time executar� um jogo por rodada. Ao final das 38 rodadas teremos o resultado final.
Consequentemente, cada plantel deve ter membros de cada uma das entidades.

O time deve ter o status que diz se ele est� apto ou n�o para a participa��o na liga. A liga deve ter o status se est� apta para iniciar ou n�o. Essa informa��o deve estar exposta na tela inicial. Para eles estarem aptos ele deve ter:

Todas as entidades devem estar com os dados preenchidos
Cada time deve ter no m�nimo 30 jogadores inscritos.
Comiss�o t�cnica com no m�nimo 5 profissionais e n�o se pode ter sobreposi��o dos cargos, i.e., s� pode ter um t�cnico um auxiliar etc.
O campeonato deve ter 20 times, n�o podendo ter nem mais nem menos.
Ao final da execu��o do campeonato deve ter o resultado do torneio.
Deve-se ter p�ginas para apresentar cada uma das funcionalidades da solu��o.
Em termos tecnol�gicos a aplica��o ao final do projeto dever� haver migrations. As telas dever�o ter um css melhorado do que a experi�ncia padr�o do Razor.

O sistema dever� ter algumas pesquisas:   

Jogador: nome, pela posi��o e p� preferido.
Comiss�o t�cnica: nome e cargo
Tabela: Jogos e est�dio
 


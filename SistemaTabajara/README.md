# Objetivo do Projeto

Desenvolver uma aplicação web utilizando .NET Framework, SQL Server e Migrations para gerenciar a Liga Tabajara de Futebol. O sistema deverá permitir o cadastro e gerenciamento de times, jogadores, comissão técnica, partidas, estatísticas e classificação geral do campeonato, obedecendo regras específicas de funcionamento da liga.

Desta forma, você deve ter uma página inicial que contenha a apresentação de sua liga e liste todos os times que fazem parte da liga. Cada time deve ter sua página que apresente suas características.

 

Desta forma, o sistema web deve permitir o cadastro dos:

Time
Jogadores
Comissão técnica
Partidas
Estatísticas dos jogos
Dessa forma, os times pertencem a liga e os jogadores e a comissão técnica estarão vinculados com um time. As partidas terão seu agendamento marcado, e as estatísticas dos jogadores estarão vinculadas a cada uma das partidas.

Detalhadamente, teremos:

Time
nome
cidade
estado
ano fundação
estádio
capacidade do estádio
Cores do Uniforme (primaria e secundária)
status

Jogador
nome
data de nascimento
nacionalidade
posição (enum: goleiro, zagueiro, volante, meia, atacante etc)
número da camisa
altura
peso
pé preferido (enum: esquerdo, direito, ambidestro)
Time

Comissão Técnica
nome
cargo (enum: treinador, auxiliar, preparador físico, fisiologista, treinador de goleiros e fisioterapeuta)
data nascimento
Time
Tabela

Lista dos jogos todos contra todos, com um jogo em casa e outro fora.
O registro dos resultados dos jogos.
Quando registrar o resultado se houver gols, deve-se informar qual do jogador fez cada um dos gols.
Deve-se ter a lista dos artilheiros do campeonato
Cada jogo deve gerar um dos resultados (vitória e/ou empate).
Para vitória serão computados 3 pontos para o time vitorioso e 1 ponto para ambos os times em caso de empate.
Deve-se computar o saldo de gols do time a cada jogo realizado.
A estrutura de execução se dará por rodadas, cada time executará um jogo por rodada. Ao final das 38 rodadas teremos o resultado final.
Consequentemente, cada plantel deve ter membros de cada uma das entidades.

O time deve ter o status que diz se ele está apto ou não para a participação na liga. A liga deve ter o status se está apta para iniciar ou não. Essa informação deve estar exposta na tela inicial. Para eles estarem aptos ele deve ter:

Todas as entidades devem estar com os dados preenchidos
Cada time deve ter no mínimo 30 jogadores inscritos.
Comissão técnica com no mínimo 5 profissionais e não se pode ter sobreposição dos cargos, i.e., só pode ter um técnico um auxiliar etc.
O campeonato deve ter 20 times, não podendo ter nem mais nem menos.
Ao final da execução do campeonato deve ter o resultado do torneio.
Deve-se ter páginas para apresentar cada uma das funcionalidades da solução.
Em termos tecnológicos a aplicação ao final do projeto deverá haver migrations. As telas deverão ter um css melhorado do que a experiência padrão do Razor.

O sistema deverá ter algumas pesquisas:   

Jogador: nome, pela posição e pé preferido.
Comissão técnica: nome e cargo
Tabela: Jogos e estádio
 


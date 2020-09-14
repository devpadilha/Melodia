create table jogador (
    id integer primary key,
    sexo varchar(20),
    data_nascimento date
);

create table login(
    id integer primary key,
    usuario varchar(20),
    senha varchar(20),
    jogador_id integer,
    foreign key (jogador_id) references jogador(id)
);

create table dificuldade(
    id integer primary key,
    nome varchar(20),
    descricao varchar(140)
);

create table nivel(
    id integer primary key,
    nome varchar(20),
    descricao varchar(140),
    dificuldade_id integer,
    foreign key (dificuldade_id) references dificuldade(id)
);

create table partida(
    id integer primary key,
    acertos integer,
    erros integer,
    jogador_id integer,
    nivel_id integer,
    foreign key (jogador_id) references  jogador(id),
    foreign key (nivel_id) references nivel(id)
);

create table elemento(
    id integer primary key,
    nome varchar(20),
    descricao varchar(140),
    resource varchar(10),
    nivel_id integer,
    foreign key (nivel_id) references nivel(id)
);

create table desafio(
    id integer primary key,
    descricao varchar(280),
    pergunta_elemento_id integer,
    resposta_elemento_id integer,
    foreign key (pergunta_elemento_id) references elemento(id),
    foreign key (resposta_elemento_id) references elemento(id)
);






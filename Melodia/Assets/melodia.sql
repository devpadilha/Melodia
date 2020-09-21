create table jogador (
    id integer primary key,
    sexo varchar(20),
    data_nascimento date
);

create table login(
    id integer primary key,
    usuario varchar(20),
    senha varchar(20),
    ativo boolean default false,
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
    concluido boolean default false,
    data real,
    jogador_id integer,
    nivel_id integer,
    foreign key (jogador_id) references  jogador(id),
    foreign key (nivel_id) references nivel(id)
);

create table elemento(
    id integer primary key,
    nome varchar(20),
    descricao varchar(140),
    resource varchar(10)
);

create table desafio(
    id integer primary key,
    descricao varchar(280),
    pergunta_elemento_id integer,
    resposta_elemento_id integer,
    nivel_id integer,
    foreign key (pergunta_elemento_id) references elemento(id),
    foreign key (resposta_elemento_id) references elemento(id),
    foreign key (nivel_id) references nivel(id)
);

create table partida_desafio(
  partida_id integer,
  desafio_id integer,
  primary key (partida_id, desafio_id),
  foreign key (partida_id) references partida(id),
  foreign key (desafio_id) references desafio(id)
);

insert into dificuldade (nome, descricao) values ('FÁCIL', '');
insert into dificuldade (nome, descricao) values ('MÉDIO', '');
insert into dificuldade (nome, descricao) values ('DIFÍCIL', '');

insert into nivel (nome, descricao, dificuldade_id) values ('NIVEL1', 'Nível 1 - Assimilação', 1);
insert into nivel (nome, descricao, dificuldade_id) values ('NIVEL1', 'Nível 1 - Assimilação', 2);
insert into nivel (nome, descricao, dificuldade_id) values ('NIVEL1', 'Nível 1 - Assimilação', 3);

insert into elemento ( nome, descricao, resource) values('CLAVE DE SOL', '', '0');
insert into elemento ( nome, descricao, resource) values('CLAVE DE SOL', '', '1');
insert into elemento ( nome, descricao, resource) values('CLAVE DE FA', '', '2');
insert into elemento ( nome, descricao, resource) values('CLAVE DE FA', '', '3');

insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('', 1, 2, 1);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('', 3, 4, 1);






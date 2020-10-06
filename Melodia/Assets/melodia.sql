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
    max_erros integer,
    dificuldade_id integer,
    foreign key (dificuldade_id) references dificuldade(id)
);

create table partida(
    id integer primary key,
    acertos integer,
    erros integer,
    data_inicio varchar(50),
    data_termino varchar(50),
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

create table questionario(
  id integer primary key,
  pergunta varchar(140),
  opcoes varchar(50),
  nivel_id integer,
  foreign key (nivel_id) references nivel(id)
);

create table questionario_resposta(
  id integer primary key,
  resposta varchar(10),
  pergunta_id integer,
  jogador_id integer,
  foreign key (pergunta_id) references  questionario(id),
  foreign key (jogador_id) references  jogador(id)
);

insert into questionario (pergunta, opcoes, nivel_id) values
('Você gosta de música?', 'Sim;Não;', 1),
('Você sabe o que é uma clave?', 'Sim;Não;', 1),
('A clave de sol e a clave de fá têm o mesmo desenho?', 'Sim;Não;', 1),
('Você sabe o que é uma mínima?', 'Sim;Não;', 1),
('Você sabe o que é uma semínima?', 'Sim;Não;', 1),
('Você sabe a diferença entre harmonia e melodia?', 'Sim;Não;', 1);

insert into questionario (pergunta, opcoes, nivel_id) values
('Você sabe a diferença entre mínima e semínima?', 'Sim;Não;', 4),
('Você sabe a diferença entre colcheia e semicolcheia?', 'Sim;Não;', 4),
('Você conhece o formato da semibreve?', 'Sim;Não;', 4),
('Você sabe a diferença entre a clave de sol e a clave de fá?', 'Sim;Não;', 4),
('Harmonia e melodia são iguais?', 'Sim;Não;', 4);

insert into dificuldade (nome, descricao) values ('FÁCIL', '');
insert into dificuldade (nome, descricao) values ('MÉDIO', '');
insert into dificuldade (nome, descricao) values ('DIFÍCIL', '');

insert into nivel (nome, descricao, dificuldade_id) values ('NIVEL1', 'Nível 1 - Assimilação', 1);
insert into nivel (nome, descricao, dificuldade_id) values ('NIVEL1', 'Nível 1 - Assimilação', 2);
insert into nivel (nome, descricao, dificuldade_id) values ('NIVEL1', 'Nível 1 - Assimilação', 3);

insert into nivel (nome, descricao, dificuldade_id) values ('NIVEL2', 'Nível 2 - Quebra-Cabeça', 1);
insert into nivel (nome, descricao, dificuldade_id) values ('NIVEL2', 'Nível 2 - Quebra-Cabeça', 2);
insert into nivel (nome, descricao, dificuldade_id) values ('NIVEL2', 'Nível 2 - Quebra-Cabeça', 3);

insert into elemento ( nome, descricao, resource) values('CLAVE DE SOL', 'nivel 1', '0');
insert into elemento ( nome, descricao, resource) values('CLAVE DE SOL', 'nivel 1', '1');
insert into elemento ( nome, descricao, resource) values('CLAVE DE FA', 'nivel 1', '2');
insert into elemento ( nome, descricao, resource) values('CLAVE DE FA', 'nivel 1', '3');

insert into elemento ( nome, descricao, resource) values('CLAVE DE SOL', 'nivel 2', '1');
insert into elemento ( nome, descricao, resource) values('CLAVE DE SOL', 'nivel 2', '2');
insert into elemento ( nome, descricao, resource) values('CLAVE DE FA', 'nivel 2', '3');
insert into elemento ( nome, descricao, resource) values('CLAVE DE FA', 'nivel 2', '4');

insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('', 1, 2, 1);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('', 3, 4, 1);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('', 1, 2, 2);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('', 3, 4, 2);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('', 1, 2, 3);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('', 3, 4, 3);

insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('', 5, 6, 4);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('', 7, 8, 4);







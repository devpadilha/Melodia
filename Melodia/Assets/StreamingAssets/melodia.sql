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
    qtde_desafios integer,
    max_erros integer,
    min_acertos integer,
    dificuldade_id integer,
    foreign key (dificuldade_id) references dificuldade(id)
);

create table partida(
    id integer primary key,
    acertos integer,
    erros integer,
    concluido boolean DEFAULT false,
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
('1. Você gosta de música?', 'Sim;Não;', 1),
('2. Você sabe o que é uma clave?', 'Sim;Não;', 1),
('3. A clave de sol e a clave de fá têm o mesmo desenho?', 'Sim;Não;', 1),
('4. Você sabe o que é uma mínima?', 'Sim;Não;', 1),
('5. Você sabe o que é uma semínima?', 'Sim;Não;', 1),
('6. Você sabe a diferença entre harmonia e melodia?', 'Sim;Não;', 1);

insert into questionario (pergunta, opcoes, nivel_id) values
('1. Você sabe a diferença entre mínima e semínima?', 'Sim;Não;', 4),
('2. Você sabe a diferença entre colcheia e semicolcheia?', 'Sim;Não;', 4),
('3. Você conhece o formato da semibreve?', 'Sim;Não;', 4),
('4. Você sabe a diferença entre a clave de sol e a clave de fá?', 'Sim;Não;', 4),
('5. Harmonia e melodia são iguais?', 'Sim;Não;', 4);

insert into questionario (pergunta, opcoes, nivel_id) values
('1. A mínima e semínima têm o mesmo formato?', 'Sim;Não;', 7),
('2. Você sabe a diferença entre semibreve e semínima?', 'Sim;Não;', 7),
('3. Você sabe quantas linhas tem a pauta?', 'Sim;Não;', 7),
('4. Duas colcheias são iguais a duas semínimas?', 'Sim;Não;', 7);

insert into questionario (pergunta, opcoes, nivel_id) values
('1. A escala musical é um conjunto de notas musicais?', 'Sim;Não;', 10),
('2. A escala musical é formada por 7 notas?', 'Sim;Não;', 10),
('3. Fi é uma nota musical?', 'Sim;Não;', 10),
('4. Ré é uma nota musical?', 'Sim;Não;', 10);

insert into questionario (pergunta, opcoes, nivel_id) values
('1. As linhas da pauta são contadas na ordem: da última para a primeira?', 'Sim;Não;', 13),
('2. Toda a pauta tem 5 linhas?', 'Sim;Não;', 14),
('3. A clave de sol deve ser posicionada na segunda linha?', 'Sim;Não;', 13),
('4. A clave de fá deve ser posicionada na quarta linha?', 'Sim;Não;', 13),
('5. Podemos colocar mais de uma nota na mesma pauta?', 'Sim;Não;', 13),
('6. Podemos colocar mais de uma clave na mesma pauta?', 'Sim;Não;', 13);

insert into questionario (pergunta, opcoes, nivel_id) values
('1. A semínima vale 1 tempo?', 'Sim;Não;', 16),
('2. A mínima vale 3 tempos?', 'Sim;Não;', 16),
('3. 2 semínimas valem o mesmo tempo que 1 mínima?', 'Sim;Não;', 16),
('4. 1 colcheia vale o mesmo tempo que 1 semibreve?', 'Sim;Não;', 16),
('5. 1 semibreve vale 4 tempos?', 'Sim;Não;', 16),
('6. 1 semínima vale o mesmo tempo que 2 colcheias?', 'Sim;Não;', 16);

insert into questionario (pergunta, opcoes, nivel_id) values
('1. Você sabe em qual linha a clave de sol deve ser posicionada?', 'Sim;Não;', 19),
('2. Podemos colocar todas as notas da escala musical na pauta?', 'Sim;Não;', 19),
('3. Podemos criar uma melodia e colocar na pauta?', 'Sim;Não;', 19),
('4. Podemos criar uma harmonia e colocar na pauta?', 'Sim;Não;', 19),
('5. Uma harmonia pode ter 4 tempos?', 'Sim;Não;', 19),
('6. Para formar uma melodia precisamos usar notas de clave e sol ou de fá?', 'Sim;Não;', 19);

insert into dificuldade (nome, descricao) values ('FÁCIL', '');
insert into dificuldade (nome, descricao) values ('MÉDIO', '');
insert into dificuldade (nome, descricao) values ('DIFÍCIL', '');

insert into nivel (nome, descricao, max_erros, min_acertos, dificuldade_id, qtde_desafios ) values ('NIVEL1', 'Nível 1 - Assimilação', 3, 4, 2, 5);
insert into nivel (nome, descricao, max_erros, min_acertos, dificuldade_id, qtde_desafios) values ('NIVEL1', 'Nível 1 - Assimilação', 3, 3, 1, 5);
insert into nivel (nome, descricao, max_erros, min_acertos, dificuldade_id, qtde_desafios) values ('NIVEL1', 'Nível 1 - Assimilação', 3, 5, 3, 5);

insert into nivel (nome, descricao, max_erros, min_acertos, dificuldade_id, qtde_desafios) values ('NIVEL2', 'Nível 2 - Jogo da Memória', 3, 2, 1, 2);
insert into nivel (nome, descricao, max_erros, min_acertos, dificuldade_id, qtde_desafios) values ('NIVEL2', 'Nível 2 - Jogo da Memória', 3, 3, 2, 3);
insert into nivel (nome, descricao, max_erros, min_acertos, dificuldade_id, qtde_desafios) values ('NIVEL2', 'Nível 2 - Jogo da Memória', 3, 4, 3, 4);

insert into nivel (nome, descricao, max_erros, min_acertos, dificuldade_id, qtde_desafios) values ('NIVEL3', 'Nível 3 - TAC', 3, 2, 1, 2);
insert into nivel (nome, descricao, max_erros, min_acertos, dificuldade_id, qtde_desafios) values ('NIVEL3', 'Nível 3 - TAC', 3, 3, 2, 3);
insert into nivel (nome, descricao, max_erros, min_acertos, dificuldade_id, qtde_desafios) values ('NIVEL3', 'Nível 3 - TAC', 3, 4, 3, 4);

insert into nivel (nome, descricao, max_erros, min_acertos, dificuldade_id, qtde_desafios) values ('NIVEL4', 'Nível 4 - ESCALA MUSICAL', 3, 3, 1, 5);
insert into nivel (nome, descricao, max_erros, min_acertos, dificuldade_id, qtde_desafios) values ('NIVEL4', 'Nível 4 - ESCALA MUSICAL', 3, 4, 2, 5);
insert into nivel (nome, descricao, max_erros, min_acertos, dificuldade_id, qtde_desafios) values ('NIVEL4', 'Nível 4 - ESCALA MUSICAL', 3, 5, 3, 5);

insert into nivel (nome, descricao, max_erros, min_acertos, dificuldade_id, qtde_desafios) values ('NIVEL5', 'Nível 5 - PAUTA MUSICAL', 3, 3, 1, 5);
insert into nivel (nome, descricao, max_erros, min_acertos, dificuldade_id, qtde_desafios) values ('NIVEL5', 'Nível 5 - PAUTA MUSICAL', 3, 4, 2, 5);
insert into nivel (nome, descricao, max_erros, min_acertos, dificuldade_id, qtde_desafios) values ('NIVEL5', 'Nível 5 - PAUTA MUSICAL', 3, 5, 3, 5);

insert into nivel (nome, descricao, max_erros, min_acertos, dificuldade_id, qtde_desafios) values ('NIVEL6', 'Nível 6 - TEMPO MUSICAL', 2, 1, 1, 5);
insert into nivel (nome, descricao, max_erros, min_acertos, dificuldade_id, qtde_desafios) values ('NIVEL6', 'Nível 6 - TEMPO MUSICAL', 2, 1, 2, 5);
insert into nivel (nome, descricao, max_erros, min_acertos, dificuldade_id, qtde_desafios) values ('NIVEL6', 'Nível 6 - TEMPO MUSICAL', 1, 2, 3, 5);

insert into nivel (nome, descricao, max_erros, min_acertos, dificuldade_id, qtde_desafios) values ('NIVEL7', 'Nível 7 - TEMPO MUSICAL', 2, 2, 1, 2);
insert into nivel (nome, descricao, max_erros, min_acertos, dificuldade_id, qtde_desafios) values ('NIVEL7', 'Nível 7 - TEMPO MUSICAL', 2, 3, 2, 3);
insert into nivel (nome, descricao, max_erros, min_acertos, dificuldade_id, qtde_desafios) values ('NIVEL7', 'Nível 7 - TEMPO MUSICAL', 1, 4, 3, 4);

insert into elemento ( nome, descricao, resource) values('CLAVE DE SOL', 'nivel 1', '02');
insert into elemento ( nome, descricao, resource) values('CLAVE DE SOL', 'nivel 1', '03');
insert into elemento ( nome, descricao, resource) values('CLAVE DE FA', 'nivel 1', '04');
insert into elemento ( nome, descricao, resource) values('CLAVE DE FA', 'nivel 1', '05');
insert into elemento ( nome, descricao, resource) values('CLAVE DE SOL', 'nivel 2', '01');
insert into elemento ( nome, descricao, resource) values('CLAVE DE SOL', 'nivel 2', '02');
insert into elemento ( nome, descricao, resource) values('CLAVE DE FA', 'nivel 2', '03');
insert into elemento ( nome, descricao, resource) values('CLAVE DE FA', 'nivel 2', '04');
insert into elemento ( nome, descricao, resource) values('CLAVE DE DO', 'nivel 1', '06');
insert into elemento ( nome, descricao, resource) values('CLAVE DE DO', 'nivel 1', '13');
insert into elemento ( nome, descricao, resource) values('COLCHEIA', 'nivel 1', '07');
insert into elemento ( nome, descricao, resource) values('COLCHEIA', 'nivel 1', '14');
insert into elemento ( nome, descricao, resource) values('HARMONIA', 'nivel 1', '08');
insert into elemento ( nome, descricao, resource) values('HARMONIA', 'nivel 1', '15');
insert into elemento ( nome, descricao, resource) values('MELODIA', 'nivel 1', '09');
insert into elemento ( nome, descricao, resource) values('MELODIA', 'nivel 1', '16');
insert into elemento ( nome, descricao, resource) values('MINIMA', 'nivel 1', '10');
insert into elemento ( nome, descricao, resource) values('MINIMA', 'nivel 1', '17');
insert into elemento ( nome, descricao, resource) values('SEMINIMA', 'nivel 1', '11');
insert into elemento ( nome, descricao, resource) values('SEMINIMA', 'nivel 1', '18');
insert into elemento ( nome, descricao, resource) values('PAUTA', 'nivel 1', '12');
insert into elemento ( nome, descricao, resource) values('PAUTA', 'nivel 1', '19');
insert into elemento ( nome, descricao, resource) values('CLAVE DE DO', 'nivel 2', '05');
insert into elemento ( nome, descricao, resource) values('CLAVE DE DO', 'nivel 2', '06');
insert into elemento ( nome, descricao, resource) values('COLCHEIA', 'nivel 2', '07');
insert into elemento ( nome, descricao, resource) values('COLCHEIA', 'nivel 2', '08');
insert into elemento ( nome, descricao, resource) values('HARMONIA', 'nivel 2', '09');
insert into elemento ( nome, descricao, resource) values('HARMONIA', 'nivel 2', '10');
insert into elemento ( nome, descricao, resource) values('MELODIA', 'nivel 2', '11');
insert into elemento ( nome, descricao, resource) values('MELODIA', 'nivel 2', '12');
insert into elemento ( nome, descricao, resource) values('MINIMA', 'nivel 2', '13');
insert into elemento ( nome, descricao, resource) values('MINIMA', 'nivel 2', '14');
insert into elemento ( nome, descricao, resource) values('SEMINIMA', 'nivel 2', '15');
insert into elemento ( nome, descricao, resource) values('SEMINIMA', 'nivel 2', '16');
insert into elemento ( nome, descricao, resource) values('PAUTA', 'nivel 2', '17');
insert into elemento ( nome, descricao, resource) values('PAUTA', 'nivel 2', '18');
insert into elemento ( nome, descricao, resource) values('CLAVE DE SOL', 'nivel 3', '00');
insert into elemento ( nome, descricao, resource) values('CLAVE DE FÁ', 'nivel 3', '01');
insert into elemento ( nome, descricao, resource) values('CLAVE DE DÓ', 'nivel 3', '02');
insert into elemento ( nome, descricao, resource) values('COLCHEIA', 'nivel 3', '03');
insert into elemento ( nome, descricao, resource) values('HARMONIA', 'nivel 3', '04');
insert into elemento ( nome, descricao, resource) values('MELODIA', 'nivel 3', '05');
insert into elemento ( nome, descricao, resource) values('MINIMA', 'nivel 3', '06');
insert into elemento ( nome, descricao, resource) values('SEMINIMA', 'nivel 3', '07');
insert into elemento ( nome, descricao, resource) values('PAUTA', 'nivel 3', '08');
insert into elemento ( nome, descricao, resource) values('ESCALA DÓ', 'nivel 4', '01');
insert into elemento ( nome, descricao, resource) values('ESCALA RÉ', 'nivel 4', '02');
insert into elemento ( nome, descricao, resource) values('ESCALA MI', 'nivel 4', '03');
insert into elemento ( nome, descricao, resource) values('ESCALA FÁ', 'nivel 4', '04');
insert into elemento ( nome, descricao, resource) values('ESCALA SOL', 'nivel 4', '05');
insert into elemento ( nome, descricao, resource) values('ESCALA LÁ', 'nivel 4', '06');
insert into elemento ( nome, descricao, resource) values('ESCALA SI', 'nivel 4', '07');
insert into elemento ( nome, descricao, resource) values('DÓ', 'nivel 4', '08');
insert into elemento ( nome, descricao, resource) values('RÉ', 'nivel 4', '09');
insert into elemento ( nome, descricao, resource) values('MI', 'nivel 4', '10');
insert into elemento ( nome, descricao, resource) values('FÁ', 'nivel 4', '11');
insert into elemento ( nome, descricao, resource) values('SOL', 'nivel 4', '12');
insert into elemento ( nome, descricao, resource) values('LÁ', 'nivel 4', '13');
insert into elemento ( nome, descricao, resource) values('SI', 'nivel 4', '14');
insert into elemento ( nome, descricao, resource) values('PAUTA CLAVE DE SOL', 'nivel 5', '04');
insert into elemento ( nome, descricao, resource) values('PAUTA CLAVE DE DÓ', 'nivel 5', '05');
insert into elemento ( nome, descricao, resource) values('PAUTA CLAVE DE FÁ', 'nivel 5', '06');
insert into elemento ( nome, descricao, resource) values('01', 'nivel 5 clave de sol', '08');
insert into elemento ( nome, descricao, resource) values('02', 'nivel 5 clave de sol', '09');
insert into elemento ( nome, descricao, resource) values('03', 'nivel 5 clave de sol', '10');
insert into elemento ( nome, descricao, resource) values('04', 'nivel 5 clave de sol', '11');
insert into elemento ( nome, descricao, resource) values('05', 'nivel 5 clave de sol', '12');
insert into elemento ( nome, descricao, resource) values('06', 'nivel 5 clave de sol', '13');
insert into elemento ( nome, descricao, resource) values('07', 'nivel 5 clave de sol', '14');
insert into elemento ( nome, descricao, resource) values('01', 'nivel 5 clave de dó', '09');
insert into elemento ( nome, descricao, resource) values('02', 'nivel 5 clave de dó', '10');
insert into elemento ( nome, descricao, resource) values('03', 'nivel 5 clave de dó', '11');
insert into elemento ( nome, descricao, resource) values('04', 'nivel 5 clave de dó', '12');
insert into elemento ( nome, descricao, resource) values('05', 'nivel 5 clave de dó', '13');
insert into elemento ( nome, descricao, resource) values('06', 'nivel 5 clave de dó', '14');
insert into elemento ( nome, descricao, resource) values('07', 'nivel 5 clave de dó', '08');
insert into elemento ( nome, descricao, resource) values('01', 'nivel 5 clave de fá', '10');
insert into elemento ( nome, descricao, resource) values('02', 'nivel 5 clave de fá', '11');
insert into elemento ( nome, descricao, resource) values('03', 'nivel 5 clave de fá', '12');
insert into elemento ( nome, descricao, resource) values('04', 'nivel 5 clave de fá', '13');
insert into elemento ( nome, descricao, resource) values('05', 'nivel 5 clave de fá', '14');
insert into elemento ( nome, descricao, resource) values('06', 'nivel 5 clave de fá', '08');
insert into elemento ( nome, descricao, resource) values('07', 'nivel 5 clave de fá', '09');
insert into elemento ( nome, descricao, resource) values('SEMÍNIMA', 'nivel 6', '04');
insert into elemento ( nome, descricao, resource) values('MÍNIMA', 'nivel 6', '05');
insert into elemento ( nome, descricao, resource) values('SEMIBREVE', 'nivel 6', '06');
insert into elemento ( nome, descricao, resource) values('PERGUNTA SEMÍNIMA', 'nivel 6', '07');
insert into elemento ( nome, descricao, resource) values('PERGUNTA MÍNIMA', 'nivel 6', '08');
insert into elemento ( nome, descricao, resource) values('COLCHEIA', 'nivel 2', '00');
insert into elemento ( nome, descricao, resource) values('SEMINIMA', 'nivel 3', '01');
insert into elemento ( nome, descricao, resource) values('MINIMA', 'nivel 4', '02');
insert into elemento ( nome, descricao, resource) values('SEMIBREVE', 'nivel 5', '03');

insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE SOL', 1, 2, 1);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE FA', 3, 4, 1);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE SOL', 1, 2, 2);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE FA', 3, 4, 2);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE SOL', 1, 2, 3);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE FA', 3, 4, 3);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE DO', 9, 10, 1);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('COLCHEIA', 11, 12, 1);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('HARMONIA', 13, 14, 1);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('MELODIA', 15, 16, 1);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('MINIMA', 17, 18, 1);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('SEMINIMA', 19, 20, 1);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('PAUTA', 21, 22, 1);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE DO', 9, 10, 2);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('COLCHEIA', 11, 12, 2);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('HARMONIA', 13, 14, 2);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('MELODIA', 15, 16, 2);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('MINIMA', 17, 18, 2);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('SEMINIMA', 19, 20, 2);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('PAUTA', 21, 22, 2);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE DO', 9, 10, 3);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('COLCHEIA', 11, 12, 3);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('HARMONIA', 13, 14, 3);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('MELODIA', 15, 16, 3);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('MINIMA', 17, 18, 3);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('SEMINIMA', 19, 20, 3);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('PAUTA', 21, 22, 3);

insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE SOL', 5, 6, 4);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE FA', 7, 8, 4);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE DO', 23, 24, 4);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('COLCHEIA', 25, 26, 4);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('HARMONIA', 27, 28, 4);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('MELODIA', 29, 30, 4);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('MINIMA', 31, 32, 4);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('SEMINIMA', 33, 34, 4);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('PAUTA', 35, 36, 4);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE SOL', 5, 6, 5);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE FA', 7, 8, 5);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE DO', 23, 24, 5);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('COLCHEIA', 25, 26, 5);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('HARMONIA', 27, 28, 5);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('MELODIA', 29, 30, 5);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('MINIMA', 31, 32, 5);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('SEMINIMA', 33, 34, 5);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('PAUTA', 35, 36, 5);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE SOL', 5, 6, 6);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE FA', 7, 8, 6);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE DO', 23, 24, 6);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('COLCHEIA', 25, 26, 6);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('HARMONIA', 27, 28, 6);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('MELODIA', 29, 30, 6);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('MINIMA', 31, 32, 6);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('SEMINIMA', 33, 34, 6);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('PAUTA', 35, 36, 6);

insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('Selecione a CLAVE DE SOL', null, 37, 7);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('Selecione a CLAVE DE FÁ', null, 38, 7);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('Selecione a CLAVE DE DÓ', null, 39, 7);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('Selecione a COLCHEIA', null, 40, 7);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('Selecione a HARMONIA', null, 41, 7);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('Selecione a MELODIA', null, 42, 7);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('Selecione a MÍNIMA', null, 43, 7);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('Selecione a SEMÍNIMA', null, 44, 7);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('Selecione a PAUTA', null, 45, 7);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('Selecione a CLAVE DE SOL', null, 37, 8);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('Selecione a CLAVE DE FÁ', null, 38, 8);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('Selecione a CLAVE DE DÓ', null, 39, 8);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('Selecione a COLCHEIA', null, 40, 8);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('Selecione a HARMONIA', null, 41, 8);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('Selecione a MELODIA', null, 42, 8);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('Selecione a MÍNIMA', null, 43, 8);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('Selecione a SEMÍNIMA', null, 44, 8);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('Selecione a PAUTA', null, 45, 8);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('Selecione a CLAVE DE SOL', null, 37, 9);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('Selecione a CLAVE DE FÁ', null, 38, 9);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('Selecione a CLAVE DE DÓ', null, 39, 9);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('Selecione a COLCHEIA', null, 40, 9);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('Selecione a HARMONIA', null, 41, 9);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('Selecione a MELODIA', null, 42, 9);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('Selecione a MÍNIMA', null, 43, 9);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('Selecione a SEMÍNIMA', null, 44, 9);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('Selecione a PAUTA', null, 45, 9);

insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('DÓ', 46, 53, 10);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('RÉ', 47, 54, 10);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('MI', 48, 55, 10);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('FÁ', 49, 56, 10);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('SOL', 50, 57, 10);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('LÁ', 51, 58, 10);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('SI', 52, 59, 10);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('DÓ', 46, 53, 11);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('RÉ', 47, 54, 11);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('MI', 48, 55, 11);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('FÁ', 49, 56, 11);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('SOL', 50, 57, 11);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('LÁ', 51, 58, 11);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('SI', 52, 59, 11);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('DÓ', 46, 53, 12);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('RÉ', 47, 54, 12);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('MI', 48, 55, 12);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('FÁ', 49, 56, 12);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('SOL', 50, 57, 12);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('LÁ', 51, 58, 12);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('SI', 52, 59, 12);


insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE SOL - DÓ', 60, 63, 13);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE SOL - RÉ', 60, 64, 13);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE SOL - MI', 60, 65, 13);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE SOL - FÁ', 60, 66, 13);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE SOL - SOL', 60, 67, 13);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE SOL - LÁ', 60, 68, 13);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE SOL - SI', 60, 69, 13);

insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE SOL - DÓ', 60, 63, 14);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE SOL - RÉ', 60, 64, 14);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE SOL - MI', 60, 65, 14);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE SOL - FÁ', 60, 66, 14);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE SOL - SOL', 60, 67, 14);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE SOL - LÁ', 60, 68, 14);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE SOL - SI', 60, 69, 14);

insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE SOL - DÓ', 60, 63, 15);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE SOL - RÉ', 60, 64, 15);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE SOL - MI', 60, 65, 15);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE SOL - FÁ', 60, 66, 15);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE SOL - SOL', 60, 67, 15);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE SOL - LÁ', 60, 68, 15);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE SOL - SI', 60, 69, 15);

insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE DÓ - RÉ', 61, 70, 13);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE DÓ - MI', 61, 71, 13);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE DÓ - FÁ', 61, 72, 13);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE DÓ - SOL', 61, 73, 13);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE DÓ - LÁ', 61, 74, 13);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE DÓ - SI', 61, 75, 13);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE DÓ - DÓ', 61, 76, 13);

insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE DÓ - RÉ', 61, 70, 14);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE DÓ - MI', 61, 71, 14);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE DÓ - FÁ', 61, 72, 14);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE DÓ - SOL', 61, 73, 14);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE DÓ - LÁ', 61, 74, 14);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE DÓ - SI', 61, 75, 14);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE DÓ - DÓ', 61, 76, 14);

insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE DÓ - RÉ', 61, 70, 15);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE DÓ - MI', 61, 71, 15);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE DÓ - FÁ', 61, 72, 15);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE DÓ - SOL', 61, 73, 15);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE DÓ - LÁ', 61, 74, 15);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE DÓ - SI', 61, 75, 15);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('CLAVE DE DÓ - DÓ', 61, 76, 15);

insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('SEMÍNIMA', 87, 85, 16);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('MÍNIMA', 88, 86, 16);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('SEMÍNIMA', 87, 85, 17);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('MÍNIMA', 88, 86, 17);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('SEMÍNIMA', 87, 85, 18);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('MÍNIMA', 88, 86, 18);

insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('Selecione as notas de 1 tempo', null, 90, 19);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('Selecione as notas de 2 tempos', null, 91, 19);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('selecione as notas de 4 tempos', null, 92, 19);

insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('Selecione as notas de 1 tempo', null, 90, 20);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('Selecione as notas de 2 tempos', null, 91, 20);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('selecione as notas de 4 tempos', null, 92, 20);

insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('Selecione as notas de 1 tempo', null, 90, 21);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('Selecione as notas de 2 tempos', null, 91, 21);
insert into desafio (descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id) values ('selecione as notas de 4 tempos', null, 92, 21);
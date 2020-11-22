using Mono.Data.Sqlite;
using System.Data;
using System;
using System.Collections.Generic;

public class DificuldadeModel
{
    private DataBase dataBase;

    public DificuldadeModel()
    {
        dataBase = new DataBase();
    }

    public Dificuldade get(int id)
    {
        Dificuldade dificuldade = null;

        string query = "SELECT id, nome, descricao FROM dificuldade WHERE id = @id";
        var param = new Dictionary<string, string>();
        param.Add("id", id.ToString());
        Dictionary<int, List<string>> retornos = dataBase.Select(query, param);
        if (retornos.Count > 0)
        {
            List<string> retorno = retornos[0];

            dificuldade = new Dificuldade();
            dificuldade.Id = Int32.Parse(retorno[0]);
            dificuldade.Nome = retorno[1];
            dificuldade.Descricao = retorno[2];
        }

        return dificuldade;
    }

    public Dificuldade obterDificuldadeJogador(Jogador jogador)
    {
        Partida partida = null;
        JogadorController jogadorController = new JogadorController();
        NivelController nivel = new NivelController();
        DesafioController desafioController = new DesafioController();
        DificuldadeController dificuldadeController = new DificuldadeController();
        string query = "SELECT p.id, p.acertos, p.erros, p.data_inicio, p.data_termino, p.concluido, p.jogador_id, p.nivel_id FROM partida p INNER JOIN nivel n ON n.id = p.nivel_id WHERE jogador_id = @jogador AND concluido = 1 AND data_termino is not null ORDER BY n.dificuldade_id DESC ";
        var param = new Dictionary<string, string>();
        param.Add("jogador", jogador.Id.ToString());

        Dictionary<int, List<string>> retornos = dataBase.Select(query, param);
        if (retornos.Count > 0)
        {
            List<string> retorno = retornos[0];

            partida = new Partida();
            partida.Id = Int32.Parse(retorno[0]);
            partida.Acertos = Int32.Parse(retorno[1]);
            partida.Erros = Int32.Parse(retorno[2]);
            partida.DataInicio = DateTime.Parse(retorno[3]);
            if (retorno[4].Length > 0)
                partida.DataTermino = DateTime.Parse(retorno[4]);
            partida.Concluido = Boolean.Parse(retorno[5]);
            partida.Jogador = jogadorController.get(Int32.Parse(retorno[6]));
            partida.Nivel = nivel.get(Int32.Parse(retorno[7]));
            partida.Desafios = desafioController.getByPartida(partida);
        }
        else
        {
            return dificuldadeController.get((int)DificuldadeEnum.Dificuldade.FACIL);
        }

        bool fgConcluido = nivel.verificarNivelCompleto(partida);

        Dificuldade dificuldade = partida.Nivel.Dificuldade;
        if (fgConcluido && dificuldade.Id != (int)DificuldadeEnum.Dificuldade.DIFICIL)
        {
            return dificuldadeController.get(dificuldade.Id + 1);
        }
        else
        {
            return partida.Nivel.Dificuldade;
        }
    }
}

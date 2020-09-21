using Mono.Data.Sqlite;
using System.Data;
using System;
using System.Collections.Generic;

public class PartidaModel
{
    private DataBase dataBase;

    public PartidaModel()
    {
        dataBase = new DataBase();
    }

    public Partida get(int id)
    {
        Partida partida = null;
        JogadorController jogador = new JogadorController();
        NivelController nivel = new NivelController();

        string query = "SELECT id, acertos, erros, concluido, jogador_id, nivel_id FROM partida WHERE id = @id";
        var param = new Dictionary<string, string>();
        param.Add("id", id.ToString());
        Dictionary<int, List<string>> retornos = dataBase.Select(query, param);
        if (retornos.Count > 0)
        {
            List<string> retorno = retornos[0];

            partida = new Partida();
            partida.Id = Int32.Parse(retorno[0]);
            partida.Acertos = Int32.Parse(retorno[1]);
            partida.Erros = Int32.Parse(retorno[2]);
            partida.Concluido = Boolean.Parse(retorno[3]);
            partida.Jogador = jogador.get(Int32.Parse(retorno[4]));
            partida.Nivel = nivel.get(Int32.Parse(retorno[5]));
        }

        return partida;
    }

    public Partida getUltima(Jogador jogador)
    {
        Partida partida = null;
        JogadorController jogadorController = new JogadorController();
        NivelController nivelController = new NivelController();
        DesafioController desafioController = new DesafioController();

        string query = "SELECT id, acertos, erros, concluido, jogador_id, nivel_id FROM partida WHERE jogador_id = @id and concluido = @concluido ORDER BY nivel_id DESC, data DESC";
        var param = new Dictionary<string, string>();
        param.Add("id", jogador.Id.ToString());
        param.Add("concluido", "true");
        Dictionary<int, List<string>> retornos = dataBase.Select(query, param);
        if (retornos.Count > 0)
        {
            List<string> retorno = retornos[0];

            partida = new Partida();
            partida.Id = Int32.Parse(retorno[0]);
            partida.Acertos = Int32.Parse(retorno[1]);
            partida.Erros = Int32.Parse(retorno[2]);
            partida.Concluido = Boolean.Parse(retorno[3]);
            partida.Jogador = jogadorController.get(Int32.Parse(retorno[4]));
            partida.Nivel = nivelController.get(Int32.Parse(retorno[5]));
            partida.Desafios = desafioController.getByPartida(partida);
        }

        return partida;
    }

    public Partida criarPartida(Jogador jogador, Nivel nivel, int qtdeDesafios)
    {
        Partida partida = null;
        JogadorController jogadorController = new JogadorController();
        NivelController nivelController = new NivelController();
        DesafioController desafioController = new DesafioController();

        string query = "INSERT INTO partida (acertos, erros, jogador_id, nivel_id, concluido, data) VALUES ( @acertos, @erros, @jogador, @nivel, @concluido, julianday('now'))";
        var param = new Dictionary<string, string>();
        param.Add("acertos", "0");
        param.Add("erros", "0");
        param.Add("jogador", jogador.Id.ToString());
        param.Add("nivel", nivel.Id.ToString());
        param.Add("concluido", "false");
        int id = dataBase.Insert(query, param);

        partida = get(id);

        partida.Desafios = desafioController.criarDesafios(partida, qtdeDesafios);

        return partida;
    }
}

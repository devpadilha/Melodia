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
        DesafioController desafioController = new DesafioController();

        string query = "SELECT id, acertos, erros, data_inicio, data_termino, jogador_id, nivel_id FROM partida WHERE id = @id";
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
            partida.DataInicio = DateTime.Parse(retorno[3]);
            if(retorno[4].Length > 0)
                partida.DataTermino = DateTime.Parse(retorno[4]);
            partida.Jogador = jogador.get(Int32.Parse(retorno[5]));
            partida.Nivel = nivel.get(Int32.Parse(retorno[6]));
            partida.Desafios = desafioController.getByPartida(partida);
        }

        return partida;
    }

    public Partida getUltima(Jogador jogador)
    {
        Partida partida = null;
        JogadorController jogadorController = new JogadorController();
        NivelController nivelController = new NivelController();
        DesafioController desafioController = new DesafioController();

        string query = "SELECT id, acertos, erros, data_inicio, data_termino, jogador_id, nivel_id FROM partida WHERE jogador_id = @id and data_termino is not null ORDER BY nivel_id DESC, data_inicio DESC";
        var param = new Dictionary<string, string>();
        param.Add("id", jogador.Id.ToString());
    
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
            partida.Jogador = jogadorController.get(Int32.Parse(retorno[5]));
            partida.Nivel = nivelController.get(Int32.Parse(retorno[6]));
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

        DateTime agora = DateTime.Now;


        string query = "INSERT INTO partida (acertos, erros, jogador_id, nivel_id, data_inicio) VALUES ( @acertos, @erros, @jogador, @nivel, @dataInicio)";
        var param = new Dictionary<string, string>();
        param.Add("acertos", "0");
        param.Add("erros", "0");
        param.Add("jogador", jogador.Id.ToString());
        param.Add("nivel", nivel.Id.ToString());
        param.Add("dataInicio", dataBase.DateTimeSQLite(agora));
        int id = dataBase.Insert(query, param);

        partida = get(id);

        partida.Desafios = desafioController.criarDesafios(partida, qtdeDesafios);

        return partida;
    }

    public void encerrarPartida(Partida partida)
    {
        DesafioController desafioController = new DesafioController();

        DateTime agora = DateTime.Now;

        string query = "UPDATE partida SET acertos = @acertos, erros = @erros, data_termino = @dataTermino WHERE id = @id";
        var param = new Dictionary<string, string>();
        param.Add("id", partida.Id.ToString());
        param.Add("acertos", partida.Acertos.ToString());
        param.Add("erros", partida.Erros.ToString());
        param.Add("dataTermino", dataBase.DateTimeSQLite(agora));

        int id = dataBase.Insert(query, param);
    }
}

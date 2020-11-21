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

        string query = "SELECT id, acertos, erros, data_inicio, data_termino, concluido, jogador_id, nivel_id FROM partida WHERE id = @id";
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
            partida.Concluido = Boolean.Parse(retorno[5]);
            partida.Jogador = jogador.get(Int32.Parse(retorno[6]));
            partida.Nivel = nivel.get(Int32.Parse(retorno[7]));
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

        string query = "SELECT id, acertos, erros, data_inicio, data_termino, concluido, jogador_id, nivel_id FROM partida WHERE jogador_id = @id and data_termino is not null and concluido = @concluido ORDER BY nivel_id DESC, data_inicio DESC";
        var param = new Dictionary<string, string>();
        param.Add("id", jogador.Id.ToString());
        param.Add("concluido", "1");

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
            partida.Nivel = nivelController.get(Int32.Parse(retorno[7]));
            partida.Desafios = desafioController.getByPartida(partida);
        }

        return partida;
    }

    public Partida getUltimaNivel(Jogador jogador, Nivel nivel)
    {
        Partida partida = null;
        JogadorController jogadorController = new JogadorController();
        NivelController nivelController = new NivelController();
        DesafioController desafioController = new DesafioController();

        string query = "SELECT p.id, p.acertos, p.erros, p.data_inicio, p.data_termino, p.concluido, p.jogador_id, p.nivel_id FROM partida p " +
            " INNER JOIN nivel n ON n.id = p.nivel_id " +
            " WHERE p.jogador_id = @id and n.nome = @nivel and p.data_termino is not null and p.concluido = @concluido ORDER BY p.data_inicio DESC";
        var param = new Dictionary<string, string>();
        param.Add("id", jogador.Id.ToString());
        param.Add("nivel", nivel.Nome.ToUpper());
        param.Add("concluido", "1");

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
            partida.Nivel = nivelController.get(Int32.Parse(retorno[7]));
            partida.Desafios = desafioController.getByPartida(partida);
        }

        return partida;
    }

    public Partida getAtual(Jogador jogador, string nivel)
    {
        Partida partida = null;
        JogadorController jogadorController = new JogadorController();
        NivelController nivelController = new NivelController();
        DesafioController desafioController = new DesafioController();

        string query = "SELECT p.id, p.acertos, p.erros, p.data_inicio, p.data_termino, p.concluido, p.jogador_id, p.nivel_id FROM partida p INNER JOIN nivel n ON n.id = p.nivel_id WHERE n.nome = @nivel and jogador_id = @jogadorId and data_termino is null ORDER BY nivel_id DESC, data_inicio DESC";
        var param = new Dictionary<string, string>();
        param.Add("nivel", nivel.ToUpper());
        param.Add("jogadorId", jogador.Id.ToString());

        Dictionary<int, List<string>> retornos = dataBase.Select(query, param);
        if (retornos.Count > 0)
        {
            List<string> retorno = retornos[0];

            partida = new Partida();
            partida.Id = Int32.Parse(retorno[0]);
            partida.Acertos = 0;
            partida.Erros = 0;
            partida.DataInicio = DateTime.Parse(retorno[3]);
            if (retorno[4].Length > 0)
                partida.DataTermino = DateTime.Parse(retorno[4]);
            partida.Concluido = false;
            partida.Jogador = jogadorController.get(Int32.Parse(retorno[6]));
            partida.Nivel = nivelController.get(Int32.Parse(retorno[7]));
            partida.Desafios = desafioController.getByPartida(partida);

            atualizarPartida(partida);
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


        string query = "INSERT INTO partida (acertos, erros, jogador_id, nivel_id, data_inicio, concluido) VALUES ( @acertos, @erros, @jogador, @nivel, @dataInicio, @concluido)";
        var param = new Dictionary<string, string>();
        param.Add("acertos", "0");
        param.Add("erros", "0");
        param.Add("jogador", jogador.Id.ToString());
        param.Add("nivel", nivel.Id.ToString());
        param.Add("dataInicio", dataBase.DateTimeSQLite(agora));
        param.Add("concluido", "0");
        int id = dataBase.Insert(query, param);

        partida = get(id);

        partida.Desafios = desafioController.criarDesafios(partida, qtdeDesafios);

        return partida;
    }

    public void encerrarPartida(Partida partida)
    {
        DesafioController desafioController = new DesafioController();

        DateTime agora = DateTime.Now;

        string query = "UPDATE partida SET acertos = @acertos, erros = @erros, data_termino = @dataTermino, concluido = @concluido WHERE id = @id";
        var param = new Dictionary<string, string>();
        param.Add("id", partida.Id.ToString());
        param.Add("acertos", partida.Acertos.ToString());
        param.Add("erros", partida.Erros.ToString());
        param.Add("dataTermino", dataBase.DateTimeSQLite(agora));
        param.Add("concluido", (partida.Concluido ? 1 : 0).ToString());

        int id = dataBase.Insert(query, param);
    }

    public void atualizarPartida(Partida partida)
    {
        DesafioController desafioController = new DesafioController();

        DateTime agora = DateTime.Now;

        string query = "UPDATE partida SET acertos = @acertos, erros = @erros, data_inicio = @dataInicio, concluido = @concluido WHERE id = @id";
        var param = new Dictionary<string, string>();
        param.Add("id", partida.Id.ToString());
        param.Add("acertos", partida.Acertos.ToString());
        param.Add("erros", partida.Erros.ToString());
        param.Add("dataInicio", dataBase.DateTimeSQLite(agora));
        param.Add("concluido", (partida.Concluido?1:0).ToString());

        int id = dataBase.Insert(query, param);
    }

    public bool verificarNivelCompleto(Partida ultimaPartida)
    {
        if(ultimaPartida == null)
        {
            return false;
        }

        Nivel nivel = ultimaPartida.Nivel;
        Jogador jogador = ultimaPartida.Jogador;
        int count = 0;

        string query = "SELECT COUNT(DISTINCT n.id) FROM partida p INNER JOIN nivel n ON n.id = p.nivel_id WHERE n.dificuldade_id = @dificuldade AND jogador_id = @jogador AND concluido = 1 AND data_termino is not null ";
        var param = new Dictionary<string, string>();
        param.Add("dificuldade", nivel.Dificuldade.Id.ToString());
        param.Add("jogador", jogador.Id.ToString());


        Dictionary<int, List<string>> retornos = dataBase.Select(query, param);
        if (retornos.Count > 0)
        {
            List<string> retorno = retornos[0];
            count = Int32.Parse(retorno[0]);
        }

        if(count >= 7)
        {
            return true;
        }
        else
        {
            return false;
        }        
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

        bool fgConcluido = this.verificarNivelCompleto(partida);

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

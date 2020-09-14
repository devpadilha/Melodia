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

        string query = "SELECT id, acertos, erros, jogador_id FROM partida WHERE id = @id";
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
            partida.Jogador = jogador.get(Int32.Parse(retorno[3]));
        }

        return partida;
    }
}

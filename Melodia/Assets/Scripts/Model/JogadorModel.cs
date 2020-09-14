using Mono.Data.Sqlite;
using System.Data;
using System;
using System.Collections.Generic;

public class JogadorModel
{
    private DataBase dataBase;

    public JogadorModel()
    {
        dataBase = new DataBase();
    }

    public Jogador get(int id)
    {
        Jogador jogador = null;     

        string query = "SELECT id, sexo, data_nascimento FROM jogador WHERE id = @id";
        var param = new Dictionary<string, string>();
        param.Add("id", id.ToString());
        Dictionary<int, List<string>> retornos = dataBase.Select(query, param);
        if (retornos.Count > 0)
        {
            List<string> retorno = retornos[0];

            jogador = new Jogador();
            jogador.Id = Int32.Parse(retorno[0]);
            jogador.Sexo = retorno[1];
            jogador.DataNascimento = DateTime.Parse(retorno[2]);
        }

        return jogador;
    }

    public Jogador save(Jogador jogador)
    {
        string query = "INSERT INTO jogador (sexo,data_nascimento) VALUES (@sexo, @data_nascimento);";
        var param = new Dictionary<string, string>();
        param.Add("sexo", jogador.Sexo);
        param.Add("data_nascimento", jogador.DataNascimento.ToString("yyyy-MM-dd"));

        int id = dataBase.Insert(query, param);
        jogador.Id = id;
        return jogador;
    }
}

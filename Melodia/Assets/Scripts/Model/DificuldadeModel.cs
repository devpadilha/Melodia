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
}

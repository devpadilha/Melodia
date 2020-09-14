using Mono.Data.Sqlite;
using System.Data;
using System;
using System.Collections.Generic;

public class NivelModel 
{
    private DataBase dataBase;

    public NivelModel()
    {
        dataBase = new DataBase();
    }

    public Nivel get(int id)
    {
        Nivel nivel = null;
        DificuldadeController dificuldade = new DificuldadeController();

        string query = "SELECT id, nome, descricao, dificuldade_id FROM nivel WHERE id = @id";
        var param = new Dictionary<string, string>();
        param.Add("id", id.ToString());
        Dictionary<int, List<string>> retornos = dataBase.Select(query, param);
        if (retornos.Count > 0)
        {
            List<string> retorno = retornos[0];

            nivel = new Nivel();
            nivel.Id = Int32.Parse(retorno[0]);
            nivel.Nome = retorno[1];
            nivel.Descricao = retorno[2];
            nivel.Dificuldade = dificuldade.get(Int32.Parse(retorno[3]));
        }

        return nivel;
    }
}

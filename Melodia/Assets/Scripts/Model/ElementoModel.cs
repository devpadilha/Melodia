using Mono.Data.Sqlite;
using System.Data;
using System;
using System.Collections.Generic;

public class ElementoModel
{
    private DataBase dataBase;

    public ElementoModel()
    {
        dataBase = new DataBase();
    }

    public Elemento get(int id)
    {
        Elemento elemento = null;
        NivelController nivel = new NivelController();

        string query = "SELECT id, nome, descricao, resource, nivel_id FROM elemento WHERE id = @id";
        var param = new Dictionary<string, string>();
        param.Add("id", id.ToString());
        Dictionary<int, List<string>> retornos = dataBase.Select(query, param);
        if (retornos.Count > 0)
        {
            List<string> retorno = retornos[0];

            elemento = new Elemento();
            elemento.Id = Int32.Parse(retorno[0]);
            elemento.Nome = retorno[1];
            elemento.Descricao = retorno[2];
            elemento.Nivel = nivel.get(Int32.Parse(retorno[3]));
        }

        return elemento;
    }
}

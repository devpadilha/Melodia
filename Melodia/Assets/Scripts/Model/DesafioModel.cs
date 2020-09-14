using Mono.Data.Sqlite;
using System.Data;
using System;
using System.Collections.Generic;

public class DesafioModel
{
    private DataBase dataBase;

    public DesafioModel()
    {
        dataBase = new DataBase();
    }

    public Desafio get(int id)
    {
        Desafio desafio = null;
        ElementoController elemento = new ElementoController();

        string query = "SELECT id, descricao, pergunta_elemento_id, resposta_elemento_id FROM desafio WHERE id = @id";
        var param = new Dictionary<string, string>();
        param.Add("id", id.ToString());
        Dictionary<int, List<string>> retornos = dataBase.Select(query, param);
        if (retornos.Count > 0)
        {
            List<string> retorno = retornos[0];

            desafio = new Desafio();
            desafio.Id = Int32.Parse(retorno[0]);
            desafio.Descricao = retorno[1];           
            desafio.Pergunta = elemento.get(Int32.Parse(retorno[2]));
            desafio.Resposta = elemento.get(Int32.Parse(retorno[3]));
        }

        return desafio;
    }
}

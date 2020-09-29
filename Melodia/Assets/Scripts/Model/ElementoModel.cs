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

        string query = "SELECT id, nome, descricao, resource FROM elemento WHERE id = @id";
        var param = new Dictionary<string, string>();
        param.Add("id", id.ToString());
        Dictionary<int, List<string>> retornos = dataBase.Select(query, param);
        if (retornos.Count > 0)
        {
            List<string> retorno = retornos[0];

            elemento = getVO(retorno);
        }

        return elemento;
    }

    public List<Elemento> getErradasByDesafio(Desafio desafio, int qtde)
    {
        RandomUtil randNum;
        int rand;
        List<Elemento> elementos = new List<Elemento>();

        string query = "SELECT e.id, e.nome, e.descricao, e.resource FROM elemento e " +
            " INNER JOIN desafio d ON d.resposta_elemento_id = e.id" +
            " WHERE d.nivel_id = @nivel AND e.id <> @elemento";
       
        var param = new Dictionary<string, string>();
        param.Add("nivel", desafio.Nivel.Id.ToString());
        param.Add("elemento", desafio.Resposta.Id.ToString());
        Dictionary<int, List<string>> retornos = dataBase.Select(query, param);

        if (qtde > retornos.Keys.Count)
        {
            qtde = retornos.Keys.Count;
        }

        randNum = new RandomUtil(0, retornos.Keys.Count);

        for (int i = 0; i < qtde; i++)
        {
           
            rand = randNum.get();            

            Elemento vo = getVO(retornos[rand]);
            elementos.Add(vo);
        }

        return elementos;
    }

    private Elemento getVO(List<string> retorno)
    {
        Elemento elemento = new Elemento();
        elemento.Id = Int32.Parse(retorno[0]);
        elemento.Nome = retorno[1];
        elemento.Descricao = retorno[2];
        elemento.Resource = retorno[3];

        return elemento;
    }
}

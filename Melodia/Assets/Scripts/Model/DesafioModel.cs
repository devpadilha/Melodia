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
        

        string query = "SELECT id, descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id FROM desafio WHERE id = @id";
        var param = new Dictionary<string, string>();
        param.Add("id", id.ToString());
        Dictionary<int, List<string>> retornos = dataBase.Select(query, param);
        if (retornos.Count > 0)
        {
            List<string> retorno = retornos[0];

            desafio = getVO(retorno);
            
        }

        return desafio;
    }

    public List<Desafio> getByPartida(Partida partida)
    {
        List<Desafio> desafios = new List<Desafio>();
        string query = "SELECT d.id, d.descricao, d.pergunta_elemento_id, d.resposta_elemento_id, d.nivel_id FROM desafio d " +
            " INNER JOIN partida_desafio pd ON pd.desafio_id = d.id " +
            " WHERE pd.partida_id = @id";
        var param = new Dictionary<string, string>();
        param.Add("id", partida.Id.ToString());
        Dictionary<int, List<string>> retornos = dataBase.Select(query, param);

        foreach(var i in retornos)
        {
            Desafio vo = getVO(retornos[i.Key]);
            desafios.Add(vo);
        }

        return desafios;
    }

    public List<Desafio> criarDesafios(Partida partida, int qtde)
    {
        RandomUtil randNum;
        int rand;
        List<Desafio> desafios = new List<Desafio>();
        string query = "SELECT id, descricao, pergunta_elemento_id, resposta_elemento_id, nivel_id FROM desafio WHERE nivel_id = @nivel";
        var param = new Dictionary<string, string>();
        param.Add("nivel", partida.Nivel.Id.ToString());
        Dictionary<int, List<string>> retornos = dataBase.Select(query, param);

        if(qtde > retornos.Keys.Count)
        {
            qtde = retornos.Keys.Count;
        }

        randNum = new RandomUtil(0, retornos.Keys.Count);

        for (int i=0; i<qtde; i++)
        {
            rand = randNum.get();
            
            Desafio vo = getVO(retornos[rand]);
            desafios.Add(vo);

            query = "INSERT INTO partida_desafio VALUES (@partida, @desafio)";
            param = new Dictionary<string, string>();
            param.Add("partida", partida.Id.ToString());
            param.Add("desafio", vo.Id.ToString());
            dataBase.Insert(query, param);            
        }

        return desafios;

    }

    private Desafio getVO(List<string> retorno)
    {
        ElementoController elemento = new ElementoController();
        NivelController nivel = new NivelController();

        Desafio desafio = new Desafio();
        desafio.Id = Int32.Parse(retorno[0]);
        desafio.Descricao = retorno[1];
        if (retorno[2].Length > 0)
        {
            desafio.Pergunta = elemento.get(Int32.Parse(retorno[2]));
        }
        desafio.Resposta = elemento.get(Int32.Parse(retorno[3]));
        desafio.Nivel = nivel.get(Int32.Parse(retorno[4]));

        return desafio;
    }
}

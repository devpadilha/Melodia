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

        string query = "SELECT id, nome, descricao, max_erros, dificuldade_id FROM nivel WHERE id = @id";
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
            nivel.MaxErros = Int32.Parse(retorno[3]);
            nivel.Dificuldade = dificuldade.get(Int32.Parse(retorno[4]));
        }

        return nivel;
    }

    public Nivel getNext(string nivelNome, Partida ultimaPartida)
    {
        Nivel nivel = null;
        DificuldadeController dificuldade = new DificuldadeController();

        string query = "SELECT id, nome, descricao, max_erros, dificuldade_id FROM nivel WHERE nome = @nome AND dificuldade_id = @dificuldade";
        var param = new Dictionary<string, string>();
        
        if(ultimaPartida == null || !ultimaPartida.Nivel.Nome.Equals(nivelNome.ToUpper()))
        {
            param.Add("nome", nivelNome.ToUpper());
            param.Add("dificuldade", "1");
        }
        else if((ultimaPartida.Nivel.Dificuldade.Id + 1) > 3)
        {
            param.Add("nome", nivelNome.ToUpper());
            param.Add("dificuldade", (ultimaPartida.Nivel.Dificuldade.Id).ToString());
        }
        else
        {
            param.Add("nome", nivelNome.ToUpper());
            param.Add("dificuldade", (ultimaPartida.Nivel.Dificuldade.Id + 1).ToString());
        }      


        Dictionary<int, List<string>> retornos = dataBase.Select(query, param);
        if (retornos.Count > 0)
        {
            List<string> retorno = retornos[0];

            nivel = new Nivel();
            nivel.Id = Int32.Parse(retorno[0]);
            nivel.Nome = retorno[1];
            nivel.Descricao = retorno[2];
            nivel.MaxErros = Int32.Parse(retorno[3]);
            nivel.Dificuldade = dificuldade.get(Int32.Parse(retorno[4]));
        }

        return nivel;
    }
}

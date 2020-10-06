using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionarioModel
{
    private DataBase dataBase;

    public QuestionarioModel()
    {
        dataBase = new DataBase();
    }

    public List<Questionario> getByNivel(Nivel nivel)
    {
        Questionario pergunta = null;
        List<Questionario> questionario = new List<Questionario>();
        NivelController nivelController = new NivelController();

        string query = "SELECT id, pergunta, opcoes, nivel_id FROM questionario WHERE nivel_id = @id";
        var param = new Dictionary<string, string>();
        param.Add("id", nivel.Id.ToString());
        Dictionary<int, List<string>> retornos = dataBase.Select(query, param);
        if (retornos.Count > 0)
        {

            foreach (var i in retornos)
            {
                List<string> retorno = retornos[i.Key];

                pergunta = new Questionario();
                pergunta.Id = Int32.Parse(retorno[0]);
                pergunta.Pergunta = retorno[1];
                pergunta.Opcoes = retorno[2];
                pergunta.Nivel = nivelController.get(Int32.Parse(retorno[3]));

                questionario.Add(pergunta);
            }
        }

        return questionario;
    }

    public bool isRespondido(string nivel, Jogador jogador)
    {
        Questionario pergunta = null;
        List<Questionario> questionario = new List<Questionario>();
        NivelController nivelController = new NivelController();


        string query = "SELECT qr.id FROM questionario q inner join questionario_resposta qr on qr.pergunta_id = q.id inner join nivel n on n.id = q.nivel_id inner join jogador j on j.id = qr.jogador_id WHERE n.nome = @nivelNome AND j.id = @jogadorId";
        var param = new Dictionary<string, string>();
        param.Add("nivelNome", nivel.ToUpper());
        param.Add("jogadorId", jogador.Id.ToString());
        Dictionary<int, List<string>> retornos = dataBase.Select(query, param);
        if (retornos.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void insertResposta(Resposta resposta)
    {
        string query = "INSERT INTO questionario_resposta (resposta,pergunta_id,jogador_id) VALUES (@resposta,@perguntaId,@jogadorId);";
        var param = new Dictionary<string, string>();
        param.Add("resposta", resposta.Opcao);
        param.Add("perguntaId", resposta.Questionario.Id.ToString());
        param.Add("jogadorId", resposta.Jogador.Id.ToString());

        dataBase.Insert(query, param);
    }
}

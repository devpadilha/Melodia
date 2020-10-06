using System.Collections.Generic;

public class QuestionarioController
{
    QuestionarioModel model;

    public QuestionarioController()
    {
        model = new QuestionarioModel();
    }

    public List<Questionario> getByNivel(Nivel nivel)
    {
        return model.getByNivel(nivel);
    }

    public void insertResposta(Resposta resposta)
    {
       model.insertResposta(resposta);
    }   

    public bool isRespondido(string nivel, Jogador jogador)
    {
        return model.isRespondido(nivel, jogador);
    }
}

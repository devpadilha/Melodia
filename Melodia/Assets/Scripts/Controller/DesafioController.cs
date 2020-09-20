using System.Collections.Generic;

public class DesafioController
{
    DesafioModel model;

    public DesafioController()
    {
        model = new DesafioModel();
    }

    public Desafio get(int id)
    {
        return model.get(id);
    }

    public List<Desafio> getByPartida (Partida partida)
    {
        return model.getByPartida(partida);
    }

    public List<Desafio> criarDesafios(Partida partida, int qtde)
    {
        return model.criarDesafios(partida, qtde);
    }
}

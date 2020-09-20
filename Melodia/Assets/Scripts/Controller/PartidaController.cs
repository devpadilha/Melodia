public class PartidaController
{
    PartidaModel model;

    public PartidaController()
    {
        model = new PartidaModel();
    }

    public Partida get(int id)
    {
        return model.get(id);
    }

    public Partida getUltima(Jogador jogador)
    {
        return model.getUltima(jogador);
    }

    public Partida criarPartida(Jogador jogador, Nivel nivel, int qtdeDesafios)
    {
        return model.criarPartida(jogador, nivel, qtdeDesafios);
    }
}

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

    public Partida getUltimaNivel(Jogador jogador, Nivel nivel)
    {
        return model.getUltimaNivel(jogador, nivel);
    }

    public Partida getAtual(Jogador jogador, string nivel)
    {
        return model.getAtual(jogador, nivel);
    }

    public Partida criarPartida(Jogador jogador, Nivel nivel, int qtdeDesafios)
    {
        return model.criarPartida(jogador, nivel, qtdeDesafios);
    }

    public void encerrarPartida(Partida partida)
    {
        model.encerrarPartida(partida);
    }
}

public class NivelController
{
    NivelModel model;

    public NivelController()
    {
        model = new NivelModel();
    }

    public Nivel get(int id)
    {
        return model.get(id);
    }

    public Nivel get(string nome)
    {
        return model.get(nome);
    }

    public Nivel getNext(string nivelNome, Partida ultimaPartida)
    {
        return model.getNext(nivelNome, ultimaPartida);
    }
}

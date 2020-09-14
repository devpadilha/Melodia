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
}

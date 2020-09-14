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
}

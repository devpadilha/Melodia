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
}

public class ElementoController
{
    ElementoModel model;

    public ElementoController()
    {
        model = new ElementoModel();
    }

    public Elemento get(int id)
    {
        return model.get(id);
    }
}

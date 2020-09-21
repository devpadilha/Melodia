using System.Collections.Generic;

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

    public List<Elemento> getErradasByDesafio(Desafio desafio, int qtde)
    {
        return model.getErradasByDesafio(desafio, qtde);
    }
}

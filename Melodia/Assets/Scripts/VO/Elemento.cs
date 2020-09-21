public class Elemento
{
    private int id;
    private string nome;
    private string descricao;
    private string resource;

    public int Id
    {
        get
        {
            return id;
        }
        set
        {
            id = value;
        }
    }

    public string Nome
    {
        get
        {
            return nome;
        }
        set
        {
            nome = value;
        }
    }

    public string Descricao
    {
        get
        {
            return descricao;
        }
        set
        {
            descricao = value;
        }
    }

    public string Resource
    {
        get
        {
            return resource;
        }
        set
        {
            resource = value;
        }
    }
}

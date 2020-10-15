public class Nivel
{
    private int id;
    private string nome;
    private string descricao;
    private int maxErros;
    private int minAcertos;
    private Dificuldade dificuldade;

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

    public int MaxErros
    {
        get
        {
            return maxErros;
        }
        set
        {
            maxErros = value;
        }
    }

    public int MinAcertos
    {
        get
        {
            return minAcertos;
        }
        set
        {
            minAcertos = value;
        }
    }

    public Dificuldade Dificuldade
    {
        get
        {
            return dificuldade;
        }
        set
        {
            dificuldade = value;
        }
    }
}

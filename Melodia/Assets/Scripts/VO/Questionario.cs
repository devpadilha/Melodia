public class Questionario
{
    private int id;
    private string pergunta;
    private string opcoes;
    private Nivel nivel;

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

    public string Pergunta
    {
        get
        {
            return pergunta;
        }
        set
        {
            pergunta = value;
        }
    }

    public string Opcoes
    {
        get
        {
            return opcoes;
        }
        set
        {
            opcoes = value;
        }
    }

    public Nivel Nivel
    {
        get
        {
            return nivel;
        }
        set
        {
            nivel = value;
        }
    }
}

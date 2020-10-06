public class Resposta
{
    private int id;
    private string opcao;
    private Questionario questionario;
    private Jogador jogador;

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

    public string Opcao
    {
        get
        {
            return opcao;
        }
        set
        {
            opcao = value;
        }
    }

    public Questionario Questionario
    {
        get
        {
            return questionario;
        }
        set
        {
            questionario = value;
        }
    }

    public Jogador Jogador
    {
        get
        {
            return jogador;
        }
        set
        {
            jogador = value;
        }
    }
}

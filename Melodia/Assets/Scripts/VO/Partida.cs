public class Partida
{
    private int id;
    private int acertos;
    private int erros;
    private Jogador jogador;
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

    public int Acertos
    {
        get
        {
            return acertos;
        }
        set
        {
            acertos = value;
        }
    }

    public int Erros
    {
        get
        {
            return erros;
        }
        set
        {
            erros = value;
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

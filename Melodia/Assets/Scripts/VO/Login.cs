public class Login
{
    private int id;
    private string usuario;
    private string senha;
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

    public string Usuario
    {
        get
        {
            return usuario;
        }
        set
        {
            usuario = value;
        }
    }

    public string Senha
    {
        get
        {
            return senha;
        }
        set
        {
            senha = value;
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

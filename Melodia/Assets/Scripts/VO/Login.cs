public class Login
{
    private int id;
    private string usuario;
    private string senha;
    private bool ativo;
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

    public bool Ativo
    {
        get
        {
            return ativo;
        }
        set
        {
            ativo = value;
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

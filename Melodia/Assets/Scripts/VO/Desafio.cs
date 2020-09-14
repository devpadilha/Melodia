public class Desafio
{
    private int id;
    private string descricao;
    private Elemento pergunta;
    private Elemento resposta;

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

    public Elemento Pergunta
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

    public Elemento Resposta
    {
        get
        {
            return resposta;
        }
        set
        {
            resposta = value;
        }
    }
}

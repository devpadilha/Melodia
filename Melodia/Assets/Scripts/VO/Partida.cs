using System.Collections.Generic;

public class Partida
{
    private int id;
    private int acertos;
    private int erros;
    private bool concluido;
    private Jogador jogador;
    private Nivel nivel;
    private List<Desafio> desafios;

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

    public bool Concluido
    {
        get
        {
            return concluido;
        }
        set
        {
            concluido = value;
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

    public List<Desafio> Desafios
    {
        get
        {
            return desafios;
        }
        set
        {
            desafios = value;
        }
    }
}

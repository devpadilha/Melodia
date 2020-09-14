using System.Data;
using System;

public class Jogador
{
    private int id;
    private string sexo;
    private DateTime dataNascimento;

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

    public string Sexo
    {
        get
        {
            return sexo;
        }
        set
        {
            sexo = value;
        }
    }

    public DateTime DataNascimento
    {
        get
        {
            return dataNascimento;
        }
        set
        {
            dataNascimento = value;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resultado
{
    private Dificuldade dificuldade;
    private Jogador jogador;
    private int totalAcertos;
    private int totalErros;
    private double totalTempo;

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

    public int TotalAcertos
    {
        get
        {
            return totalAcertos;
        }
        set
        {
            totalAcertos = value;
        }
    }

    public int TotalErros
    {
        get
        {
            return totalErros;
        }
        set
        {
            totalErros = value;
        }
    }

    public double TotalTempo
    {
        get
        {
            return totalTempo;
}
        set
        {
            totalTempo = value;
        }
    }

}

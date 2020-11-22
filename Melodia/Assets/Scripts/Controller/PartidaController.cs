using System;
using System.Collections.Generic;

public class PartidaController
{
    PartidaModel model;

    public PartidaController()
    {
        model = new PartidaModel();
    }

    public Partida get(int id)
    {
        return model.get(id);
    }

    public Partida getUltima(Jogador jogador)
    {
        return model.getUltima(jogador);
    }

    public Partida getUltimaNivel(Jogador jogador, Nivel nivel)
    {
        return model.getUltimaNivel(jogador, nivel);
    }

    public Partida getAtual(Jogador jogador, string nivel)
    {
        return model.getAtual(jogador, nivel);
    }

    public Partida criarPartida(Jogador jogador, Nivel nivel)
    {
        return model.criarPartida(jogador, nivel);
    }

    public void encerrarPartida(Partida partida)
    {
        model.encerrarPartida(partida);
    }    

    public Resultado calcularResultado(Partida ultimaPartida)
    {
        Resultado resultado = new Resultado();
        Jogador jogador = ultimaPartida.Jogador;
        Dificuldade dificuldade = ultimaPartida.Nivel.Dificuldade;
        List<Partida> partidas = model.obterPartidas(jogador, dificuldade);
        int totalAcertos = 0, totalErros = 0;
        double totalTempo = 0.0d;

        foreach(Partida partida in partidas)
        {
            totalAcertos += partida.Acertos;
            totalErros += partida.Erros;

            if (partida.DataTermino != null)
            {
                TimeSpan ts = partida.DataTermino - partida.DataInicio;
                totalTempo += ts.TotalMinutes;
            }
        }

        resultado.Dificuldade = dificuldade;
        resultado.Jogador = jogador;
        resultado.TotalAcertos = totalAcertos;
        resultado.TotalErros = totalErros;
        resultado.TotalTempo = totalTempo;        

        return resultado;
    }
}

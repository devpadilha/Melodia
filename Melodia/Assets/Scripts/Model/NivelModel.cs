using Mono.Data.Sqlite;
using System.Data;
using System;
using System.Collections.Generic;

public class NivelModel 
{
    private DataBase dataBase;

    public NivelModel()
    {
        dataBase = new DataBase();
    }

    public Nivel get(int id)
    {
        Nivel nivel = null;
        DificuldadeController dificuldade = new DificuldadeController();

        string query = "SELECT id, nome, descricao, max_erros, min_acertos, dificuldade_id, qtde_desafios FROM nivel WHERE id = @id";
        var param = new Dictionary<string, string>();
        param.Add("id", id.ToString());
        Dictionary<int, List<string>> retornos = dataBase.Select(query, param);
        if (retornos.Count > 0)
        {
            List<string> retorno = retornos[0];

            nivel = new Nivel();
            nivel.Id = Int32.Parse(retorno[0]);
            nivel.Nome = retorno[1];
            nivel.Descricao = retorno[2];
            nivel.MaxErros = Int32.Parse(retorno[3]);
            nivel.MinAcertos = Int32.Parse(retorno[4]);
            nivel.Dificuldade = dificuldade.get(Int32.Parse(retorno[5]));
            nivel.QtdeDesafios = Int32.Parse(retorno[6]);
        }

        return nivel;
    }

    public Nivel get(string nome)
    {
        Nivel nivel = null;
        DificuldadeController dificuldade = new DificuldadeController();

        string query = "SELECT id, nome, descricao, max_erros, min_acertos, dificuldade_id, qtde_desafios FROM nivel WHERE nome = @nome and dificuldade_id = @dificuldade";
        var param = new Dictionary<string, string>();
        param.Add("nome", nome);
        param.Add("dificuldade", "1");
        Dictionary<int, List<string>> retornos = dataBase.Select(query, param);
        if (retornos.Count > 0)
        {
            List<string> retorno = retornos[0];

            nivel = new Nivel();
            nivel.Id = Int32.Parse(retorno[0]);
            nivel.Nome = retorno[1];
            nivel.Descricao = retorno[2];
            nivel.MaxErros = Int32.Parse(retorno[3]);
            nivel.MinAcertos = Int32.Parse(retorno[4]);
            nivel.Dificuldade = dificuldade.get(Int32.Parse(retorno[5]));
            nivel.QtdeDesafios = Int32.Parse(retorno[6]);
        }

        return nivel;
    }

    public Nivel getNext(string nivelNome, Partida ultimaPartida)
    {
        Nivel nivel = get(nivelNome);
        DificuldadeController dificuldade = new DificuldadeController();
        PartidaController partida = new PartidaController();
        NivelController nivelController = new NivelController();

        bool fgNivelConcluido = nivelController.verificarNivelCompleto(ultimaPartida);

        string query = "SELECT id, nome, descricao, max_erros, min_acertos, dificuldade_id, qtde_desafios FROM nivel WHERE nome = @nome AND dificuldade_id = @dificuldade";
        var param = new Dictionary<string, string>();
        
        if(ultimaPartida == null)
        {
            param.Add("nome", nivelNome.ToUpper());
            param.Add("dificuldade", "1");
        }
        else if (ultimaPartida.Nivel.Dificuldade.Id.Equals((int) DificuldadeEnum.Dificuldade.FACIL))
        {
            param.Add("nome", nivelNome.ToUpper());
            if (fgNivelConcluido)
            {
                param.Add("dificuldade", "2");
            }
            else
            {
                param.Add("dificuldade", "1");
            }    
        }
        else if (ultimaPartida.Nivel.Dificuldade.Id.Equals((int)DificuldadeEnum.Dificuldade.MEDIO))
        {
            param.Add("nome", nivelNome.ToUpper());
            if (fgNivelConcluido)
            {
                param.Add("dificuldade", "3");
            }
            else
            {
                param.Add("dificuldade", "2");
            }
        }
        else
        {
            param.Add("nome", nivelNome.ToUpper());
            param.Add("dificuldade", "3");
        }      


        Dictionary<int, List<string>> retornos = dataBase.Select(query, param);
        if (retornos.Count > 0)
        {
            List<string> retorno = retornos[0];

            nivel = new Nivel();
            nivel.Id = Int32.Parse(retorno[0]);
            nivel.Nome = retorno[1];
            nivel.Descricao = retorno[2];
            nivel.MaxErros = Int32.Parse(retorno[3]);
            nivel.MinAcertos = Int32.Parse(retorno[4]);
            nivel.Dificuldade = dificuldade.get(Int32.Parse(retorno[5]));
            nivel.QtdeDesafios = Int32.Parse(retorno[6]);
        }

        return nivel;
    }

    public bool verificarNivelCompleto(Partida ultimaPartida)
    {
        if (ultimaPartida == null)
        {
            return false;
        }

        Nivel nivel = ultimaPartida.Nivel;
        Jogador jogador = ultimaPartida.Jogador;
        int count = 0;

        string query = "SELECT COUNT(DISTINCT n.id) FROM partida p INNER JOIN nivel n ON n.id = p.nivel_id WHERE n.dificuldade_id = @dificuldade AND jogador_id = @jogador AND concluido = 1 AND data_termino is not null ";
        var param = new Dictionary<string, string>();
        param.Add("dificuldade", nivel.Dificuldade.Id.ToString());
        param.Add("jogador", jogador.Id.ToString());


        Dictionary<int, List<string>> retornos = dataBase.Select(query, param);
        if (retornos.Count > 0)
        {
            List<string> retorno = retornos[0];
            count = Int32.Parse(retorno[0]);
        }

        if (count >= 7)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}

using Mono.Data.Sqlite;
using System.Data;
using System;
using System.Collections.Generic;

public class LoginModel 
{
    private DataBase dataBase;

    public LoginModel()
    {
        dataBase = new DataBase();
    }

    public Login get(int id)
    {
        Login login = null;

        DataBase dataBase = new DataBase();
        JogadorController jogador = new JogadorController();

        string query = "SELECT id, usuario, senha, jogador_id FROM login WHERE id = @id";
        var param = new Dictionary<string, string>();
        param.Add("id", id.ToString());
        Dictionary<int, List<string>> retornos = dataBase.Select(query, param);
        if (retornos.Count > 0)
        {
            List<string> retorno = retornos[0];

            login = new Login();
            login.Id = Int32.Parse(retorno[0]);
            login.Usuario = retorno[1];
            login.Senha = retorno[2];
            login.Jogador = jogador.get(Int32.Parse(retorno[3]));
        }

        return login;
    }

    public Login get(string usuario, string senha)
    {
        Login login = null;

       
        JogadorController jogador = new JogadorController();

        string query = "SELECT id, usuario, senha, jogador_id FROM login WHERE usuario = @usuario and senha = @senha";
        var param = new Dictionary<string, string>();
        param.Add("usuario", usuario);
        param.Add("senha", senha);
        Dictionary<int, List<string>> retornos = dataBase.Select(query, param);
        if (retornos.Count > 0)
        {
            List<string> retorno = retornos[0];

            login = new Login();
            login.Id = Int32.Parse(retorno[0]);
            login.Usuario = retorno[1];
            login.Senha = retorno[2];
            login.Jogador = jogador.get(Int32.Parse(retorno[3]));
        }

        return login;
    }

    public Login save(Login login)
    {
        string query = "INSERT INTO login (usuario,senha,jogador_id) VALUES (@usuario, @senha,@fkid);";
        var param = new Dictionary<string, string>();
        param.Add("usuario", login.Usuario);
        param.Add("senha", login.Senha);
        param.Add("fkid", login.Jogador.Id.ToString());

        int id = dataBase.Insert(query, param);

        login.Id = id;

        return login;
    }
}

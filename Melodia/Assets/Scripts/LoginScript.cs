using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginScript : MonoBehaviour
{
    public InputField login;
    public InputField senha;
    public string sceneName;

    private void Start()
    {
        ReadCharacters();
    }

    private void ReadCharacters()
    {
        string connectionString = "URI=file:" + Application.dataPath + "/melodia_database.db";

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM login";
                command.CommandType = CommandType.Text;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        print((string)reader[1] + "\n" +
                              (int)reader[2] + "\n" +
                              (string)reader[3]);
                    }
                }
            }
        }
    }

    private bool Login()
    {
        string connectionString = "URI=file:" + Application.dataPath + "/melodia_database.db";

        string pLogin = this.login.text;
        string pSenha = this.senha.text;
        bool sucesso = false;

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT id, login, senha FROM login WHERE login = @login AND senha = @senha";
                command.CommandType = CommandType.Text;
                command.Parameters.Add(new SqliteParameter("login", pLogin));
                command.Parameters.Add(new SqliteParameter("senha", pSenha));

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        sucesso = true;
                    }
                }
            }
        }
        return sucesso;
    }

    public void RealizarLogin()
    {
        bool isLogado = Login();
        if (isLogado)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void Save()
    {
        string connectionString = "URI=file:" + Application.dataPath + "/melodia_database.db";

        string pLogin = this.login.text;
        string pSenha = this.senha.text;
      

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                using (var command = connection.CreateCommand())
                {
                    try
                    {
                        command.CommandText = "INSERT INTO login (login,senha) VALUES (@login, @senha);";
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqliteParameter("login", pLogin));
                        command.Parameters.Add(new SqliteParameter("senha", pSenha));                        

                        command.Transaction = transaction;

                        var rows = command.ExecuteNonQuery();

                        transaction.Commit();                       
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        print(e.Message);
                    }
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginScript : MonoBehaviour
{
    public InputField login;
    public InputField senha;
    public Dropdown sexo;
    public Dropdown dia;
    public Dropdown mes;
    public Dropdown ano;
    public Text retornoTelaLogin;
    public string sceneName;

    private void Start()
    {
        retornoTelaLogin.enabled = false;
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
        else
        {
            this.retornoTelaLogin.enabled = true;
            this.retornoTelaLogin.text = "Login ou senha inválidos";
        }
    }

    public void SalvarJogador()
    {
        string connectionString = "URI=file:" + Application.dataPath + "/melodia_database.db";

        List<Dropdown.OptionData> menuOptions = this.sexo.GetComponent<Dropdown>().options;
        int menuIndex = this.sexo.GetComponent<Dropdown>().value;
        string sexo = menuOptions[menuIndex].text;

        menuOptions = this.dia.GetComponent<Dropdown>().options;
        menuIndex = this.dia.GetComponent<Dropdown>().value;
        string dia_nascimento = menuOptions[menuIndex].text;

        menuOptions = this.mes.GetComponent<Dropdown>().options;
        menuIndex = this.mes.GetComponent<Dropdown>().value;
        string mes_nascimento = menuOptions[menuIndex].text;

        menuOptions = this.ano.GetComponent<Dropdown>().options;
        menuIndex = this.ano.GetComponent<Dropdown>().value;
        string ano_nascimento = menuOptions[menuIndex].text;

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
                        command.CommandText = "INSERT INTO jogador (sexo,dia_nascimento,mes_nascimento,ano_nascimento) VALUES (@sexo, @dia, @mes, @ano);";
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqliteParameter("sexo", sexo));
                        command.Parameters.Add(new SqliteParameter("dia", dia_nascimento));
                        command.Parameters.Add(new SqliteParameter("mes", mes_nascimento));
                        command.Parameters.Add(new SqliteParameter("ano", ano_nascimento));

                        command.Transaction = transaction;

                        var rows = command.ExecuteNonQuery();

                        command.CommandText = "select last_insert_rowid();";

                        Int64 fkId64 = (Int64)command.ExecuteScalar();
                        int fkId = (int)fkId64;

                        command.CommandText = "INSERT INTO login (login,senha,jogador_id) VALUES (@login, @senha,@fkid);";
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqliteParameter("login", pLogin));
                        command.Parameters.Add(new SqliteParameter("senha", pSenha));
                        command.Parameters.Add(new SqliteParameter("fkid", fkId));

                        command.Transaction = transaction;

                        rows = command.ExecuteNonQuery();

                        transaction.Commit();
                        SceneManager.LoadScene(sceneName);
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        this.retornoTelaLogin.enabled = true;
                        this.retornoTelaLogin.text = "Esse login não está disponível";
                        print(e.Message);
                    }
                }
            }
        }
    }
}

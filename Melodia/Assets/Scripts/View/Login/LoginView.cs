using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginView : MonoBehaviour
{
    public InputField login;
    public InputField senha;
    public Dropdown sexo;
    public Dropdown dia;
    public Dropdown mes;
    public Dropdown ano;
    public Text retornoTelaLogin;
    public string sceneName;

    private LoginController controller;

    private void Start()
    {
        retornoTelaLogin.enabled = false;
        controller = new LoginController();
    }

    private bool Login()
    {
        string pLogin = this.login.text;
        string pSenha = this.senha.text;
        bool sucesso = false;

        Login login = controller.logar(pLogin, pSenha);

        if(login != null)
        {
            sucesso = true;
            controller.setAtivo(login.Id);
        }
        print(login.Usuario);


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

        string data_nascimento = ano_nascimento + "-" + mes_nascimento + "-" + dia_nascimento;

        string pLogin = this.login.text;
        string pSenha = this.senha.text;

        JogadorController jogadorController = new JogadorController();
        Jogador jogador = new Jogador();
        jogador.Sexo = sexo;
        jogador.DataNascimento = DateTime.Parse(data_nascimento);

        jogador = jogadorController.save(jogador);

        Login login = new Login();
        login.Usuario = pLogin;
        login.Senha = pSenha;
        login.Jogador = jogador;

        login = controller.save(login);

        SceneManager.LoadScene(sceneName);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    public GameObject[] elementos;
    public GridItem[] items;

    public Login usuario;
    public Partida ultimaPartida;
    public Partida partida;
    public Nivel nivel;
    private LoginController loginController;
    private PartidaController partidaController;
    private NivelController nivelController;

    // Start is called before the first frame update
    void Start()
    {
        loginController = new LoginController();
        partidaController = new PartidaController();
        nivelController = new NivelController();

        usuario = loginController.getAtivo();

        ultimaPartida = partidaController.getUltima(usuario.Jogador);

        CriarPartida();

        GetElementos();
        CreateGrid();
    }

    private void CriarPartida()
    {
        nivel = nivelController.getNext("NÍVEL 1", ultimaPartida);
        
        partida = partidaController.criarPartida(usuario.Jogador, nivel, 1);
    }

    private void CreateGrid()
    {
        items = new GridItem[3];


        items[0] = InstantiateElemento(Comportamento.PERGUNTA, "0", new Vector3(-5, 0));
        items[1] = InstantiateElemento(Comportamento.RESPOSTACERTA, "1", new Vector3(5, 1));
        items[2] = InstantiateElemento(Comportamento.RESPOSTAERRADA, "3", new Vector3(5, 3));


    }

    GridItem InstantiateElemento(Comportamento comportamento, string resource, Vector3 posicao)
    {
        GameObject elemento = elementos[Int32.Parse(resource)];
        print(elemento);
        GridItem newElemento = Instantiate(elemento, posicao, Quaternion.identity).GetComponent<GridItem>();
        
        newElemento.create(comportamento.ToString(), resource);
        return newElemento;
    }

  
    void GetElementos()
    {
        elementos = Resources.LoadAll<GameObject>("Elementos");
    }

    enum Comportamento
    {
        PERGUNTA,
        RESPOSTACERTA,
        RESPOSTAERRADA
    }
}

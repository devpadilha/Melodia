using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class GridManager : MonoBehaviour
{
    public Dictionary<int, Vector3> grid;
    private GameObject[] elementos;
    public GridItem[] items;
    public int index;
    private Login usuario;
    private Partida ultimaPartida;
    private Partida partida;
    private Nivel nivel;

    private LoginController loginController;
    private PartidaController partidaController;
    private NivelController nivelController;
    private ElementoController elementoController;

    // Start is called before the first frame update
    void Start()
    {
        loginController = new LoginController();
        partidaController = new PartidaController();
        nivelController = new NivelController();
        elementoController = new ElementoController();

        usuario = loginController.getAtivo();

        ultimaPartida = partidaController.getUltima(usuario.Jogador);

        CriarPartida();

        index = 0;

        montaGrid();
        GetElementos();
        CreateGrid();

        GridItem.OnMouseOverItemEventHandler += MouseClick;
    }

    private void CriarPartida()
    {
        nivel = nivelController.getNext(NivelEnum.Nivel.NIVEL1.ToString(), ultimaPartida);
        
        partida = partidaController.criarPartida(usuario.Jogador, nivel, 2);
    }

    private void CreateGrid()
    {
        RandomUtil randNum = new RandomUtil(2, 4);
        int rand;

        Desafio desafio = partida.Desafios[index];

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null)
            {
                Destroy(items[i].gameObject);
                items[i] = null;
            }
        }

        if (nivel.Dificuldade.Id.Equals((int)DificuldadeEnum.Dificuldade.FACIL))
        {
            List<Elemento> respostasErradas = elementoController.getErradasByDesafio(desafio, 1);
            items = new GridItem[3];

            items[0] = InstantiateElemento(Comportamento.PERGUNTA, desafio.Pergunta.Resource, grid[0]);

            rand = randNum.get();

            print(rand);

            items[1] = InstantiateElemento(Comportamento.RESPOSTACERTA, desafio.Resposta.Resource, grid[rand]);

            rand = randNum.get();

            print(rand);
            items[2] = InstantiateElemento(Comportamento.RESPOSTAERRADA, respostasErradas[0].Resource, grid[rand]);     
        } 
    }

    GridItem InstantiateElemento(Comportamento comportamento, string resource, Vector3 posicao)
    {
        GameObject elemento = elementos[Int32.Parse(resource)];
        GridItem newElemento = Instantiate(elemento, posicao, Quaternion.identity).GetComponent<GridItem>();
        
        newElemento.create(comportamento.ToString(), resource);
        return newElemento;
    }

  
    void GetElementos()
    {
        elementos = Resources.LoadAll<GameObject>("Elementos");
    }

    private void montaGrid()
    {
        grid = new Dictionary<int, Vector3>();
        grid.Add(0, new Vector3(-5, 0));
        grid.Add(1, new Vector3(5, 4));
        grid.Add(2, new Vector3(5, 2));
        grid.Add(3, new Vector3(5, 0));
        grid.Add(4, new Vector3(5, -2));
        grid.Add(5, new Vector3(5, -4));
    }

    void MouseClick(GridItem item)
    {
        GameObject[] reacoes = Resources.LoadAll<GameObject>("Reacoes");
        print(item.Comportamento);
        if (item.Comportamento.Equals(Comportamento.RESPOSTACERTA.ToString()))
        {
           
            for (int i = 0; i < items.Length; i++)
            {                            
                if (items[i] != null)
                {
                    Destroy(items[i].gameObject);
                    items[i] = null;
                }
            }

            GameObject elemento = reacoes[0];
            items[0] = Instantiate(elemento, new Vector3(0, 0), Quaternion.identity).GetComponent<GridItem>();

            index++;
            if(index < partida.Desafios.ToArray().Length)
            {
                Invoke(nameof(CreateGrid), 2.0f);
            }

        }
        else if(item.Comportamento.Equals(Comportamento.RESPOSTAERRADA.ToString()))
        {
            for (int i = 0; i < items.Length; i++)
            {
                Destroy(items[i].gameObject);
            }

            GameObject elemento = reacoes[1];
            items[0] = Instantiate(elemento, new Vector3(0, 0), Quaternion.identity).GetComponent<GridItem>();
            Invoke(nameof(CreateGrid), 2.0f);

        }
        
    }

    enum Comportamento
    {
        PERGUNTA,
        RESPOSTACERTA,
        RESPOSTAERRADA
    }
}

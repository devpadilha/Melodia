using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager2 : MonoBehaviour
{
    public Dictionary<int, Vector3> grid;
    private GameObject[] elementos;
    public GridItem2[] items;
    public GridItem2[] itemsQ;
    private GridItem2 ultimo;
    private GridItem2 atual;
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

        ultimo = null;

        montaGrid();
        GetElementos();
        CreateGrid();

        GridItem2.OnMouseOverItemEventHandler += MouseClick;
    }

    void MouseClick(GridItem2 item)
    {
        this.atual = item;
        
        if(ultimo==null || ultimo.Index != item.Index) {
            
            if (ultimo == null)
            {
                ultimo = item;
                items[item.Index].Rend.enabled = true;
                itemsQ[item.Index].Rend.enabled = false;            
            }
            else
            {
                items[item.Index].Rend.enabled = true;
                itemsQ[item.Index].Rend.enabled = false;
                

                if (items[ultimo.Index].Valor.Equals(items[item.Index].Valor))
                {
                    Invoke(nameof(limparPecas), 1.5f);                    
                }
                else
                {
                    Invoke(nameof(resetarTabuleiro), 1.5f);
                }
            }           
        }

    }

    private void limparPecas() 
    {
        bool fgFim = true;

        Destroy(items[ultimo.Index].gameObject);
        items[ultimo.Index] = null;
        Destroy(itemsQ[ultimo.Index].gameObject);
        itemsQ[ultimo.Index] = null;

        Destroy(items[atual.Index].gameObject);
        items[atual.Index] = null;
        Destroy(itemsQ[atual.Index].gameObject);
        itemsQ[atual.Index] = null;

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null)
            {                
                fgFim = false;
            }
        }


        if (fgFim)
        {            
            GameObject[] reacoes = Resources.LoadAll<GameObject>("Reacoes");
            GameObject elemento = reacoes[0];
            items[0] = Instantiate(elemento, new Vector3(0, 0), Quaternion.identity).GetComponent<GridItem2>();
        }
    }

    private void resetarTabuleiro()
    {
        ultimo = null;
        atual = null;
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null)
            {
                items[i].Rend.enabled = false;
                itemsQ[i].Rend.enabled = true;
            }
        }
    }



    private void CriarPartida()
    {
        nivel = nivelController.getNext(NivelEnum.Nivel.NIVEL2.ToString(), ultimaPartida);
        print(nivel.Descricao);

        partida = partidaController.criarPartida(usuario.Jogador, nivel, 2);
    }

    private void CreateGrid()
    {
        RandomUtil randNum = new RandomUtil(0, 4);
        int rand;

        List<Desafio> desafios = partida.Desafios;

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
            
            items = new GridItem2[4];
            itemsQ = new GridItem2[4];
           
            Desafio desafio = desafios[0];
            rand = randNum.get();
            items[rand] = InstantiateElemento(valor: desafio.Pergunta.Nome, resource: desafio.Pergunta.Resource, posicao: grid[rand], false, rand);
            rand = randNum.get();
            items[rand] = InstantiateElemento(valor: desafio.Resposta.Nome, resource: desafio.Resposta.Resource, posicao: grid[rand], false, rand);

            desafio = desafios[1];
            rand = randNum.get();
            items[rand] = InstantiateElemento(valor: desafio.Pergunta.Nome, resource: desafio.Pergunta.Resource, posicao: grid[rand], false, rand);
            rand = randNum.get();
            items[rand] = InstantiateElemento(valor: desafio.Resposta.Nome, resource: desafio.Resposta.Resource, posicao: grid[rand], false, rand);

            for (int i = 0; i < itemsQ.Length; i++)
            {
                itemsQ[i] = InstantiateElemento(valor: "DESCONHECIDO", resource: "0", posicao: grid[i], true, i);
            }
        }
    }

    GridItem2 InstantiateElemento(string valor, string resource, Vector3 posicao, bool render, int index)
    {
        GameObject elemento = elementos[Int32.Parse(resource)];
        GridItem2 newElemento = Instantiate(elemento, posicao, Quaternion.identity).GetComponent<GridItem2>();

        newElemento.create(valor, resource, render, index);
        return newElemento;
    }


    void GetElementos()
    {
        elementos = Resources.LoadAll<GameObject>("Nivel2");
    }

    private void montaGrid()
    {
        grid = new Dictionary<int, Vector3>();
        grid.Add(0, new Vector3(-1, 0));
        grid.Add(1, new Vector3(1, 0));
        grid.Add(2, new Vector3(-1, -2));
        grid.Add(3, new Vector3(1, -2));
        grid.Add(4, new Vector3(-3, 0));
        grid.Add(5, new Vector3(-3, -2));
        grid.Add(6, new Vector3(3, 0));
        grid.Add(7, new Vector3(3, -2));      
        
    }
}

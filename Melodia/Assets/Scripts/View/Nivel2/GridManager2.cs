using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GridManager2 : MonoBehaviour
{
    private int numErros;
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
    public ItemHud[] huds;
    public ItemHud[] vidas;

    private LoginController loginController;
    private PartidaController partidaController;
    private NivelController nivelController;
    private ElementoController elementoController;

    private bool fgClick;

    private AudioSource source;
    public AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        loginController = new LoginController();
        partidaController = new PartidaController();
        nivelController = new NivelController();
        elementoController = new ElementoController();

        source = GetComponent<AudioSource>();

        usuario = loginController.getAtivo();

        ultimaPartida = partidaController.getUltima(usuario.Jogador);

        CriarPartida();

        ultimo = null;

        montaGrid();
        GetElementos();
        CreateGrid();
        CriarHUD();

        fgClick = true;
        GridItem2.OnMouseOverItemEventHandler += MouseClick;
    }

    private void DecrementarErros()
    {
        numErros = numErros - 1;
        Debug.Log(numErros);

        Destroy(vidas[vidas.Length - 1].gameObject);
        Array.Resize(ref vidas, vidas.Length - 1);
    }

    void MouseClick(GridItem2 item)
    {
        if (fgClick)
        {
            fgClick = false;
            source.PlayOneShot(clip);

            this.atual = item;

            if (ultimo == null || ultimo.Index != item.Index)
            {

                if (ultimo == null)
                {
                    ultimo = item;
                    items[item.Index].Rend.enabled = true;
                    itemsQ[item.Index].Rend.enabled = false;
                    fgClick = true;
                }
                else
                {
                    items[item.Index].Rend.enabled = true;
                    itemsQ[item.Index].Rend.enabled = false;


                    if (items[ultimo.Index].Valor.Equals(items[item.Index].Valor))
                    {
                        Invoke(nameof(limparPecas), 1.5f);
                        partida.Acertos++;
                    }
                    else
                    {

                        DecrementarErros();
                        partida.Erros++;
                        if (numErros == 0)
                        {
                            Invoke(nameof(EncerrarPartida), 1.5f);
                        }                        
                        Invoke(nameof(resetarTabuleiro), 1.5f);
                    }
                }
            }
            
        }

    }

    private void CriarHUD()
    {
        GameObject[] icones = Resources.LoadAll<GameObject>("Hud");
        huds = new ItemHud[6];

        huds[0] = Instantiate(icones[0], new Vector3(2, 4), Quaternion.identity).GetComponent<ItemHud>();
        huds[0].create("MENU", "0");

        huds[1] = Instantiate(icones[1], new Vector3(6, 4), Quaternion.identity).GetComponent<ItemHud>();
        huds[1].create("SAIR", "1");

        if (nivel.Dificuldade.Id.Equals((int)DificuldadeEnum.Dificuldade.FACIL))
        {
            huds[2] = Instantiate(icones[3], new Vector3(-8, -4.5f), Quaternion.identity).GetComponent<ItemHud>();
        }
        else if (nivel.Dificuldade.Id.Equals((int)DificuldadeEnum.Dificuldade.MEDIO))
        {
            huds[2] = Instantiate(icones[3], new Vector3(-8, -4.5f), Quaternion.identity).GetComponent<ItemHud>();
            huds[3] = Instantiate(icones[3], new Vector3(-7, -4.5f), Quaternion.identity).GetComponent<ItemHud>();
        }
        else if (nivel.Dificuldade.Id.Equals((int)DificuldadeEnum.Dificuldade.DIFICIL))
        {
            huds[2] = Instantiate(icones[3], new Vector3(-8, -4.5f), Quaternion.identity).GetComponent<ItemHud>();
            huds[4] = Instantiate(icones[3], new Vector3(-7, -4.5f), Quaternion.identity).GetComponent<ItemHud>();
            huds[5] = Instantiate(icones[3], new Vector3(-6, -4.5f), Quaternion.identity).GetComponent<ItemHud>();
        }


        Dictionary<int, Vector3> gridVidas = new Dictionary<int, Vector3>();
        gridVidas.Add(0, new Vector3(-8, 4));
        gridVidas.Add(1, new Vector3(-7, 4));
        gridVidas.Add(2, new Vector3(-6, 4));
        gridVidas.Add(3, new Vector3(-5, 4));
        gridVidas.Add(4, new Vector3(-4, 4));
        vidas = new ItemHud[numErros];
        for (int i = 0; i < numErros; i++)
        {
            vidas[i] = Instantiate(icones[2], gridVidas[i], Quaternion.identity).GetComponent<ItemHud>();
        }
    }

    private void EncerrarPartida()
    {
        if (partida.Acertos >= nivel.MinAcertos)
        {
            partida.Concluido = true;
        }

        Debug.Log(partida.Acertos + " - " + nivel.MinAcertos);

        partidaController.encerrarPartida(partida);
        GridItem2.OnMouseOverItemEventHandler -= MouseClick;

        if (partida.Acertos >= nivel.MinAcertos)
        {
            SceneManager.LoadScene("NivelCompleto");
        }
        else
        {
            SceneManager.LoadScene("NivelFracasso");
        }
        fgClick = true;
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
            Invoke(nameof(EncerrarPartida), 1.5f);
        }
        fgClick = true;
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
        fgClick = true;
    }



    private void CriarPartida()
    {
        partida = partidaController.getAtual(usuario.Jogador, NivelEnum.Nivel.NIVEL2.ToString());

        if (partida == null)
        {
            nivel = nivelController.getNext(NivelEnum.Nivel.NIVEL2.ToString(), ultimaPartida);

            if (nivel.Dificuldade.Id.Equals((int)DificuldadeEnum.Dificuldade.FACIL))
            {
                partida = partidaController.criarPartida(usuario.Jogador, nivel, 2);
            }
            else if (nivel.Dificuldade.Id.Equals((int)DificuldadeEnum.Dificuldade.MEDIO))
            {
                partida = partidaController.criarPartida(usuario.Jogador, nivel, 3);
            }
            else if (nivel.Dificuldade.Id.Equals((int)DificuldadeEnum.Dificuldade.DIFICIL))
            {
                partida = partidaController.criarPartida(usuario.Jogador, nivel, 4);
            }
        }
        else
        {
            nivel = partida.Nivel;
        }

        numErros = nivel.MaxErros;
    }

    private void CreateGrid()
    {
        
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
            RandomUtil randNum = new RandomUtil(0, 4);
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
        else if (nivel.Dificuldade.Id.Equals((int)DificuldadeEnum.Dificuldade.MEDIO))
        {
            RandomUtil randNum = new RandomUtil(0, 6);
            items = new GridItem2[6];
            itemsQ = new GridItem2[6];

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

            desafio = desafios[2];
            rand = randNum.get();
            items[rand] = InstantiateElemento(valor: desafio.Pergunta.Nome, resource: desafio.Pergunta.Resource, posicao: grid[rand], false, rand);
            rand = randNum.get();
            items[rand] = InstantiateElemento(valor: desafio.Resposta.Nome, resource: desafio.Resposta.Resource, posicao: grid[rand], false, rand);

            for (int i = 0; i < itemsQ.Length; i++)
            {
                itemsQ[i] = InstantiateElemento(valor: "DESCONHECIDO", resource: "0", posicao: grid[i], true, i);
            }
        }
        else if (nivel.Dificuldade.Id.Equals((int)DificuldadeEnum.Dificuldade.DIFICIL))
        {
            RandomUtil randNum = new RandomUtil(0, 8);
            items = new GridItem2[8];
            itemsQ = new GridItem2[8];

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

            desafio = desafios[2];
            rand = randNum.get();
            items[rand] = InstantiateElemento(valor: desafio.Pergunta.Nome, resource: desafio.Pergunta.Resource, posicao: grid[rand], false, rand);
            rand = randNum.get();
            items[rand] = InstantiateElemento(valor: desafio.Resposta.Nome, resource: desafio.Resposta.Resource, posicao: grid[rand], false, rand);

            desafio = desafios[3];
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
        grid.Add(0, new Vector3(-1.5f, 1));
        grid.Add(1, new Vector3(1.5f, 1));
        grid.Add(2, new Vector3(-1.5f, -2));
        grid.Add(3, new Vector3(1.5f, -2));
        grid.Add(4, new Vector3(-4.5f, 1));
        grid.Add(5, new Vector3(-4.5f, -2));
        grid.Add(6, new Vector3(4.5f, 1));
        grid.Add(7, new Vector3(4.5f, -2));      
        
    }
}

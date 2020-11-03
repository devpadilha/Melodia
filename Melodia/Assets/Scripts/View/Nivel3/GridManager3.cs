using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GridManager3 : MonoBehaviour
{
    private int numErros;
    public Dictionary<int, Vector3> grid;
    private GameObject[] elementos;
    public GridItem3[] items;
    public Text pergunta;
    private int index;
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

        index = 0;

        source = GetComponent<AudioSource>();

        usuario = loginController.getAtivo();

        ultimaPartida = partidaController.getUltima(usuario.Jogador);

        CriarPartida();

        montaGrid();
        GetElementos();
        CreateGrid();
        CriarHUD();

        fgClick = true;
        GridItem3.OnMouseOverItemEventHandler += MouseClick;
    }

    private void DecrementarErros()
    {
        numErros = numErros - 1;
        Debug.Log(numErros);

        Destroy(vidas[vidas.Length - 1].gameObject);
        Array.Resize(ref vidas, vidas.Length - 1);
    }

    void MouseClick(GridItem3 item)
    {
        if (fgClick)
        {            
            fgClick = false;
            source.PlayOneShot(clip);

            if (item.Comportamento.Equals(Comportamento.RESPOSTACERTA.ToString()))
            {
                SpriteRenderer sprite = item.GetComponent<SpriteRenderer>();
                Color color = sprite.color;
                color.a = 0.5f;
                sprite.color = color;
                partida.Acertos++;
                if(nivel.MinAcertos == partida.Acertos)
                {
                    EncerrarPartida();
                }
            }
            else if (item.Comportamento.Equals(Comportamento.RESPOSTAERRADA.ToString()))
            {
                DecrementarErros();
                partida.Erros++;
                if (numErros == 0)
                {
                    Invoke(nameof(EncerrarPartida), 1.5f);
                }
            }

            fgClick = true;
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
        ItemHud.OnMouseOverItemEventHandler += HudClick;
    }

    private void HudClick(ItemHud item)
    {
        switch (item.Comportamento)
        {
            case "SAIR":
                ItemHud.OnMouseOverItemEventHandler -= HudClick;
                GridItem3.OnMouseOverItemEventHandler -= MouseClick;
                Debug.Log("Saindo...");
                Application.Quit();
                break;
            case "MENU":
                ItemHud.OnMouseOverItemEventHandler -= HudClick;
                GridItem3.OnMouseOverItemEventHandler -= MouseClick;
                SceneManager.LoadScene("MainMenu");
                break;
        }
    }

    private void EncerrarPartida()
    {
        if (partida.Acertos >= nivel.MinAcertos)
        {
            partida.Concluido = true;
        }

        partidaController.encerrarPartida(partida);
        GridItem3.OnMouseOverItemEventHandler -= MouseClick;

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



    private void CriarPartida()
    {
        partida = partidaController.getAtual(usuario.Jogador, NivelEnum.Nivel.NIVEL3.ToString());

        if (partida == null)
        {
            nivel = nivelController.getNext(NivelEnum.Nivel.NIVEL3.ToString(), ultimaPartida);

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

        items = new GridItem3[12];

        if (nivel.Dificuldade.Id.Equals((int)DificuldadeEnum.Dificuldade.FACIL))
        {
            RandomUtil randNum = new RandomUtil(0, items.Length);

            Desafio desafio = desafios[index];

            pergunta.text = desafio.Descricao;

            List<Elemento> respostasErradas = elementoController.getErradasByDesafio(desafio, 5);

            for(int i=0; i<2; i++)
            {
                rand = randNum.get();
                items[rand] = InstantiateElemento(Comportamento.RESPOSTACERTA, desafio.Resposta.Resource, grid[rand]);
            }

            for (int k = 0; k < respostasErradas.ToArray().Length; k++)
            {
                for (int i = 0; i < 2; i++)
                {
                    rand = randNum.get();
                    items[rand] = InstantiateElemento(Comportamento.RESPOSTAERRADA, respostasErradas[k].Resource, grid[rand]);
                }
            }
        }
        else if (nivel.Dificuldade.Id.Equals((int)DificuldadeEnum.Dificuldade.MEDIO))
        {
            RandomUtil randNum = new RandomUtil(0, items.Length);

            Desafio desafio = desafios[index];

            pergunta.text = desafio.Descricao;

            List<Elemento> respostasErradas = elementoController.getErradasByDesafio(desafio, 5);

            for (int i = 0; i < 3; i++)
            {
                rand = randNum.get();
                items[rand] = InstantiateElemento(Comportamento.RESPOSTACERTA, desafio.Resposta.Resource, grid[rand]);
            }

            for (int k = 0; k < respostasErradas.ToArray().Length - 1; k++)
            {
                for (int i = 0; i < 2; i++)
                {
                    rand = randNum.get();
                    items[rand] = InstantiateElemento(Comportamento.RESPOSTAERRADA, respostasErradas[k].Resource, grid[rand]);
                }
            }

            rand = randNum.get();
            items[rand] = InstantiateElemento(Comportamento.RESPOSTAERRADA, respostasErradas[respostasErradas.ToArray().Length-1].Resource, grid[rand]);
        }
        else if (nivel.Dificuldade.Id.Equals((int)DificuldadeEnum.Dificuldade.DIFICIL))
        {
            RandomUtil randNum = new RandomUtil(0, items.Length);

            Desafio desafio = desafios[index];

            pergunta.text = desafio.Descricao;

            List<Elemento> respostasErradas = elementoController.getErradasByDesafio(desafio, 4);

            for (int i = 0; i < 4; i++)
            {
                rand = randNum.get();
                items[rand] = InstantiateElemento(Comportamento.RESPOSTACERTA, desafio.Resposta.Resource, grid[rand]);
            }

            for (int k = 0; k < respostasErradas.ToArray().Length; k++)
            {
                for (int i = 0; i < 2; i++)
                {
                    rand = randNum.get();
                    items[rand] = InstantiateElemento(Comportamento.RESPOSTAERRADA, respostasErradas[k].Resource, grid[rand]);
                }
            }
        }

    }

    GridItem3 InstantiateElemento(Comportamento comportamento, string resource, Vector3 posicao)
    {
        GameObject elemento = elementos[Int32.Parse(resource)];
        GridItem3 newElemento = Instantiate(elemento, posicao, Quaternion.identity).GetComponent<GridItem3>();

        newElemento.create(comportamento.ToString(), resource);
        return newElemento;
    }


    void GetElementos()
    {
        elementos = Resources.LoadAll<GameObject>("Nivel3");
    }

    private void montaGrid()
    {
        grid = new Dictionary<int, Vector3>();

        grid.Add(0, new Vector3(-7.5f, 0.5f));
        grid.Add(1, new Vector3(-4.5f, 0.5f));
        grid.Add(2, new Vector3(-1.5f, 0.5f));
        grid.Add(3, new Vector3(1.5f, 0.5f));        
        grid.Add(4, new Vector3(4.5f, 0.5f));
        grid.Add(5, new Vector3(7.5f, 0.5f));

        grid.Add(6, new Vector3(-7.5f, -2.5f));
        grid.Add(7, new Vector3(-4.5f, -2.5f));
        grid.Add(8, new Vector3(-1.5f, -2.5f));
        grid.Add(9, new Vector3(1.5f, -2.5f));           
        grid.Add(10, new Vector3(4.5f, -2.5f));
        grid.Add(11, new Vector3(7.5f, -2.5f));

        
    }

    enum Comportamento
    {
        RESPOSTACERTA,
        RESPOSTAERRADA,
        NENHUM
    }
}

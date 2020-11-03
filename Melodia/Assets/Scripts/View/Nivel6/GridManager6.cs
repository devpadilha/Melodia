using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public class GridManager6 : MonoBehaviour
{

    private int numErros;
    public ItemHud[] vidas;
    public Dictionary<int, Vector3> grid;
    public GameObject[] elementos;
    public GridItem6[] items;
    public ItemHud[] huds;
    public int index;
    private Login usuario;
    private Partida ultimaPartida;
    private Partida partida;
    private Nivel nivel;
    private bool fgClick;

    private LoginController loginController;
    private PartidaController partidaController;
    private NivelController nivelController;
    private ElementoController elementoController;

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

        index = 0;

        items = new GridItem6[10];

        montaGrid();
        GetElementos();
        IniciarTreinamento();
        CriarHUD();
        fgClick = true;
        GridItem6.OnMouseOverItemEventHandler += MouseClick;
    }

    private void DecrementarErros()
    {
        numErros = numErros - 1;
        Debug.Log(numErros);

        Destroy(vidas[vidas.Length - 1].gameObject);
        Array.Resize(ref vidas, vidas.Length - 1);
    }

    private void CriarPartida()
    {
        partida = partidaController.getAtual(usuario.Jogador, NivelEnum.Nivel.NIVEL6.ToString());

        if (partida == null)
        {
            nivel = nivelController.getNext(NivelEnum.Nivel.NIVEL6.ToString(), ultimaPartida);
            partida = partidaController.criarPartida(usuario.Jogador, nivel, 5);
        }
        else
        {
            nivel = partida.Nivel;
        }

        numErros = nivel.MaxErros;
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

    private void CreateGrid()
    {

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
            RandomUtil randNum = new RandomUtil(1, 3);
            List<Elemento> respostasErradas = elementoController.getErradasByDesafio(desafio, 1);

            items[0] = InstantiateElemento(Comportamento.NENHUM, "0", grid[5]);

            items[1] = InstantiateElemento(Comportamento.PERGUNTA, desafio.Pergunta.Resource, grid[0]);

            rand = randNum.get();

            items[2] = InstantiateElemento(Comportamento.RESPOSTACERTA, desafio.Resposta.Resource, grid[rand]);

            rand = randNum.get();

            items[3] = InstantiateElemento(Comportamento.RESPOSTAERRADA, respostasErradas[0].Resource, grid[rand]);
        }
        if (nivel.Dificuldade.Id.Equals((int)DificuldadeEnum.Dificuldade.MEDIO))
        {
            RandomUtil randNum = new RandomUtil(1, 3);
            List<Elemento> respostasErradas = elementoController.getErradasByDesafio(desafio, 1);

            items[0] = InstantiateElemento(Comportamento.NENHUM, "0", grid[5]);

            items[1] = InstantiateElemento(Comportamento.PERGUNTA, desafio.Pergunta.Resource, grid[0]);

            rand = randNum.get();

            items[2] = InstantiateElemento(Comportamento.RESPOSTACERTA, desafio.Resposta.Resource, grid[rand]);

            rand = randNum.get();

            items[3] = InstantiateElemento(Comportamento.RESPOSTAERRADA, respostasErradas[0].Resource, grid[rand]);

            /*rand = randNum.get();

            items[4] = InstantiateElemento(Comportamento.RESPOSTAERRADA, respostasErradas[1].Resource, grid[rand]);*/
        }
        if (nivel.Dificuldade.Id.Equals((int)DificuldadeEnum.Dificuldade.DIFICIL))
        {
            RandomUtil randNum = new RandomUtil(1, 3);
            List<Elemento> respostasErradas = elementoController.getErradasByDesafio(desafio, 1);

            items[0] = InstantiateElemento(Comportamento.NENHUM, "0", grid[5]);

            items[1] = InstantiateElemento(Comportamento.PERGUNTA, desafio.Pergunta.Resource, grid[0]);

            rand = randNum.get();

            items[2] = InstantiateElemento(Comportamento.RESPOSTACERTA, desafio.Resposta.Resource, grid[rand]);

            rand = randNum.get();

            items[3] = InstantiateElemento(Comportamento.RESPOSTAERRADA, respostasErradas[0].Resource, grid[rand]);

            /*rand = randNum.get();

            items[4] = InstantiateElemento(Comportamento.RESPOSTAERRADA, respostasErradas[1].Resource, grid[rand]);

            rand = randNum.get();

            items[5] = InstantiateElemento(Comportamento.RESPOSTAERRADA, respostasErradas[2].Resource, grid[rand]);*/
        }
        fgClick = true;
    }

    private void CreateGridTreinamento()
    {
        Desafio desafio = partida.Desafios[index];

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null)
            {
                Destroy(items[i].gameObject);
                items[i] = null;
            }
        }


        items[0] = InstantiateElemento(Comportamento.NENHUM, "04", new Vector3(-8, -1));
        items[1] = InstantiateElemento(Comportamento.NENHUM, "00", new Vector3(-6.5f, -1));
        items[2] = InstantiateElemento(Comportamento.NENHUM, "01", new Vector3(-5, -1));

        items[3] = InstantiateElemento(Comportamento.NENHUM, "05", new Vector3(-3, -1));
        items[4] = InstantiateElemento(Comportamento.NENHUM, "00", new Vector3(-1.5f, -1));
        items[5] = InstantiateElemento(Comportamento.NENHUM, "02", new Vector3(0, -1));

        items[6] = InstantiateElemento(Comportamento.NENHUM, "06", new Vector3(2, -1));
        items[7] = InstantiateElemento(Comportamento.NENHUM, "00", new Vector3(3.5f, -1));
        items[8] = InstantiateElemento(Comportamento.NENHUM, "03", new Vector3(5, -1));

        items[9] = InstantiateElemento(Comportamento.TREINAMENTONEXT, "09", grid[6]);

        fgClick = true;
    }

    GridItem6 InstantiateElemento(Comportamento comportamento, string resource, Vector3 posicao)
    {
        GameObject elemento = elementos[Int32.Parse(resource)];
        GridItem6 newElemento = Instantiate(elemento, posicao, Quaternion.identity).GetComponent<GridItem6>();

        newElemento.create(comportamento.ToString(), resource);
        return newElemento;
    }


    void GetElementos()
    {
        elementos = Resources.LoadAll<GameObject>("Nivel6");
    }

    private void montaGrid()
    {
        grid = new Dictionary<int, Vector3>();
        grid.Add(0, new Vector3(-7, -1));
        grid.Add(1, new Vector3(4, 0));
        grid.Add(2, new Vector3(4, -2));
        grid.Add(3, new Vector3(4, 2));
        grid.Add(4, new Vector3(4, -4));
        grid.Add(5, new Vector3(-2, -1));
        grid.Add(6, new Vector3(8, -1));
    }

    void MouseClick(GridItem6 item)
    {
        if (fgClick)
        {
            fgClick = false;
            GameObject[] reacoes = Resources.LoadAll<GameObject>("Reacoes");

            if (item.Comportamento.Equals(Comportamento.RESPOSTACERTA.ToString()))
            {

                source.PlayOneShot(clip);
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i] != null)
                    {
                        Destroy(items[i].gameObject);
                        items[i] = null;
                    }
                }

                GameObject elemento = reacoes[0];
                items[0] = Instantiate(elemento, new Vector3(0, 0), Quaternion.identity).GetComponent<GridItem6>();

                partida.Acertos++;

                index++;
                if (index < partida.Desafios.ToArray().Length)
                {
                    Invoke(nameof(CreateGrid), 2.0f);
                }
                else
                {
                    Invoke(nameof(EncerrarPartida), 2.0f);
                }

            }
            else if (item.Comportamento.Equals(Comportamento.RESPOSTAERRADA.ToString()))
            {

                source.PlayOneShot(clip);
                DecrementarErros();
                if (numErros == 0)
                {
                    Invoke(nameof(EncerrarPartida), 0f);
                }
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i] != null)
                    {
                        Destroy(items[i].gameObject);
                        items[i] = null;
                    }
                }

                GameObject elemento = reacoes[1];
                items[0] = Instantiate(elemento, new Vector3(0, 0), Quaternion.identity).GetComponent<GridItem6>();

                partida.Erros++;

                Invoke(nameof(CreateGrid), 2.0f);

            }
            else if (item.Comportamento.Equals(Comportamento.TREINAMENTONEXT.ToString()))
            {
                source.PlayOneShot(clip);        
                
                index = 0;
                Invoke(nameof(IniciarPartida), 0.5f);
                
            }

        }

    }

    private void EncerrarPartida()
    {
        if (partida.Acertos >= nivel.MinAcertos)
        {
            partida.Concluido = true;
        }

        partidaController.encerrarPartida(partida);
        GridItem6.OnMouseOverItemEventHandler -= MouseClick;

        if (partida.Acertos >= nivel.MinAcertos)
        {
            SceneManager.LoadScene("NivelCompleto");
        }
        else
        {
            SceneManager.LoadScene("NivelFracasso");
        }

    }

    private void IniciarTreinamento()
    {
        GameObject[] textos = Resources.LoadAll<GameObject>("Texto");

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null)
            {
                Destroy(items[i].gameObject);
                items[i] = null;
            }
        }

        items[0] = Instantiate(textos[0], new Vector3(0, 0), Quaternion.identity).GetComponent<GridItem6>();
        Invoke(nameof(CreateGridTreinamento), 2f);
    }

    private void IniciarPartida()
    {
        GameObject[] textos = Resources.LoadAll<GameObject>("Texto");

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null)
            {
                Destroy(items[i].gameObject);
                items[i] = null;
            }
        }

        items[0] = Instantiate(textos[1], new Vector3(0, 0), Quaternion.identity).GetComponent<GridItem6>();
        Invoke(nameof(CreateGrid), 2f);
    }

    enum Comportamento
    {
        PERGUNTA,
        RESPOSTACERTA,
        RESPOSTAERRADA,
        NENHUM,
        TREINAMENTONEXT
    }
}

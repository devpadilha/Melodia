using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GridManager5 : MonoBehaviour
{

    private int numErros;
    public ItemHud[] vidas;
    public Dictionary<int, Vector3> grid;
    public Dictionary<int, Vector3> gridPauta;
    public GameObject[] elementos;
    public GameObject[] elementosTreinamento;
    public GridItem5[] items;
    public ItemHud[] huds;
    public int index;
    private Login usuario;
    private Partida ultimaPartida;
    private Partida partida;
    private Nivel nivel;
    private bool fgClick;
    public Text campoPergunta;

    private LoginController loginController;
    private PartidaController partidaController;
    private NivelController nivelController;
    private ElementoController elementoController;

    private AudioSource source;
    public AudioClip clip;

    public AudioSource backgroudSound;

    // Start is called before the first frame update
    void Start()
    {
        loginController = new LoginController();
        partidaController = new PartidaController();
        nivelController = new NivelController();
        elementoController = new ElementoController();

        source = GetComponent<AudioSource>();

        usuario = loginController.getAtivo();

        CriarPartida();

        index = 0;

        items = new GridItem5[20];

        montaGrid();
        GetElementos();
        IniciarTreinamento();        
        CriarHUD();
        fgClick = true;
        GridItem5.OnMouseOverItemEventHandler += MouseClick;
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


        items[0] = InstantiateElemento(Comportamento.NENHUM, elementosTreinamento[index], grid[0]);
        items[1] = InstantiateElemento(Comportamento.TREINAMENTONEXT, "0", grid[6]);
        fgClick = true;
    }


    private void IniciarTreinamento()
    {
        GameObject[] textos = Resources.LoadAll<GameObject>("Texto");
        campoPergunta.enabled = false;

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null)
            {
                Destroy(items[i].gameObject);
                items[i] = null;
            }
        }

        items[0] = Instantiate(textos[0], new Vector3(0, 0), Quaternion.identity).GetComponent<GridItem5>();
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

        items[0] = Instantiate(textos[1], new Vector3(0, 0), Quaternion.identity).GetComponent<GridItem5>();
        Invoke(nameof(CreateGrid), 2f);
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
        nivel = nivelController.get(NivelEnum.Nivel.NIVEL5.ToString());
        ultimaPartida = partidaController.getUltimaNivel(usuario.Jogador, nivel);
        partida = partidaController.getAtual(usuario.Jogador, NivelEnum.Nivel.NIVEL5.ToString());

        if (partida == null)
        {
            nivel = nivelController.getNext(NivelEnum.Nivel.NIVEL5.ToString(), ultimaPartida);
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
        huds = new ItemHud[7];

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

        string som = PlayerPrefs.GetString("SOM");
        if (som.Equals("ON"))
        {
            backgroudSound.UnPause();
            huds[6] = Instantiate(icones[4], new Vector3(-1, 4), Quaternion.identity).GetComponent<ItemHud>();
            huds[6].create("SOMOFF", "4");
        }
        else
        {
            backgroudSound.Pause();
            huds[6] = Instantiate(icones[5], new Vector3(-1, 4), Quaternion.identity).GetComponent<ItemHud>();
            huds[6].create("SOMON", "5");
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
        GameObject[] icones = Resources.LoadAll<GameObject>("Hud");
        switch (item.Comportamento)
        {
            case "SAIR":
                ItemHud.OnMouseOverItemEventHandler -= HudClick;
                GridItem5.OnMouseOverItemEventHandler -= MouseClick;
                Debug.Log("Saindo...");
                Application.Quit();
                break;
            case "MENU":
                ItemHud.OnMouseOverItemEventHandler -= HudClick;
                GridItem5.OnMouseOverItemEventHandler -= MouseClick;
                SceneManager.LoadScene("MainMenu");
                break;

            case "SOMOFF":
                backgroudSound.Pause();
                PlayerPrefs.SetString("SOM", "OFF");
                Destroy(huds[6].gameObject);
                huds[6] = Instantiate(icones[5], new Vector3(-1, 4), Quaternion.identity).GetComponent<ItemHud>();
                huds[6].create("SOMON", "5");
                break;

            case "SOMON":
                backgroudSound.UnPause();
                PlayerPrefs.SetString("SOM", "ON");
                Destroy(huds[6].gameObject);
                huds[6] = Instantiate(icones[4], new Vector3(-1, 4), Quaternion.identity).GetComponent<ItemHud>();
                huds[6].create("SOMOFF", "4");
                break;
        }
    }

    private void CreateGrid()
    {
        Vector3 pauta = new Vector3(0,0);
        int rand;

        Desafio desafio = partida.Desafios[index];
        campoPergunta.enabled = true;

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null)
            {
                Destroy(items[i].gameObject);
                items[i] = null;
            }
        }

        if (desafio.Pergunta.Nome.Equals("PAUTA CLAVE DE SOL"))
        {
            pauta = new Vector3(-1, -1.39f);
        }
        else if(desafio.Pergunta.Nome.Equals("PAUTA CLAVE DE DÓ"))
        {
            pauta = new Vector3(-1, -1.21f);
        }
        else if (desafio.Pergunta.Nome.Equals("PAUTA CLAVE DE FÁ"))
        {
            pauta = new Vector3(-1, -1.2f);
        }

        if (nivel.Dificuldade.Id.Equals((int)DificuldadeEnum.Dificuldade.FACIL))
        {
            RandomUtil randNum = new RandomUtil(1, 3);
            List<Elemento> respostasErradas = elementoController.getErradasByDesafio(desafio, 1);        

            items[0] = InstantiateElemento(Comportamento.PERGUNTA, desafio.Pergunta.Resource, pauta);

            rand = randNum.get();
            items[1] = InstantiateElemento(Comportamento.RESPOSTACERTA, desafio.Resposta.Resource, grid[rand]);

            rand = randNum.get();
            items[2] = InstantiateElemento(Comportamento.RESPOSTAERRADA, respostasErradas[0].Resource, grid[rand]);

            int pos = Int32.Parse(desafio.Resposta.Nome);

            if (pos != 1)
            {
                items[3] = InstantiateElemento(Comportamento.NENHUM, "07", gridPauta[pos]);
            }

            pos = pos + 7;          

            if (gridPauta.ContainsKey(pos))
            {
                items[4] = InstantiateElemento(Comportamento.NENHUM, "07", gridPauta[pos]);
            }
            

        }
        if (nivel.Dificuldade.Id.Equals((int)DificuldadeEnum.Dificuldade.MEDIO))
        {
            RandomUtil randNum = new RandomUtil(1, 4);
            List<Elemento> respostasErradas = elementoController.getErradasByDesafio(desafio, 2);

            items[0] = InstantiateElemento(Comportamento.PERGUNTA, desafio.Pergunta.Resource, pauta);

            rand = randNum.get();
            items[1] = InstantiateElemento(Comportamento.RESPOSTACERTA, desafio.Resposta.Resource, grid[rand]);

            rand = randNum.get();
            items[2] = InstantiateElemento(Comportamento.RESPOSTAERRADA, respostasErradas[0].Resource, grid[rand]);

            rand = randNum.get();
            items[3] = InstantiateElemento(Comportamento.RESPOSTAERRADA, respostasErradas[1].Resource, grid[rand]);

            int pos = Int32.Parse(desafio.Resposta.Nome);

            if (pos != 1)
            {
                items[4] = InstantiateElemento(Comportamento.NENHUM, "07", gridPauta[pos]);
            }

            pos = pos + 7;

            if (gridPauta.ContainsKey(pos))
            {
                items[5] = InstantiateElemento(Comportamento.NENHUM, "07", gridPauta[pos]);
            }


        }
        if (nivel.Dificuldade.Id.Equals((int)DificuldadeEnum.Dificuldade.DIFICIL))
        {
            RandomUtil randNum = new RandomUtil(1, 5);
            List<Elemento> respostasErradas = elementoController.getErradasByDesafio(desafio, 3);

            items[0] = InstantiateElemento(Comportamento.PERGUNTA, desafio.Pergunta.Resource, pauta);

            rand = randNum.get();
            items[1] = InstantiateElemento(Comportamento.RESPOSTACERTA, desafio.Resposta.Resource, grid[rand]);

            rand = randNum.get();
            items[2] = InstantiateElemento(Comportamento.RESPOSTAERRADA, respostasErradas[0].Resource, grid[rand]);

            rand = randNum.get();
            items[3] = InstantiateElemento(Comportamento.RESPOSTAERRADA, respostasErradas[1].Resource, grid[rand]);

            rand = randNum.get();
            items[4] = InstantiateElemento(Comportamento.RESPOSTAERRADA, respostasErradas[2].Resource, grid[rand]);

            int pos = Int32.Parse(desafio.Resposta.Nome);

            if (pos != 1)
            {
                items[5] = InstantiateElemento(Comportamento.NENHUM, "07", gridPauta[pos]);
            }

            pos = pos + 7;

            if (gridPauta.ContainsKey(pos))
            {
                items[6] = InstantiateElemento(Comportamento.NENHUM, "07", gridPauta[pos]);
            }


        }
        fgClick = true;
    }
    

    GridItem5 InstantiateElemento(Comportamento comportamento, string resource, Vector3 posicao)
    {
        GameObject elemento = elementos[Int32.Parse(resource)];  
        return InstantiateElemento(comportamento, elemento, posicao);
    }

    GridItem5 InstantiateElemento(Comportamento comportamento, GameObject elemento, Vector3 posicao)
    {
        GridItem5 newElemento = Instantiate(elemento, posicao, Quaternion.identity).GetComponent<GridItem5>();

        newElemento.create(comportamento.ToString(), "00");
        return newElemento;
    }


    void GetElementos()
    {
        elementos = Resources.LoadAll<GameObject>("Nivel5");
        elementosTreinamento = new GameObject[3];
        elementosTreinamento[0] = elementos[1];
        elementosTreinamento[1] = elementos[2];
        elementosTreinamento[2] = elementos[3];
    }

    private void montaGrid()
    {
        grid = new Dictionary<int, Vector3>();
        grid.Add(0, new Vector3(-1, -1.39f));
        grid.Add(1, new Vector3(-1, -4));
        grid.Add(2, new Vector3(1, -4));
        grid.Add(3, new Vector3(-3, -4));
        grid.Add(4, new Vector3(3, -4));
        grid.Add(5, new Vector3(-2, -1));
        grid.Add(6, new Vector3(8, -1));

        gridPauta = new Dictionary<int, Vector3>();
        gridPauta.Add(1, new Vector3(-6.48f, -2.18f));
        gridPauta.Add(2, new Vector3(-5.36f, -1.92f));
        gridPauta.Add(3, new Vector3(-4.23f, -1.67f));
        gridPauta.Add(4, new Vector3(-3.08f, -1.50f));
        gridPauta.Add(5, new Vector3(-1.92f, -1.36f));
        gridPauta.Add(6, new Vector3(-0.82f, -1.14f));
        gridPauta.Add(7, new Vector3(0.32f, -0.90f));
        gridPauta.Add(8, new Vector3(1.50f, -0.67f));
        gridPauta.Add(9, new Vector3(2.60f, -0.55f));
        gridPauta.Add(10, new Vector3(3.72f, -0.32f));
        gridPauta.Add(11, new Vector3(4.89f, 0.00f));
        gridPauta.Add(12, new Vector3(6f, 0.15f));
    }

    void MouseClick(GridItem5 item)
    {
        if (fgClick)
        {
            fgClick = false;
            GameObject[] reacoes = Resources.LoadAll<GameObject>("Reacoes");
            campoPergunta.enabled = false;

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
                items[0] = Instantiate(elemento, new Vector3(0, 0), Quaternion.identity).GetComponent<GridItem5>();

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
                items[0] = Instantiate(elemento, new Vector3(0, 0), Quaternion.identity).GetComponent<GridItem5>();

                partida.Erros++;

                Invoke(nameof(CreateGrid), 2.0f);

            }
            else if (item.Comportamento.Equals(Comportamento.TREINAMENTONEXT.ToString()))
            {
                source.PlayOneShot(clip);
                index++;
                if (index < elementosTreinamento.Length)
                {
                    Invoke(nameof(CreateGridTreinamento), 0f);
                }
                else
                {
                    index = 0;
                    Invoke(nameof(IniciarPartida), 0.5f);
                }
            }
            else
            {
                fgClick = true;
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
        GridItem5.OnMouseOverItemEventHandler -= MouseClick;
        ItemHud.OnMouseOverItemEventHandler -= HudClick;

        if (partida.Acertos >= nivel.MinAcertos)
        {
            SceneManager.LoadScene("NivelCompleto");
        }
        else
        {
            SceneManager.LoadScene("NivelFracasso");
        }

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
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GridManager7 : MonoBehaviour
{
    private int numErros;
    public Dictionary<int, Vector3> grid;
    private GameObject[] elementos;
    public GridItem7[] items;
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

    public AudioSource backgroudSound;

    private MontarAjuda ajuda;

    // Start is called before the first frame update
    void Start()
    {
        loginController = new LoginController();
        partidaController = new PartidaController();
        nivelController = new NivelController();
        elementoController = new ElementoController();

        PlayerPrefs.SetString("NIVEL", "NIVEL7");

        index = 0;
        items = new GridItem7[12];

        source = GetComponent<AudioSource>();

        usuario = loginController.getAtivo();

        CriarPartida();        

        montaGrid();
        GetElementos();
        CreateGrid();
        CriarHUD();

        fgClick = true;
        GridItem7.OnMouseOverItemEventHandler += MouseClick;
    }

    private void DecrementarErros()
    {
        numErros = numErros - 1;

        Destroy(vidas[vidas.Length - 1].gameObject);
        Array.Resize(ref vidas, vidas.Length - 1);
    }

    void MouseClick(GridItem7 item)
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
                if (nivel.MinAcertos == partida.Acertos)
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
        huds = new ItemHud[8];

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

        huds[7] = Instantiate(icones[6], new Vector3(8, -4.5f), Quaternion.identity).GetComponent<ItemHud>();
        huds[7].create("AJUDA", "6");


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
                GridItem7.OnMouseOverItemEventHandler -= MouseClick;
                Debug.Log("Saindo...");
                Application.Quit();
                break;
            case "MENU":
                ItemHud.OnMouseOverItemEventHandler -= HudClick;
                GridItem7.OnMouseOverItemEventHandler -= MouseClick;
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

            case "AJUDA":
                Debug.Log("teste ajuda1");
                CarregarAjuda();
                break;
        }
    }

    private void CarregarAjuda()
    {
        if (ajuda == null)
        {
            GameObject[] temps = Resources.LoadAll<GameObject>("Ajuda");
            GameObject poUp = temps[0];

            ajuda = Instantiate(poUp, new Vector3(0, 0), Quaternion.identity).GetComponent<MontarAjuda>();
        }

        ajuda.GetComponent<Canvas>().enabled = true;
    }

    private void EncerrarPartida()
    {
        if (partida.Acertos >= nivel.MinAcertos)
        {
            partida.Concluido = true;
        }

        partidaController.encerrarPartida(partida);
        GridItem7.OnMouseOverItemEventHandler -= MouseClick;
        ItemHud.OnMouseOverItemEventHandler -= HudClick;

        if (partida.Acertos >= nivel.MinAcertos)
        {
            SceneManager.LoadScene("Parabens");
        }
        else
        {
            SceneManager.LoadScene("NivelFracasso");
        }
        fgClick = true;
    }



    private void CriarPartida()
    {
        nivel = nivelController.get(NivelEnum.Nivel.NIVEL7.ToString());
        ultimaPartida = partidaController.getUltimaNivel(usuario.Jogador, nivel);
        partida = partidaController.getAtual(usuario.Jogador, NivelEnum.Nivel.NIVEL7.ToString());
       

        if (partida == null)
        {
     
            nivel = nivelController.getNext(NivelEnum.Nivel.NIVEL7.ToString(), ultimaPartida);     

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
            RandomUtil randNum = new RandomUtil(0, items.Length);

            Desafio desafio = desafios[index];

            pergunta.text = desafio.Descricao;

            List<Elemento> respostasErradas = elementoController.getErradasByDesafio(desafio, 2);

            Debug.Log(respostasErradas.ToArray().Length);

            for (int i = 0; i < 2; i++)
            {
                rand = randNum.get();
                items[rand] = InstantiateElemento(Comportamento.RESPOSTACERTA, desafio.Resposta.Resource, grid[rand]);
            }

            for (int k = 0; k < respostasErradas.ToArray().Length; k++)
            {
                for (int i = 0; i < 5; i++)
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

            List<Elemento> respostasErradas = elementoController.getErradasByDesafio(desafio, 2);

            for (int i = 0; i < 3; i++)
            {
                rand = randNum.get();
                items[rand] = InstantiateElemento(Comportamento.RESPOSTACERTA, desafio.Resposta.Resource, grid[rand]);
            }

            for (int k = 0; k < respostasErradas.ToArray().Length; k++)
            {
                for (int i = 0; i < 4; i++)
                {
                    rand = randNum.get();
                    items[rand] = InstantiateElemento(Comportamento.RESPOSTAERRADA, respostasErradas[k].Resource, grid[rand]);
                }
            }

            rand = randNum.get();
            items[rand] = InstantiateElemento(Comportamento.RESPOSTAERRADA, respostasErradas[respostasErradas.ToArray().Length - 1].Resource, grid[rand]);
        }
        else if (nivel.Dificuldade.Id.Equals((int)DificuldadeEnum.Dificuldade.DIFICIL))
        {
            RandomUtil randNum = new RandomUtil(0, items.Length);

            Desafio desafio = desafios[index];

            pergunta.text = desafio.Descricao;

            List<Elemento> respostasErradas = elementoController.getErradasByDesafio(desafio, 2);

            for (int i = 0; i < 4; i++)
            {
                rand = randNum.get();
                items[rand] = InstantiateElemento(Comportamento.RESPOSTACERTA, desafio.Resposta.Resource, grid[rand]);
            }

            for (int k = 0; k < respostasErradas.ToArray().Length; k++)
            {
                for (int i = 0; i < 4; i++)
                {
                    rand = randNum.get();
                    items[rand] = InstantiateElemento(Comportamento.RESPOSTAERRADA, respostasErradas[k].Resource, grid[rand]);
                }
            }
        }

    }

    GridItem7 InstantiateElemento(Comportamento comportamento, string resource, Vector3 posicao)
    {
        GameObject elemento = elementos[Int32.Parse(resource)];
        GridItem7 newElemento = Instantiate(elemento, posicao, Quaternion.identity).GetComponent<GridItem7>();

        newElemento.create(comportamento.ToString(), resource);
        return newElemento;
    }


    void GetElementos()
    {
        elementos = Resources.LoadAll<GameObject>("Nivel7");
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GridManagerParabens : MonoBehaviour
{
    public ItemHud[] huds;
    private Login usuario;

    private LoginController loginController;
    private PartidaController partidaController;
    private NivelController nivelController;
    private ElementoController elementoController;

    private Resultado resultado;

    private AudioSource source;
    public AudioClip clip;

    public AudioSource backgroudSound;

    public Text mensagem;
    public Text tempo;
    public Text acertos;
    public Text erros;


    // Start is called before the first frame update
    void Start()
    {
        loginController = new LoginController();
        partidaController = new PartidaController();
        nivelController = new NivelController();
        elementoController = new ElementoController();

        PlayerPrefs.SetString("NIVEL", "NIVEL1");

        source = GetComponent<AudioSource>();

        usuario = loginController.get(Int32.Parse(PlayerPrefs.GetString("USUARIO")));        

        Partida ultimaPartida = partidaController.getUltima(usuario.Jogador);
        Dificuldade dificuldade = ultimaPartida.Nivel.Dificuldade;

        resultado = partidaController.calcularResultado(ultimaPartida);

        if (dificuldade.Id.Equals((int)DificuldadeEnum.Dificuldade.FACIL))
        {
            mensagem.text = "Você concluiu todos os níveis na dificuldade FÁCIL";
        }
        else if (dificuldade.Id.Equals((int)DificuldadeEnum.Dificuldade.MEDIO))
        {
            mensagem.text = "Você concluiu todos os níveis na dificuldade MÉDIA";
        }
        else if (dificuldade.Id.Equals((int)DificuldadeEnum.Dificuldade.DIFICIL))
        {
            mensagem.text = "Você concluiu todos os níveis na dificuldade DIFÍCIL";
        }

        tempo.text = Math.Round(resultado.TotalTempo, 2).ToString() + " minutos";
        acertos.text = resultado.TotalAcertos.ToString();
        erros.text = resultado.TotalErros.ToString();


        CriarHUD();
    }    

    private void CriarHUD()
    {
        GameObject[] icones = Resources.LoadAll<GameObject>("Hud");
        huds = new ItemHud[8];

        huds[0] = Instantiate(icones[0], new Vector3(2, 4), Quaternion.identity).GetComponent<ItemHud>();
        huds[0].create("MENU", "0");

        huds[1] = Instantiate(icones[1], new Vector3(6, 4), Quaternion.identity).GetComponent<ItemHud>();
        huds[1].create("SAIR", "1");
       

        string som = PlayerPrefs.GetString("SOM");
        if (som.Equals("ON"))
        {
            backgroudSound.UnPause();
            huds[2] = Instantiate(icones[4], new Vector3(-1, 4), Quaternion.identity).GetComponent<ItemHud>();
            huds[2].create("SOMOFF", "4");
        }
        else
        {
            backgroudSound.Pause();
            huds[2] = Instantiate(icones[5], new Vector3(-1, 4), Quaternion.identity).GetComponent<ItemHud>();
            huds[2].create("SOMON", "5");
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
                Debug.Log("Saindo...");
                Application.Quit();
                break;
            case "MENU":
                ItemHud.OnMouseOverItemEventHandler -= HudClick;
                SceneManager.LoadScene("MainMenu");
                break;

            case "SOMOFF":
                backgroudSound.Pause();
                PlayerPrefs.SetString("SOM", "OFF");
                Destroy(huds[2].gameObject);
                huds[2] = Instantiate(icones[5], new Vector3(-1, 4), Quaternion.identity).GetComponent<ItemHud>();
                huds[2].create("SOMON", "5");
                break;

            case "SOMON":
                backgroudSound.UnPause();
                PlayerPrefs.SetString("SOM", "ON");
                Destroy(huds[2].gameObject);
                huds[2] = Instantiate(icones[4], new Vector3(-1, 4), Quaternion.identity).GetComponent<ItemHud>();
                huds[2].create("SOMOFF", "4");
                break;            
        }
    }
   
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Login usuario;
    private LoginController loginController;
    private PartidaController partidaController;
    private NivelController nivelController;
    private QuestionarioController questionarioController;
    private DificuldadeController dificuldadeController;
    private AudioSource source;
    public AudioClip clip;
    private string nextScene;
    public ItemHud[] huds;
    public AudioSource backgroudSound;

    public GameObject[] items;
    // Start is called before the first frame update
    void Start()
    {
        loginController = new LoginController();
        partidaController = new PartidaController();
        questionarioController = new QuestionarioController();
        nivelController = new NivelController();
        dificuldadeController = new DificuldadeController();

        source = GetComponent<AudioSource>();

        usuario = loginController.get(Int32.Parse(PlayerPrefs.GetString("USUARIO")));        

        carregarItems();
        carregarItensMenu();

        CriarHUD();

        MenuItem.OnMouseOverItemEventHandler += MouseClick;
        
    }

    void MouseClick(MenuItem item)
    {
        source.PlayOneShot(clip);
        if (questionarioController.isRespondido(item.Nivel, usuario.Jogador))
        {
            nextScene = item.Nivel;
            Invoke(nameof(carregarCena), 0.5f);
        }
        else
        {
            nextScene = "Questionario";
            PlayerPrefs.SetString("NIVEL", item.Nivel.ToUpper());
            Invoke(nameof(carregarCena), 0.5f);
        }
        
    }

    private void CriarHUD()
    {
        GameObject[] icones = Resources.LoadAll<GameObject>("Hud");
        huds = new ItemHud[7];       

        huds[0] = Instantiate(icones[1], new Vector3(6, 4), Quaternion.identity).GetComponent<ItemHud>();
        huds[0].create("SAIR", "1");
        huds[6] = Instantiate(icones[7], new Vector3(6, -4.5f), Quaternion.identity).GetComponent<ItemHud>();
        huds[6].create("SOBRE", "1");

        string som = PlayerPrefs.GetString("SOM");

        if(som == null || som == "")
        {
            PlayerPrefs.SetString("SOM", "ON");
            som = PlayerPrefs.GetString("SOM");
        }

        if (som.Equals("ON"))
        {
            backgroudSound.UnPause();
            huds[1] = Instantiate(icones[4], new Vector3(3, 4), Quaternion.identity).GetComponent<ItemHud>();
            huds[1].create("SOMOFF", "4");
        }
        else
        {
            backgroudSound.Pause();
            huds[1] = Instantiate(icones[5], new Vector3(3, 4), Quaternion.identity).GetComponent<ItemHud>();
            huds[1].create("SOMON", "5");
        }

        Dificuldade dificuldadeJogador = dificuldadeController.obterDificuldadeJogador(usuario.Jogador);

        if (dificuldadeJogador.Id.Equals((int)DificuldadeEnum.Dificuldade.FACIL))
        {
            huds[2] = Instantiate(icones[3], new Vector3(-8, 4), Quaternion.identity).GetComponent<ItemHud>();
        }
        else if (dificuldadeJogador.Id.Equals((int)DificuldadeEnum.Dificuldade.MEDIO))
        {
            huds[2] = Instantiate(icones[3], new Vector3(-8, 4), Quaternion.identity).GetComponent<ItemHud>();
            huds[3] = Instantiate(icones[3], new Vector3(-7, 4), Quaternion.identity).GetComponent<ItemHud>();
        }
        else if (dificuldadeJogador.Id.Equals((int)DificuldadeEnum.Dificuldade.DIFICIL))
        {
            huds[2] = Instantiate(icones[3], new Vector3(-8, 4), Quaternion.identity).GetComponent<ItemHud>();
            huds[4] = Instantiate(icones[3], new Vector3(-7, 4), Quaternion.identity).GetComponent<ItemHud>();
            huds[5] = Instantiate(icones[3], new Vector3(-6, 4), Quaternion.identity).GetComponent<ItemHud>();
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
                MenuItem.OnMouseOverItemEventHandler -= MouseClick;
                Debug.Log("Saindo...");
                Application.Quit();
                break;

            case "SOBRE":
                ItemHud.OnMouseOverItemEventHandler -= HudClick;
                SceneManager.LoadScene("Sobre");
                break;

            case "SOMOFF":
                backgroudSound.Pause();
                PlayerPrefs.SetString("SOM", "OFF");
                Destroy(huds[1].gameObject);
                huds[1] = Instantiate(icones[5], new Vector3(3, 4), Quaternion.identity).GetComponent<ItemHud>();
                huds[1].create("SOMON", "5");                
                break;

            case "SOMON":
                backgroudSound.UnPause();
                PlayerPrefs.SetString("SOM", "ON");
                Destroy(huds[1].gameObject);
                huds[1] = Instantiate(icones[4], new Vector3(3, 4), Quaternion.identity).GetComponent<ItemHud>();
                huds[1].create("SOMOFF", "4");
                break;
        }
    }

    private void carregarCena()
    {
        MenuItem.OnMouseOverItemEventHandler -= MouseClick;
        ItemHud.OnMouseOverItemEventHandler -= HudClick;
        SceneManager.LoadScene(nextScene);
    }

    private void carregarItensMenu()
    {
        GameObject item;
        MenuItem newItem;    

        if (podeCarregarNivel(NivelEnum.Nivel.NIVEL1))
        {
            item = items[0];
            newItem = Instantiate(item, new Vector3(-5, -3), Quaternion.identity).GetComponent<MenuItem>();
            newItem.create("Nivel1", "0");
        }
        if (podeCarregarNivel(NivelEnum.Nivel.NIVEL2))
        {
            item = items[1];
            newItem = Instantiate(item, new Vector3(-3, -2.5f), Quaternion.identity).GetComponent<MenuItem>();
            newItem.create("Nivel2", "1");
        }
        if (podeCarregarNivel(NivelEnum.Nivel.NIVEL3))
        {
            item = items[1];
            newItem = Instantiate(item, new Vector3(-1, -2f), Quaternion.identity).GetComponent<MenuItem>();
            newItem.create("Nivel3", "1");
        }
        if (podeCarregarNivel(NivelEnum.Nivel.NIVEL4))
        {
            item = items[1];
            newItem = Instantiate(item, new Vector3(1, -1.5f), Quaternion.identity).GetComponent<MenuItem>();
            newItem.create("Nivel4", "1");
        }
        if (podeCarregarNivel(NivelEnum.Nivel.NIVEL5))
        {
            item = items[1];
            newItem = Instantiate(item, new Vector3(3, -1f), Quaternion.identity).GetComponent<MenuItem>();
            newItem.create("Nivel5", "1");
        }
        if (podeCarregarNivel(NivelEnum.Nivel.NIVEL6))
        {
            item = items[1];
            newItem = Instantiate(item, new Vector3(5, -0.5f), Quaternion.identity).GetComponent<MenuItem>();
            newItem.create("Nivel6", "1");
        }
        if (podeCarregarNivel(NivelEnum.Nivel.NIVEL7))
        {
            item = items[1];
            newItem = Instantiate(item, new Vector3(7, 0), Quaternion.identity).GetComponent<MenuItem>();
            newItem.create("Nivel7", "1");
        }

    }

    private bool podeCarregarNivel(NivelEnum.Nivel nivelNome)
    {
        bool flag = false;
        Nivel nivel = nivelController.get(nivelNome.ToString());
        Partida ultimaPartida = partidaController.getUltimaNivel(usuario.Jogador, nivel);
        Dificuldade dificuldadeJogador = dificuldadeController.obterDificuldadeJogador(usuario.Jogador);
        
        switch (nivelNome)
        {
            case NivelEnum.Nivel.NIVEL1:
                flag = true;
                break;

            case NivelEnum.Nivel.NIVEL2:
                if(ultimaPartida != null && ultimaPartida.Nivel.Dificuldade.Id >= dificuldadeJogador.Id)
                {
                    flag = true;
                }
                else
                {
                    nivel = nivelController.get(NivelEnum.Nivel.NIVEL1.ToString());
                    ultimaPartida = partidaController.getUltimaNivel(usuario.Jogador, nivel);
                    if(ultimaPartida == null || ultimaPartida.Nivel.Dificuldade.Id < dificuldadeJogador.Id)
                    {
                        flag = false;
                    }
                    else
                    {
                        flag = true;
                    }

                }               
                break;

            case NivelEnum.Nivel.NIVEL3:
                if (ultimaPartida != null && ultimaPartida.Nivel.Dificuldade.Id >= dificuldadeJogador.Id)
                {
                    flag = true;
                }
                else
                {
                    nivel = nivelController.get(NivelEnum.Nivel.NIVEL2.ToString());
                    ultimaPartida = partidaController.getUltimaNivel(usuario.Jogador, nivel);
                    if (ultimaPartida == null || ultimaPartida.Nivel.Dificuldade.Id < dificuldadeJogador.Id)
                    {
                        flag = false;
                    }
                    else
                    {
                        flag = true;
                    }

                }
                break;

            case NivelEnum.Nivel.NIVEL4:
                if (ultimaPartida != null && ultimaPartida.Nivel.Dificuldade.Id >= dificuldadeJogador.Id)
                {
                    flag = true;
                }
                else
                {
                    nivel = nivelController.get(NivelEnum.Nivel.NIVEL3.ToString());
                    ultimaPartida = partidaController.getUltimaNivel(usuario.Jogador, nivel);
                    if (ultimaPartida == null || ultimaPartida.Nivel.Dificuldade.Id < dificuldadeJogador.Id)
                    {
                        flag = false;
                    }
                    else
                    {
                        flag = true;
                    }

                }
                break;

            case NivelEnum.Nivel.NIVEL5:
                if (ultimaPartida != null && ultimaPartida.Nivel.Dificuldade.Id >= dificuldadeJogador.Id)
                {
                    flag = true;
                }
                else
                {
                    nivel = nivelController.get(NivelEnum.Nivel.NIVEL4.ToString());
                    ultimaPartida = partidaController.getUltimaNivel(usuario.Jogador, nivel);
                    if (ultimaPartida == null || ultimaPartida.Nivel.Dificuldade.Id < dificuldadeJogador.Id)
                    {
                        flag = false;
                    }
                    else
                    {
                        flag = true;
                    }

                }
                break;


            case NivelEnum.Nivel.NIVEL6:
                if (ultimaPartida != null && ultimaPartida.Nivel.Dificuldade.Id >= dificuldadeJogador.Id)
                {
                    flag = true;
                }
                else
                {
                    nivel = nivelController.get(NivelEnum.Nivel.NIVEL5.ToString());
                    ultimaPartida = partidaController.getUltimaNivel(usuario.Jogador, nivel);
                    if (ultimaPartida == null || ultimaPartida.Nivel.Dificuldade.Id < dificuldadeJogador.Id)
                    {
                        flag = false;
                    }
                    else
                    {
                        flag = true;
                    }

                }
                break;

            case NivelEnum.Nivel.NIVEL7:
                if (ultimaPartida != null && ultimaPartida.Nivel.Dificuldade.Id >= dificuldadeJogador.Id)
                {
                    flag = true;
                }
                else
                {
                    nivel = nivelController.get(NivelEnum.Nivel.NIVEL6.ToString());
                    ultimaPartida = partidaController.getUltimaNivel(usuario.Jogador, nivel);
                    if (ultimaPartida == null || ultimaPartida.Nivel.Dificuldade.Id < dificuldadeJogador.Id)
                    {
                        flag = false;
                    }
                    else
                    {
                        flag = true;
                    }

                }
                break;
        }

        return flag;
    }

    private void carregarItems()
    {
        items = Resources.LoadAll<GameObject>("Menu");
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SobreManager : MonoBehaviour
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


        CriarHUD();

    }  

    private void CriarHUD()
    {
        GameObject[] icones = Resources.LoadAll<GameObject>("Hud");
        huds = new ItemHud[10];

        huds[0] = Instantiate(icones[1], new Vector3(6, 4), Quaternion.identity).GetComponent<ItemHud>();
        huds[0].create("SAIR", "1");
        huds[6] = Instantiate(icones[0], new Vector3(2, 4), Quaternion.identity).GetComponent<ItemHud>();
        huds[6].create("MENU", "0");

        string som = PlayerPrefs.GetString("SOM");

        if (som == null || som == "")
        {
            PlayerPrefs.SetString("SOM", "ON");
            som = PlayerPrefs.GetString("SOM");
        }

        if (som.Equals("ON"))
        {
            backgroudSound.UnPause();
            huds[1] = Instantiate(icones[4], new Vector3(-1, 4), Quaternion.identity).GetComponent<ItemHud>();
            huds[1].create("SOMOFF", "4");
        }
        else
        {
            backgroudSound.Pause();
            huds[1] = Instantiate(icones[5], new Vector3(-1, 4), Quaternion.identity).GetComponent<ItemHud>();
            huds[1].create("SOMON", "5");
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
                Destroy(huds[1].gameObject);
                huds[1] = Instantiate(icones[5], new Vector3(-1, 4), Quaternion.identity).GetComponent<ItemHud>();
                huds[1].create("SOMON", "5");
                break;

            case "SOMON":
                backgroudSound.UnPause();
                PlayerPrefs.SetString("SOM", "ON");
                Destroy(huds[1].gameObject);
                huds[1] = Instantiate(icones[4], new Vector3(-1, 4), Quaternion.identity).GetComponent<ItemHud>();
                huds[1].create("SOMOFF", "4");
                break;
        }
    }    

}

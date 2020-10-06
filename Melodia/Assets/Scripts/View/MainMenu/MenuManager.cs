using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Login usuario;
    public Partida ultimaPartida;
    private LoginController loginController;
    private PartidaController partidaController;
    private QuestionarioController questionarioController;

    public GameObject[] items;
    // Start is called before the first frame update
    void Start()
    {
        loginController = new LoginController();
        partidaController = new PartidaController();
        questionarioController = new QuestionarioController();

        usuario = loginController.getAtivo();
        ultimaPartida = partidaController.getUltima(usuario.Jogador);

        carregarItems();
        carregarItensMenu();
        MenuItem.OnMouseOverItemEventHandler += MouseClick;
    }

    void MouseClick(MenuItem item)
    {
        if(questionarioController.isRespondido(item.Nivel, usuario.Jogador))
        {
            SceneManager.LoadScene(item.Nivel);      
        }
        else
        {
            SceneManager.LoadScene("Questionario");
        }
        
    }

    private void carregarItensMenu()
    {
        GameObject item;
        MenuItem newItem;    

        
        item = items[0];
        newItem = Instantiate(item, new Vector3(-5, -3), Quaternion.identity).GetComponent<MenuItem>();
        newItem.create("Nivel1", "0");
           
        

        if(ultimaPartida != null && (ultimaPartida.Nivel.Nome.Equals(NivelEnum.Nivel.NIVEL2.ToString().ToUpper()) || (ultimaPartida.Nivel.Nome.Equals(NivelEnum.Nivel.NIVEL1.ToString().ToUpper()) && ultimaPartida.Nivel.Dificuldade.Id.Equals((int)DificuldadeEnum.Dificuldade.DIFICIL))))
        {
            item = items[1];
            newItem = Instantiate(item, new Vector3(-3, -2.5f), Quaternion.identity).GetComponent<MenuItem>();
            newItem.create("Nivel2", "1");
        }
    }

    private void carregarItems()
    {
        items = Resources.LoadAll<GameObject>("Menu");
    }
}

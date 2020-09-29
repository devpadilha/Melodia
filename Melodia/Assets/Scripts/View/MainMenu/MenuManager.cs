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

    public GameObject[] items;
    // Start is called before the first frame update
    void Start()
    {
        loginController = new LoginController();
        partidaController = new PartidaController();

        usuario = loginController.getAtivo();
        ultimaPartida = partidaController.getUltima(usuario.Jogador);

        carregarItems();
        carregarItensMenu();
        MenuItem.OnMouseOverItemEventHandler += MouseClick;
    }

    void MouseClick(MenuItem item)
    {
        Debug.Log(item.Nivel);
        SceneManager.LoadScene(item.Nivel);
    }

    private void carregarItensMenu()
    {
        GameObject item;
        MenuItem newItem;

        if (ultimaPartida == null || ultimaPartida.Nivel.Nome.Equals(NivelEnum.Nivel.NIVEL1))
        {
            item = items[0];
            newItem = Instantiate(item, new Vector3(-5, -3), Quaternion.identity).GetComponent<MenuItem>();
            newItem.create("Nivel1", "0");

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

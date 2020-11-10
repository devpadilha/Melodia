using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestionarioManagement : MonoBehaviour
{
    public Text pergunta;
    public Toggle op1;
    public Toggle op2;
    private List<Questionario> questionario;
    public Login usuario;
    public Partida ultimaPartida;
    private Nivel nivel;
    private QuestionarioController questionarioController;
    private LoginController loginController;
    private PartidaController partidaController;
    private NivelController nivelController;
    private int index;
    private bool fgClick;

    public ItemHud[] huds;
    public AudioSource backgroudSound;

    // Start is called before the first frame update
    void Start()
    {
        questionarioController = new QuestionarioController();
        loginController = new LoginController();
        partidaController = new PartidaController();
        nivelController = new NivelController();

        CriarHUD();

        usuario = loginController.getAtivo();
        ultimaPartida = partidaController.getUltima(usuario.Jogador);

        string nivelNome = PlayerPrefs.GetString("NIVEL");
        
        nivel = nivelController.get(nivelNome);
        questionario = questionarioController.getByNivel(nivel);
        

        op1.isOn = false;
        op2.isOn = false;

        op1.onValueChanged.AddListener((x) => Invoke("ToggleClick", 0.1f));
        op2.onValueChanged.AddListener((x) => Invoke("ToggleClick", 0.1f));


        montaQuestionario();

        index = 0;
        fgClick = true;
    }

    private void CriarHUD()
    {
        GameObject[] icones = Resources.LoadAll<GameObject>("Hud");
        huds = new ItemHud[6];


        string som = PlayerPrefs.GetString("SOM");
        if (som.Equals("ON"))
        {
            backgroudSound.UnPause();
            huds[0] = Instantiate(icones[4], new Vector3(3, 4), Quaternion.identity).GetComponent<ItemHud>();
            huds[0].create("SOMOFF", "4");
        }
        else
        {
            backgroudSound.Pause();
            huds[0] = Instantiate(icones[5], new Vector3(3, 4), Quaternion.identity).GetComponent<ItemHud>();
            huds[0].create("SOMON", "5");
        }

        ItemHud.OnMouseOverItemEventHandler += HudClick;
    }

    private void HudClick(ItemHud item)
    {
        GameObject[] icones = Resources.LoadAll<GameObject>("Hud");
        switch (item.Comportamento)
        {         

            case "SOMOFF":
                backgroudSound.Pause();
                PlayerPrefs.SetString("SOM", "OFF");
                Destroy(huds[0].gameObject);
                huds[0] = Instantiate(icones[5], new Vector3(3, 4), Quaternion.identity).GetComponent<ItemHud>();
                huds[0].create("SOMON", "5");
                break;

            case "SOMON":
                backgroudSound.UnPause();
                PlayerPrefs.SetString("SOM", "ON");
                Destroy(huds[0].gameObject);
                huds[0] = Instantiate(icones[4], new Vector3(3, 4), Quaternion.identity).GetComponent<ItemHud>();
                huds[0].create("SOMOFF", "4");
                break;
        }
    }

    private void ToggleClick()
    {
        if (fgClick)
        {
            fgClick = false;
            Resposta resposta = new Resposta();

            resposta.Questionario = questionario[index];
            resposta.Jogador = usuario.Jogador;

            if (op1.isOn)
            {
                resposta.Opcao = "Sim";

            }
            else if (op2.isOn)
            {
                resposta.Opcao = "Não";
            }
            else
            {
                fgClick = true;
                return;
            }


            questionarioController.insertResposta(resposta);
            index++;
            if (index < questionario.ToArray().Length)
            {
                Invoke("montaQuestionario", 0.5f);
            }
            else
            {
                ItemHud.OnMouseOverItemEventHandler -= HudClick;
                SceneManager.LoadScene("MainMenu");
            }
            
        }
    }

    private void montaQuestionario()
    {
        Questionario questao = questionario[index];

        string[] opcoes = questao.Opcoes.Split(';');

        pergunta.text = questao.Pergunta;
        /*
        Text temp = op1.GetComponent<Text>();
        temp.text = opcoes[0];

        temp = op2.GetComponent<Text>();
        temp.text = opcoes[1];
        */
        op1.isOn = false;
        op2.isOn = false;
        fgClick = true;
    }
}

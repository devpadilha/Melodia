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
    // Start is called before the first frame update
    void Start()
    {
        questionarioController = new QuestionarioController();
        loginController = new LoginController();
        partidaController = new PartidaController();
        nivelController = new NivelController();

        usuario = loginController.getAtivo();
        ultimaPartida = partidaController.getUltima(usuario.Jogador);
        

        if (ultimaPartida == null || ultimaPartida.Nivel.Nome.Equals(NivelEnum.Nivel.NIVEL1.ToString().ToUpper()))
        {
            nivel = nivelController.getNext(NivelEnum.Nivel.NIVEL1.ToString(), ultimaPartida);
            questionario = questionarioController.getByNivel(nivel);
        }
        if (ultimaPartida != null && ultimaPartida.Nivel.Nome.Equals(NivelEnum.Nivel.NIVEL1.ToString().ToUpper()) && ultimaPartida.Nivel.Dificuldade.Id.Equals((int) DificuldadeEnum.Dificuldade.DIFICIL))
        {
            nivel = nivelController.getNext(NivelEnum.Nivel.NIVEL2.ToString(), ultimaPartida);
            questionario = questionarioController.getByNivel(nivel);
        }
        if (ultimaPartida != null && ultimaPartida.Nivel.Nome.Equals(NivelEnum.Nivel.NIVEL2.ToString().ToUpper()) && ultimaPartida.Nivel.Dificuldade.Id.Equals((int)DificuldadeEnum.Dificuldade.DIFICIL))
        {
            nivel = nivelController.getNext(NivelEnum.Nivel.NIVEL3.ToString(), ultimaPartida);
            questionario = questionarioController.getByNivel(nivel);
        }
        if (ultimaPartida != null && ultimaPartida.Nivel.Nome.Equals(NivelEnum.Nivel.NIVEL3.ToString().ToUpper()) && ultimaPartida.Nivel.Dificuldade.Id.Equals((int)DificuldadeEnum.Dificuldade.DIFICIL))
        {
            nivel = nivelController.getNext(NivelEnum.Nivel.NIVEL4.ToString(), ultimaPartida);
            questionario = questionarioController.getByNivel(nivel);
        }

        op1.isOn = false;
        op2.isOn = false;

        op1.onValueChanged.AddListener((x) => Invoke("ToggleClick", 0.1f));
        op2.onValueChanged.AddListener((x) => Invoke("ToggleClick", 0.1f));


        montaQuestionario();

        index = 0;
        fgClick = true;
    }

    // Update is called once per frame
    void Update()
    {
       
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

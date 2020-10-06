using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneUtil : MonoBehaviour
{
    public string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        backgroudScale();
    }

    // Update is called once per frame
    void Update()
    {
        
    }  
    
    private void backgroudScale()
    {
        SpriteRenderer grafico = GetComponent<SpriteRenderer>();

        if (grafico != null)
        {
            float larguraImagem = grafico.sprite.bounds.size.x;
            float alturaImagem = grafico.sprite.bounds.size.y;
            float alturaTela = Camera.main.orthographicSize * 2f;
            float larguraTela = alturaTela / Screen.height * Screen.width;

            Vector2 novaEscala = transform.localScale;
            novaEscala.x = larguraTela / larguraImagem + 0.25f;
            novaEscala.y = alturaTela / alturaImagem;
            transform.localScale = novaEscala;
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Exit()
    {
        Debug.Log("Saindo...");
        Application.Quit();
    }
}

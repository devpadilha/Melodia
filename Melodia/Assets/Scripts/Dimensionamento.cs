using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dimensionamento : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
        SpriteRenderer grafico = GetComponent<SpriteRenderer>();

        float larguraImagem = grafico.sprite.bounds.size.x;
        float alturaImagem = grafico.sprite.bounds.size.y;
        float alturaTela = Camera.main.orthographicSize * 2f;
        float larguraTela = alturaTela / Screen.height * Screen.width;
        
        Vector2 novaEscala = transform.localScale;
        novaEscala.x = larguraTela / larguraImagem + 0.25f;
        novaEscala.y = alturaTela / alturaImagem;
        transform.localScale = novaEscala;        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

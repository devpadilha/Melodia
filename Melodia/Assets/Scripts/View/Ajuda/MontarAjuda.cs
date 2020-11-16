using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MontarAjuda : MonoBehaviour
{
    public Image imagem;
    
    // Start is called before the first frame update
    void Start()
    {
        string nivelNome = PlayerPrefs.GetString("NIVEL");
        imagem.sprite = Resources.Load<Sprite>(nivelNome); 
    }
}

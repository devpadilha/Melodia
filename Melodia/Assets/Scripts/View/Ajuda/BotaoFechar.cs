using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoFechar : MonoBehaviour
{
    public Canvas popUp;

    public void fecharAjuda()
    {
        popUp.enabled = false;        
    }

}

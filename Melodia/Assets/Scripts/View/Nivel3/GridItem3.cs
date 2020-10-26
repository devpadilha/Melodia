using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridItem3 : MonoBehaviour
{
    private string comportamento;
    public string Comportamento
    {
        get
        {
            return comportamento;
        }
        set
        {
            comportamento = value;
        }
    }

    public void create(string comportamento, string resource)
    {
        this.comportamento = comportamento;
        gameObject.name = string.Format("Sprite [{0}] [{1}]", comportamento, resource);
    }

    private void OnMouseDown()
    {
        Debug.Log(this.comportamento);
        if (OnMouseOverItemEventHandler != null)
        {
            OnMouseOverItemEventHandler(this);
        }
    }

    public delegate void OnMouseOverItem(GridItem3 item);
    public static event OnMouseOverItem OnMouseOverItemEventHandler;
}

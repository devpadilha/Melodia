using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridItem2 : MonoBehaviour
{
    private int index;
    private string valor;
    public Renderer rend;

    public int Index
    {
        get
        {
            return index;
        }
        set
        {
            index = value;
        }
    }

    public string Valor
    {
        get
        {
            return valor;
        }
        set
        {
            valor = value;
        }
    }    

    public Renderer Rend
    {
        get
        {
            return rend;
        }
        set
        {
            rend = value;
        }
    }

    public void create(string valor, string resource, bool render, int index)
    {
        this.valor = valor;
        this.rend = GetComponent<Renderer>();
        this.rend.enabled = render;
        this.index = index;
        gameObject.name = string.Format("Sprite [{0}] [{1}]", valor, resource);
    }

    private void OnMouseDown()
    {
        if (OnMouseOverItemEventHandler != null)
        {
            OnMouseOverItemEventHandler(this);
        }
    }

    public delegate void OnMouseOverItem(GridItem2 item);
    public static event OnMouseOverItem OnMouseOverItemEventHandler;
}

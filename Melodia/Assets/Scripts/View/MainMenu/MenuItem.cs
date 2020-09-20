using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuItem : MonoBehaviour
{
    private string nivel;
    public string Nivel
    {
        get
        {
            return nivel;
        }
        set
        {
            nivel = value;
        }
    }

    public void create(string nivel, string resource)
    {
        this.nivel = nivel;
        gameObject.name = string.Format("Sprite [{0}] [{1}]", nivel, resource);
    }

    private void OnMouseDown()
    {
        if(OnMouseOverItemEventHandler != null)
        {
            OnMouseOverItemEventHandler(this);
        }
    }

    public delegate void OnMouseOverItem(MenuItem item);
    public static event OnMouseOverItem OnMouseOverItemEventHandler;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (OnMouseOverItemEventHandler != null)
            {
                OnMouseOverItemEventHandler(this);
            }
        }
    }
}

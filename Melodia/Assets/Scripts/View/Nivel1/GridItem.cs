using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridItem : MonoBehaviour
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

    public delegate void OnMouseOverItem(GridItem item);
    public static event OnMouseOverItem OnMouseOverItemEventHandler;
}

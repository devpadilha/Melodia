using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemHud : MonoBehaviour
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
        gameObject.name = string.Format("Sprite [{0}] [{1}]", this.comportamento, resource);
    }

    private void OnMouseDown()
    {
        Debug.Log(this.comportamento);
        if (OnMouseOverItemEventHandler != null)
        {
            OnMouseOverItemEventHandler(this);
        }
    }

    public delegate void OnMouseOverItem(ItemHud item);
    public static event OnMouseOverItem OnMouseOverItemEventHandler;
}

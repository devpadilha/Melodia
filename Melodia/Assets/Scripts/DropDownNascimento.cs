using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownNascimento : MonoBehaviour
{
    public Dropdown dia;
    public Dropdown mes;
    public Dropdown ano;
    // Start is called before the first frame update
    void Start()
    {
        if(dia != null)
        {
            dia = GetComponent<Dropdown>();
            dia.ClearOptions();
            dia.AddOptions(iniciarDropDown("dia"));
        }
       if(mes != null)
        {
            mes = GetComponent<Dropdown>();
            mes.ClearOptions();
            mes.AddOptions(iniciarDropDown("mes"));
        }
       if(ano != null)
        {
            ano = GetComponent<Dropdown>();
            ano.ClearOptions();
            ano.AddOptions(iniciarDropDown("ano"));
        }
       
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    List<string> iniciarDropDown(string tipo)
    {
        List<string> options = new List<string>();
        switch (tipo)
        {
            case "dia":
                for(int i = 1; i <= 31; i++)
                {
                    options.Add(i.ToString());
                }
                break;
            case "mes":
                for (int i = 1; i <= 12; i++)
                {
                    options.Add(i.ToString());
                }
                break;
            case "ano":
                for (int i = DateTime.Now.Year; i >= 1900 ; i--)
                {
                    options.Add(i.ToString());
                }
                break;
        }
        return options;
    }
}

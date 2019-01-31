using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsBehavour : MonoBehaviour
{
    public GameObject[] controles;
    public int actualControler=0;
    public Text text;
    private void Update()
    {
        ViewControlers();
    }
    public void ChageForwardControler()
    {
        actualControler++;
        if (actualControler > 2)
            actualControler = 0;
    }
    public void ChageBackwardControler()
    {
        actualControler--;
        if (actualControler < 0)
            actualControler = 2;
    }
    public void ViewControlers()
    {
        if (actualControler == 0)
        {
            controles[0].SetActive(true);
            controles[1].SetActive(false);
            controles[2].SetActive(false);
            text.text = "Teclado y Ratón";
        }
        else if(actualControler==1)
        {
            controles[0].SetActive(false);
            controles[1].SetActive(true);
            controles[2].SetActive(false);
            text.text = "Xbox Controller";
        }
        else if (actualControler == 2)
        {
            controles[0].SetActive(false);
            controles[1].SetActive(false);
            controles[2].SetActive(true);
            text.text = "PS4 Controller";
        }
    }
}

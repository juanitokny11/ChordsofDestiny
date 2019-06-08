using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapa : MonoBehaviour
{
    public GameObject mapaJ;
    public GameObject mapaJ2;
    public Animator camanim;
    private void Start()
    {
        camanim = GetComponent<Animator>();
    }
    void encender()
    {
        mapaJ.SetActive (true);
        mapaJ2.SetActive (true);
    }

    void apagar()
    {
        mapaJ.SetActive (false);
        mapaJ2.SetActive (false);
        camanim.enabled = false;
    }
}

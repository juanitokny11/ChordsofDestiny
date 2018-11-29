using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class metronomo : MonoBehaviour {
    public Image imag;
    public Sprite metronomo_01;
    
    //private AudioSource audio;
    public bool daño = false;

    private static metronomo instance;

    public static metronomo getInstance()
    {
        return instance;
    }

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        //audio = GetComponent<AudioSource>();
        //audio.Play();
    }

    void Update () {
        if (imag.sprite = metronomo_01)
        {
            DañoPlus();
            Invoke("Dañomenos", 0.01f);
        }
    }
    void DañoPlus()
    {
        daño = true;
    }
    void Dañomenos()
    {
        daño = false;
    }
}

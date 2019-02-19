using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FadeInMusica : MonoBehaviour
{
    public AudioSource musica;
    void Start()
    {
        musica.DOFade(0.3f,10f);
    }


}

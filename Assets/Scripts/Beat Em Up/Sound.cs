using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
   public AudioSource salidabos;
    public List<AudioSource> fuerteaire;
   public  List<AudioSource> debilaire;
    public List<AudioSource> groupieAire;
    public List<AudioSource> groupieHit;
    public int random;
    public int random2;
    public bool hit = false;
   public AudioSource walkgroupie;
    public void Update()
    {
        random = Random.Range(0,6);
        random2 = Random.Range(0, 3);
    }
    void door()
   {
       salidabos.Play();
   }

    void airedebil()
   {
        if(!hit)
            debilaire[random].Play();
   }

    void airefuerte()
   {
         if(!hit)
            fuerteaire[random2].Play();
   }

    void andargroupie()
   {
       walkgroupie.pitch = Random.Range(0.7f,1.3f);
        walkgroupie.volume = Random.Range(0.15f,0.45f);
       walkgroupie.Play(); 
   }
    void AtaqueDebilGroupie()
    {
        if (!hit)
        {
            groupieAire[1].Play();
        }
        else if (hit)
        {
            groupieHit[1].Play();
        }
    }
    void AtaqueFuerteGroupie()
    {
        if (!hit)
        {
            groupieAire[0].Play();
        }
        else if (hit)
        {
            groupieHit[0].Play();
        }
    }
}

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
    public List<AudioSource> fanAire;
    public List<AudioSource> fanHit;

     public List<AudioSource> Falling;
    public int random;
    public int random2;
    public bool HitLow;
    public bool hit = false;
   public AudioSource walkgroupie;
    public AudioSource walkfan;
    public AudioSource DisolveGroupi;
    public AudioSource DisolveFan;

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
        walkgroupie.volume = Random.Range(0.15f,0.42f);
       walkgroupie.Play(); 
   }

 void andarfan()
   {
       walkfan.pitch = Random.Range(0.7f,1.3f);
        walkfan.volume = Random.Range(0.15f,0.30f);
       walkfan.Play(); 
   }

   void Ataqueairefan()
    {
        fanAire[1].pitch = Random.Range(0.95f,1.05f);
        fanAire[1].volume = Random.Range(0.95f,1.00f);
        fanAire[1].Play();
    }
    public void golpefan()
    {
        fanHit[1].pitch = Random.Range(0.95f, 1.05f);
        fanHit[1].volume = Random.Range(0.87f, 1.00f);
        fanHit[1].Play();
    }

void disolveFa()
   {
        DisolveFan.pitch = Random.Range(0.8f,1.3f);
        DisolveFan.volume = Random.Range(0.70f,0.85f);
        DisolveFan.Play(); 
   }

    void AtaqueDebilGroupie()
    {
        groupieAire[1].pitch = Random.Range(0.95f,1.05f);
        groupieAire[1].volume = Random.Range(0.90f,1.00f);
        groupieAire[1].Play();
    }
    public void HitDebilGroupie()
    {
        groupieHit[1].pitch = Random.Range(0.95f, 1.05f);
        groupieHit[1].volume = Random.Range(0.90f, 1.00f);
        groupieHit[1].PlayDelayed(0.15f);
    }
    void disolveGroupie()
   {
        DisolveGroupi.pitch = Random.Range(0.8f,1.3f);
        DisolveGroupi.volume = Random.Range(0.90f,1.00f);
        DisolveGroupi.Play(); 
   }

    void AtaqueFuerteGroupie()
    {
        groupieAire[0].pitch = Random.Range(0.95f,1.05f);
        groupieAire[0].volume = Random.Range(0.85f,0.95f);
        groupieAire[0].Play();
    }
    public void HitFuerteGroupie()
    {
        groupieHit[0].pitch = Random.Range(0.95f, 1.05f);
        groupieHit[0].volume = Random.Range(0.90f, 1.00f);
        groupieHit[0].Play();
    }

    void fall()
   {
        Falling[0].pitch = Random.Range(0.4f,1.05f);
        Falling[0].volume = Random.Range(0.92f,1.00f);
        Falling[0].Play(); 
   }

    void HitFuerte()
    {
        HitLow = false;
    }
    void HitDebil()
    {
        HitLow = true;
    }
}

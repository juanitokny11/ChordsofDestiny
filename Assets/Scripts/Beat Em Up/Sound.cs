using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
   public AudioSource salidabos;
    public AudioSource luzsalida;
    public AudioSource fin;
    public Image Frase;
    public List<Sprite> frasesBoss;
     public AudioSource muertebos;
     public AudioSource saltoarrib;
     public AudioSource saltoabaj;
    public AudioSource chispas;
     public AudioSource brazo;
    public List<AudioSource> fuerteaire;
   public  List<AudioSource> debilaire;
    public List<AudioSource> golpesHacha;
    public List<AudioSource> groupieAire;
    public List<AudioSource> groupieHit;
    public List<AudioSource> fanAire;
    public List<AudioSource> fanHit;
     public List<AudioSource> Falling;
      public List<AudioSource> Bossatack;
    public AudioSource musica;
    public AudioSource bossmusica;
    public List<AudioSource> Bossinvoca;    
      public AudioSource Bossmulti;
    public AudioSource ataqueGirSound;

    public AudioSource aireinicio;
    public AudioSource aireinici2;

    public AudioSource fraseini;
    public AudioSource ataqueAltoSound;
    public AudioSource golpeGirSound;
    public AudioSource golpeAltoSound;
    public PlayerAttackList attackList;
    public int random;
    public int random2;
    public bool HitLow;
    public bool hit = false;
   public AudioSource walkgroupie;
    public AudioSource walkfan;
    public AudioSource walkboss;
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

    void doorbeep()
    {
        luzsalida.Play();
    }

    void morir()
    {
        musica.Stop();
        bossmusica.Stop();
        fin.Play();
    }

     void aireini()
   {
       aireinicio.Play();
   }    

   void aireini2()
   {
       aireinici2.Play();
   }    

   void dothis()
   {
       fraseini.Play();
   }  
    void airedebil()
   {
        if(!attackList.D3)
            debilaire[random].Play();
        ataquegiratorio();
        if (hit && attackList.D3)
            GolpeGiratorio();
   }

    void airefuerte()
   {
         if(!attackList.F3)
            fuerteaire[random2].Play();
        ataquedesdealto();
        if (hit && attackList.F3)
            GolpeDesdeArriba();
   }
   public void GolpeHacha()
    {
        int random = Random.Range(0, 4);
        golpesHacha[random].pitch = Random.Range(0.95f, 1.05f);
        golpesHacha[random].volume = Random.Range(0.95f, 1.00f);
        golpesHacha[random].Play();
    }
    public void GolpeGiratorio()
    {
        golpeGirSound.pitch = Random.Range(0.8f, 1.3f);
        golpeGirSound.volume = Random.Range(0.45f, 0.6f);
        golpeGirSound.PlayDelayed(0.27f);
    }
    public void GolpeDesdeArriba()
    {
        golpeAltoSound.pitch = Random.Range(0.8f, 1.2f);
        golpeAltoSound.volume = Random.Range(0.45f, 0.6f);
        golpeAltoSound.Play();
    }
    public void ataquedesdealto()
    {
        if (attackList.F3)
        {
            ataqueAltoSound.pitch = Random.Range(0.8f, 1.3f);
            ataqueAltoSound.volume = Random.Range(0.45f, 0.6f);
            ataqueAltoSound.Play();
        }
    }
   public void ataquegiratorio()
    {
        if (!hit && attackList.D3)
        {
            ataqueGirSound.pitch = Random.Range(0.85f, 1.3f);
            ataqueGirSound.volume = Random.Range(0.25f, 0.38f);
            ataqueGirSound.Play();
        }
    }
    void andargroupie()
   {
        if (BeatEmupManager.instance.pause == true)
        {
            walkgroupie.pitch = Random.Range(0.7f, 1.3f);
            walkgroupie.volume = Random.Range(0.15f, 0.42f);
            walkgroupie.Play();
        }
   }

    //audios fan

 void andarfan()
   {
        if (BeatEmupManager.instance.pause == true)
        {
            walkfan.pitch = Random.Range(0.7f, 1.3f);
            walkfan.volume = Random.Range(0.15f, 0.30f);
            walkfan.Play();
        }
   }

 void Ataqueairefan()
    {
        int random = Random.Range(0,4);
        fanAire[random].pitch = Random.Range(0.95f,1.05f);
        fanAire[random].volume = Random.Range(0.95f,1.00f);
        fanAire[random].Play();
    }
    public void golpefan()
    {
        int random = Random.Range(0,5);
        fanHit[random].pitch = Random.Range(0.95f, 1.05f);
        fanHit[random].volume = Random.Range(0.87f, 1.00f);
        fanHit[random].Play();
    }

void disolveFa()
   {
        DisolveFan.pitch = Random.Range(0.8f,1.3f);
        DisolveFan.volume = Random.Range(0.70f,0.85f);
        DisolveFan.Play(); 
   }
//audios boss

void saltodow()
{
    saltoabaj.Play();
}

void saltou()
{
    saltoarrib.Play();
}

 void bosmuere()
    {
        muertebos.Play();
    }

 void invocboss()
    {
        int random = Random.Range(0, 3);
        Bossinvoca[random].Play();
    }

    void basicboss()
    {
        int random = Random.Range(0, 5);
        Bossatack[random].pitch = Random.Range(0.90f,1.05f);
        Bossatack[random].volume = Random.Range(0.85f,0.95f);
        Bossatack[random].Play();
    }

     void bossgira()
    {
        Bossmulti.pitch = Random.Range(0.90f,1.05f);
        Bossmulti.volume = Random.Range(0.85f,0.95f);
        Bossmulti.Play();
    }

    void andarboss()
   {
       
            walkboss.pitch = Random.Range(0.7f, 1.3f);
            walkboss.volume = Random.Range(0.25f, 0.40f);
            walkboss.Play();
        
   }

   public void brazorot()
   {
        brazo.Play();
    }
    public void Chispas()
    {
        chispas.pitch = Random.Range(0.8f, 1.1f);
        chispas.Play();
    }
    //audios groupie
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
    //audio comun fan y groupie
    void fall()
   {
        Falling[0].pitch = Random.Range(0.5f,1.05f);
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

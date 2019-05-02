using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
   public AudioSource salidabos;
   public  List<AudioSource> debilaire;
    public int random;
    public bool hit = false;
   public AudioSource walkgroupie;
    public void Update()
    {
        random = Random.Range(0,6);
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

    void andargroupie()
   {
       walkgroupie.pitch = Random.Range(0.7f,1.3f);
        walkgroupie.volume = Random.Range(0.15f,0.45f);
       walkgroupie.Play();
       
   }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
   public AudioSource salidabos;
   public AudioSource debilaire1;

   public AudioSource walkgroupie;
 
   void door()
   {
       salidabos.Play();
   }

    void airedebil()
   {
       debilaire1.Play();
   }

    void andargroupie()
   {
       walkgroupie.pitch = Random.Range(0.7f,1.3f);
        walkgroupie.volume = Random.Range(0.15f,0.45f);
       walkgroupie.Play();
       
   }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
   public AudioSource salidabos;

   void door()
   {
       salidabos.Play();
   }
}

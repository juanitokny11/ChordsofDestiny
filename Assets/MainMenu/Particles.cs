using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{

    public ParticleSystem chispas;

    // Start is called before the first frame update
   void logo()
   {
       chispas.Play();
   }
}

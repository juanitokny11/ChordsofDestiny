using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleZone : MonoBehaviour
{
    private BoxCollider colider;
    public GameObject UI;
    public AudioSource musica;
   

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UI.SetActive(true);
            musica.Play();
        }
    }
    
    void Update()
    {
        
    }
}

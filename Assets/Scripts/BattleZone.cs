using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleZone : MonoBehaviour
{
    private BoxCollider colider;
    public GameObject UI;
    public AudioSource musica;
    public GameObject[] enemies;
    public int enemiescounter;
    private void Start()
    {
        enemiescounter = enemies.Length;
    }
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
       if (enemiescounter <= 0)
        {
            UI.SetActive(false);
            musica.Stop();
        }
    }
}

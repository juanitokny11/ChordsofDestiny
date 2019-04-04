using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TptoBossBattle : MonoBehaviour
{
    public Transform tppoint;
    public GameObject musicBoss;
    public GameObject musicGameplay;
    public bool changeMusic=false;
    public void Start()
    {
        musicGameplay.GetComponent<AudioSource>().Play();
    }
    private void Update()
    {
        if (changeMusic == true)
        {
            musicGameplay.GetComponent<AudioSource>().enabled = false;
            Invoke("ActivateBossMusic", 1f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.transform.position = tppoint.position;
            changeMusic = true;
        }
    }
    void ActivateBossMusic()
    {
        musicBoss.SetActive(true);
    }
}

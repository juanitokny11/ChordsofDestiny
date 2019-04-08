using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TptoBossBattle : MonoBehaviour
{
    public Transform tppoint;
    public ShakeCamera camera;
    public GameObject musicBoss;
    public GameObject musicGameplay;
    public bool changeMusic=false;
    public void Start()
    {
        //musicGameplay.GetComponent<AudioSource>().Play();
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ShakeCamera>();
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
            camera.lockCamera = false;
            changeMusic = true;
        }
    }
    void ActivateBossMusic()
    {
        musicBoss.SetActive(true);
    }
}

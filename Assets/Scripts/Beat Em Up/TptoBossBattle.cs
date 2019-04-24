using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TptoBossBattle : MonoBehaviour
{
    public Transform tppoint;
    public ShakeCamera camera;
    public BattleZone BossZone;
    public Canvas BossLife;
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
            if(BossLife!=null)
            BossLife.enabled = true;
            camera.GetComponent<Camera>().fieldOfView = 55;
        }
        if (BeatEmupManager.instance.pause == true)
        {
            musicBoss.GetComponent<AudioSource>().mute = false;
        }
        else if(BeatEmupManager.instance.pause == false)
            musicBoss.GetComponent<AudioSource>().mute = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.transform.position = tppoint.position;
            camera.lockCamera = false;
            changeMusic = true;
            BossZone.bossZone = true;
        }
    }
    void ActivateBossMusic()
    {
        musicBoss.GetComponent<AudioSource>().Play();
    }
}

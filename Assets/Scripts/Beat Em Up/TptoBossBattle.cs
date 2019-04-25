using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TptoBossBattle : MonoBehaviour
{
    public Transform tppoint;
    public ShakeCamera camera;
    public BattleZone BossZone;
    public Canvas BossLife;
    public AudioSource musicBoss;
    //public GameObject musicGameplay;
    public bool changeMusic=false;
    public void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ShakeCamera>();
    }
    private void Update()
    {
        if (changeMusic == true)
        {
            //musicGameplay.GetComponent<AudioSource>().enabled = false;
            //ActivateBossMusic();
            if(BossLife!=null)
            BossLife.enabled = true;
            camera.GetComponent<Camera>().fieldOfView = 47;
        }
        /*if (BeatEmupManager.instance.pause == true)
        {
            //musicBoss.mute = false;
        }
        else if(BeatEmupManager.instance.pause == false)
            musicBoss.mute = true;*/
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
        musicBoss.DOFade(0.4f, 5f);
        musicBoss.Play();
    }
}

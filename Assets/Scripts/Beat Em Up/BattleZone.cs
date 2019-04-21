﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleZone : MonoBehaviour
{
    //private BoxCollider colider;
    public new GameObject camera;
    public GameObject UI;
    public GameObject score;
   // public Text[] namesEnemies;
    //public Image[] lifeBars;
    public Image goImage;
    public AudioSource musica;
    public BossIA boss;
    public List<EnemyMovement> enemies;
    public int id;
    public int enemiescounter;
    private void Start()
    {
        //colider = GetComponent<BoxCollider>();
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        enemiescounter = enemies.Count;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        { 
            UI.SetActive(true);
            score.SetActive(true);
            GetComponent<BoxCollider>().enabled = false;
            enemies[1].gameObject.SetActive(true);
            enemies[2].gameObject.SetActive(true);
            camera.GetComponent<ShakeCamera>().lockCamera = true;
            //colider.enabled = false;
            if(BeatEmupManager.instance.pause == true)
                musica.Play();
            else if (BeatEmupManager.instance.pause == false)
                musica.Stop();
        }
    }
    void Update()
    {
       /* if (BeatEmupManager.instance.pause == true)
             musica.Play();
         else if (BeatEmupManager.instance.pause == false)
             musica.Stop();*/
        if (enemiescounter <= 0)
        {
            Invoke("UnlockCamera", 1f);
            musica.Stop();
            SetGo();
            Invoke("StopGo", 3f);
            Invoke("Destroy", 4f);
        }
    }
    void UnlockCamera()
    {
        camera.GetComponent<ShakeCamera>().lockCamera = false;
       // camera.GetComponent<ShakeCamera>().enemiesdied = true;
    }
    void SetGo()
    {
        InvokeRepeating("Blink", 0.1f, 0.1f);
    }
    void Blink()
    {
        if (Time.fixedTime % .5 < .2)
        {
            goImage.enabled = false;
        }
        else
        {
            goImage.enabled = true;
        }
    }
    void StopGo()
    {
        CancelInvoke("Blink");
        UI.SetActive(false);
        score.SetActive(false);
        goImage.enabled = false;
    }
    private void Destroy()
    {
        Destroy(gameObject);
    }
}

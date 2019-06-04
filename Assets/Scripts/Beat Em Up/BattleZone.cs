﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BattleZone : MonoBehaviour
{
    //private BoxCollider colider;
    public new ShakeCamera camera;
    public GameObject UI;
    public GameObject score;
    public GameObject EnemyUI;
    public HealthScript Player;
    public BoxCollider enemyBlocker;
    public AudioSource goSound;
    public bool bossZone;
   // public Text[] namesEnemies;
    //public Image[] lifeBars;
    public Image goImage;
    public AudioSource musica;
    public BossIA boss;
    public GameObject pua;
    public Transform puaPos;
    public List<EnemyMovement> enemies;
    public int id;
    public int enemiescounter;
    private void Start()
    {
        //colider = GetComponent<BoxCollider>();
        //camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ShakeCamera>();
        enemiescounter = enemies.Count;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthScript>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UI.GetComponent<Canvas>().enabled = true;
            enemyBlocker.enabled = false;
            score.SetActive(true);
            GetComponent<BoxCollider>().enabled = false;
            if (!bossZone)
            {
                if(enemies.Count>=2)
                    enemies[1].gameObject.SetActive(true);
                if (enemies.Count >= 3)
                    enemies[2].gameObject.SetActive(true);
                camera.GetComponent<ShakeCamera>().lockCamera = true;
            }
        }
    }
    void Update()
    {
        if (BeatEmupManager.instance.pause == true)
            musica.mute=false;
        if (BeatEmupManager.instance.pause == false)
            musica.mute=true;
        if (!bossZone)
        {
            if (enemiescounter <= 0)
            {
                EnemyUI.SetActive(false);
                Invoke("UnlockCamera", 1f);
                //musica.DOFade(0.4f, 5f);
                //musica.Pause();
                SetGo();
                Invoke("StopGo", 2.5f);
                Invoke("Destroy", 2.8f);
                //onTime = true;
            }
        }
        else if (bossZone)
        {
            if (enemiescounter <= 1)
            {
                EnemyUI.SetActive(false);
            }
        }
    }
    void UnlockCamera()
    {
        //camera.ToUnlock();
        //Invoke("UnlockCam",2f);
        camera.enemiesdied = true;
        camera.lockCamera = false;
        
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
            goSound.Play();
        }
    }
    void StopGo()
    {
        CancelInvoke("Blink");
        UI.GetComponent<Canvas>().enabled = false;
        score.SetActive(false);
        goImage.enabled = false;
    }
    private void Destroy()
    {
        if (id == 5 || id == 7)
        {
            Instantiate(pua, enemies[0].transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}

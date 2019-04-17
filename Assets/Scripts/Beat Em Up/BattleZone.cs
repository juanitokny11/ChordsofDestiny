﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleZone : MonoBehaviour
{
    private BoxCollider colider;
    public new GameObject camera;
    public GameObject UI;
    public GameObject go;
    public Text[] namesEnemies;
    public Image[] lifeBars;
    public Image goImage;
    public AudioSource musica;
    public List<EnemyMovement> enemies;
    /*public Animator[] pivotesz1;
    public Animator[] pivotesz2;
    public Animator[] pivotesz3;
    public Animator[] pivotesz4;
    public Animator[] pivotesz5;
    public Animator[] pivotesz6;
    public Animator[] pivotesz7;
    public Animator[] pivotesz8;
    public Animator[] pivotesz9;
    public Animator[] pivotesz10;*/
    public int id;
    public int enemiescounter;
    private void Start()
    {
        colider = GetComponent<BoxCollider>();
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        enemiescounter = enemies.Count;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        { 
            UI.SetActive(true);
            enemies[1].gameObject.SetActive(true);
            enemies[2].gameObject.SetActive(true);
            camera.GetComponent<ShakeCamera>().lockCamera = true;
            colider.enabled = false;
            musica.Play();
            /* if (id == 1)
             {
                 for (int i = 0; i < pivotesz1.Length- 1; i++)
                 {
                     pivotesz1[i].Play("PivoteAnim", -1, 0);
                 }
             }
             if (id == 2)
             {
                 for (int i = 0; i < pivotesz2.Length - 1; i++)
                 {
                     pivotesz2[i].SetBool("anim", true);
                 }
             }
             if (id == 3)
             {
                 for (int i = 0; i < pivotesz3.Length - 1; i++)
                 {
                     pivotesz3[i].SetBool("anim", true);
                 }
             }
             if (id == 4)
             {
                 for (int i = 0; i < pivotesz4.Length - 1; i++)
                 {
                     pivotesz4[i].SetBool("anim", true);
                 }
             }
             if (id == 5)
             {
                 for (int i = 0; i < pivotesz5.Length - 1; i++)
                 {
                     pivotesz5[i].SetBool("anim", true);
                 }
             }
             if (id == 6)
             {
                 for (int i = 0; i < pivotesz6.Length - 1; i++)
                 {
                     pivotesz6[i].SetBool("anim", true);
                 }
             }
             if (id == 7)
             {
                 for (int i = 0; i < pivotesz7.Length - 1; i++)
                 {
                     pivotesz7[i].SetBool("anim", true);
                 }
             }
             if (id == 8)
             {
                 for (int i = 0; i < pivotesz8.Length - 1; i++)
                 {
                     pivotesz8[i].SetBool("anim", true);
                 }
             }
             if (id == 9)
             {
                 for (int i = 0; i < pivotesz9.Length - 1; i++)
                 {
                     pivotesz9[i].SetBool("anim", true);
                 }
             }
             if (id == 10)
             {
                 for (int i = 0; i < pivotesz10.Length - 1; i++)
                 {
                     pivotesz10[i].SetBool("anim", true);
                 }
             }*/
        }
    }
    void Update()
    {
        if(enemies[0])
        if (enemiescounter <= 0)
        {
            //this.gameObject.SetActive(false);
            Invoke("UnlockCamera", 1f);
            musica.Stop();
            SetGo();
            Invoke("StopGo", 5f);
        }
    }
    void UnlockCamera()
    {
        camera.GetComponent<ShakeCamera>().lockCamera = false;
        camera.GetComponent<ShakeCamera>().enemiesdied = true;
    }
    void SetGo()
    {
        InvokeRepeating("Blink", 0f, 0.05f);
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
        goImage.enabled = false;
    }
}

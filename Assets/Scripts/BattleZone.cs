﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleZone : MonoBehaviour
{
    private BoxCollider colider;
    public GameObject camera;
    public GameObject UI;
    //public GameObject cd;
    //public GameObject cdpoint;
    //public AudioSource musica;
    public List<GameObject> enemies;
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
            //UI.SetActive(true);
            enemies[2].SetActive(true);
            enemies[3].SetActive(true);
            camera.GetComponent<ShakeCamera>().lockCamera = true;
            colider.enabled = false;
            //musica.Play();
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
        if (enemiescounter <= 0)
        {
            //this.gameObject.SetActive(false);
            //UI.SetActive(false);
            Invoke("UnlockCamera", 1f);
            //musica.Stop();
            /*if (id == 4){
                Instantiate(cd, cdpoint.transform.position, Quaternion.identity, null);
            }*/
        }
    }
    void UnlockCamera()
    {
        camera.GetComponent<ShakeCamera>().lockCamera = false;
    }
}

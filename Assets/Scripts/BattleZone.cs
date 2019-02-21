using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleZone : MonoBehaviour
{
    private BoxCollider colider;
    public GameObject UI;
    public AudioSource musica;
    public GameObject Playercam;
    public GameObject camZ1;
    public GameObject camZ2;
    public GameObject camZ3;
    public GameObject camZ4;
    public GameObject camZ5;
    public GameObject camZ6;
    public GameObject camZ7;
    public GameObject camZ8;
    public GameObject camZ9;
    public GameObject camZ10;
    public GameObject[] enemies;
    public Animator[] pivotesz1;
    public Animator[] pivotesz2;
    public Animator[] pivotesz3;
    public Animator[] pivotesz4;
    public Animator[] pivotesz5;
    public Animator[] pivotesz6;
    public Animator[] pivotesz7;
    public Animator[] pivotesz8;
    public Animator[] pivotesz9;
    public Animator[] pivotesz10;
    public int id;
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
            if (id == 1)
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
            }
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

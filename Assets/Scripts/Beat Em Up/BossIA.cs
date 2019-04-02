using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIA : MonoBehaviour
{
    public enum Estados { Attack,Invoke,Walk,Idle}
    public List<Estados> boss_State;
    public Estados current_Boss_State ;
    public int random;
    void Start()
    {
        current_Boss_State = Estados.Idle;
    }
    void Update()
    {
        random = Random.Range(1, 101);
        if (random >= 40)
        {
            Debug.Log("AtaqueDebil");
        }
        else if (random < 40)
        {
            Debug.Log("AtaqueFuerte");
        }

    }
    void Attack()
    {
        random = Random.Range(1, 101);
        if (random >= 40)
        {
            Debug.Log("Ataque1");
        }
        else if (random < 40)
        {
            Debug.Log("Ataque2");
        }
    }
}

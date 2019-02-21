using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionEnemy : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            MyGameManager.getInstance().Daño(5f);
        }
    }
   
}

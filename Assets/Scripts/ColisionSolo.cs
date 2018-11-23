using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionSolo : MonoBehaviour {

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyBehaviour>().Damage(10);
        }
 
    }
}

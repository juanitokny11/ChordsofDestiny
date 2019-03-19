using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackList : MonoBehaviour
{
    public List<PlayerAttack2.ComboState> attacks;
  
    void Start()
    {
        attacks = GetComponent<PlayerAttack2>().attacks; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void DoAttacks()
    {

    }
}

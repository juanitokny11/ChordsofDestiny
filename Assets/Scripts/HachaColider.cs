using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HachaColider : MonoBehaviour {

	
 public void OnTriggerEnter(Collider other)
    { 
        if(other.CompareTag("Enemy")){
            FPSInputManager.getInstance().cursolo += FPSInputManager.getInstance().cargasolo;
            FPSInputManager.getInstance().soloBar.fillAmount= FPSInputManager.getInstance().cursolo/ FPSInputManager.getInstance().Maxsolo;
        }
	}
}

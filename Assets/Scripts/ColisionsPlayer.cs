using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColisionsPlayer : MonoBehaviour
{
    private CapsuleCollider colider;
    //public SoundPlayer sound;
private void Awake(){
 colider= GetComponent<CapsuleCollider> ();
}
  public void OnTriggerEnter(Collider other)
    { 
        if(other.CompareTag("Enemy")){
            FPSInputManager.getInstance().curHealth -= FPSInputManager.getInstance().Damage;
            FPSInputManager.getInstance().HealthBar.fillAmount= FPSInputManager.getInstance().curHealth/ FPSInputManager.getInstance().MaxHealth;
        }
        if (other.tag == "corchera")
        {

            FPSInputManager.getInstance().AddMoney(1);
           FPSInputManager.getInstance().notacogida=true;
            //sound.Play(1, 1);
        }
        if (other.tag == "negra")
        {
            FPSInputManager.getInstance().AddMoney(5);
            FPSInputManager.getInstance().notacogida=true;
            //sound.Play(1, 1);
        }
        if (other.tag == "blanca")
        {
            FPSInputManager.getInstance().AddMoney(20);
            FPSInputManager.getInstance().notacogida=true;
            //sound.Play(1, 1);
        }
        if (other.tag == "redonda")
        {
            FPSInputManager.getInstance().AddMoney(40);
            FPSInputManager.getInstance().notacogida=true;
            //sound.Play(1, 1);
        }
    }
  

}

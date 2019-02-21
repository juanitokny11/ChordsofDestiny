using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ColisionsPlayer : MonoBehaviour
{
     public void OnTriggerEnter(Collider other)
    { 
         if (other.tag == "Scene")
        {
            SceneManager.LoadScene("Boss");
        }
        if (other.tag == "corchera")
        {
             MyGameManager.getInstance().AddMoney(1);
            MyGameManager.getInstance().notacogida=true;
            //sound.Play(1, 1);
        }
        if (other.tag == "negra")
        {
             MyGameManager.getInstance().AddMoney(5);
             MyGameManager.getInstance().notacogida=true;
            //sound.Play(1, 1);
        }
        if (other.tag == "blanca")
        {
             MyGameManager.getInstance().AddMoney(20);
             MyGameManager.getInstance().notacogida=true;
            //sound.Play(1, 1);
        }
        if (other.tag == "redonda")
        {
             MyGameManager.getInstance().AddMoney(40);
             MyGameManager.getInstance().notacogida=true;
            //sound.Play(1, 1);
        }
         if (other.tag == "Clavedesol")
        {
             MyGameManager.getInstance().CargaClave();
            //sound.Play(1, 1);
        }
    }
}

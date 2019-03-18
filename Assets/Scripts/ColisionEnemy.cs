using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionEnemy : MonoBehaviour{

    public GameObject mainCamera;

    public void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("hola");
            mainCamera.GetComponent<ShakeCamera>().lockCamera = true;
        }
    }
}

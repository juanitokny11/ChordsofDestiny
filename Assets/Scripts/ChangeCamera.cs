using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{

    public Camera gameplaycam;
    public Camera  bosscam;

    // Start is called before the first frame update
    void Start()
    {

        gameplaycam.enabled = true;
        bosscam.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("1"))
     {
          gameplaycam.enabled = true;
        bosscam.enabled = false;
     }

     if (Input.GetKeyDown("2"))
     {
          gameplaycam.enabled = false;
        bosscam.enabled = true;
     }
        
    }
}

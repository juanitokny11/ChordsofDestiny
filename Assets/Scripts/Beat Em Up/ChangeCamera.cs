using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{

    public Camera gameplaycam;
    public Camera  bosscam;

    void Start()
    {
        gameplaycam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        bosscam = GameObject.FindGameObjectWithTag("BossCam").GetComponent<Camera>();
    }
}

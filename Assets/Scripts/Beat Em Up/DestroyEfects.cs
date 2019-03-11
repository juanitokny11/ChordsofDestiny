using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEfects : MonoBehaviour
{
    public float timer = 2.0f;
    void Start()
    {
        Invoke("DestroyObject", timer);
    }
    void DestroyObject()
    {
        GameObject.Destroy(this);
    }
   
}

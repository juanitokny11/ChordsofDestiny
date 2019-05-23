using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadomPos : MonoBehaviour
{
    public Transform pos;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Random.Range(-260f,-300f),27.42f,Random.Range(-5f,-9f));
    }
}

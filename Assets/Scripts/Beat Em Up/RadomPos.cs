using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadomPos : MonoBehaviour
{
    public Transform pos;
    public GameObject player;
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Random.Range(-260f,-300f),27.42f,Random.Range(-5f,-8f));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            transform.position = new Vector3(Random.Range(-260f, -300f), 27.42f, Random.Range(-5f, -8f));
        }
    }
}

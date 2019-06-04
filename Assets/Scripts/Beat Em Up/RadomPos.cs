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
        transform.position = new Vector3(Random.Range(-260f,-290f),27.42f,Random.Range(-5f,-8f));
        if (player.transform.position.x == transform.position.x && player.transform.position.z == transform.position.z)
        {
            transform.position = new Vector3(Random.Range(-260f, -290f), 27.42f, Random.Range(-5f, -8f));
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLockControl2 : MonoBehaviour
{
    public BoxCollider boxcol;
    public CapsuleCollider capscol;
    public ShakeCamera cam;

    // Start is called before the first frame update
    void Start()
    {
        boxcol = GetComponentInParent<BoxCollider>();
        capscol = GetComponent<CapsuleCollider>();
        //cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ShakeCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        /*if (other.tag == "Player")
        {
            cam.lockCamera = true;
            capscol.enabled=true;
            boxcol.enabled = false;
        }*/
        if (other.tag == "Player")
        {
            cam.lockCamera = false;
            capscol.enabled = false;
            boxcol.enabled = true;
        }
    }
}

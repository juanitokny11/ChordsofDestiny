using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    public float power = 0.2f;
    public float duration = 0.2f;
    public float slownDownAmount = 1f;
    public BoxCollider col1;
    public BoxCollider col2;
    public bool lockCamera=false;
    private bool shoultShake;
    private float initialDuration;
    private Vector3 startPosition;
    public GameObject player;       //Public variable to store a reference to the player game object
    private Vector3 offset;         //Private variable to store the offset distance between the player and camera
    void Start()
    {
        offset = transform.position - player.transform.position;
        startPosition = transform.localPosition;
        initialDuration = duration;
    }
    void Update()
    {
        if (!lockCamera)
            Unlock();
        else if (lockCamera)
            Lock();
        Shake();
    }
    public void Shake()
    {
        if (shoultShake)
        {
            if (duration > 0f) { 
                transform.localPosition = transform.localPosition + Random.insideUnitSphere * power;
                duration -= Time.deltaTime*slownDownAmount;
            }
            else
            {
                shoultShake = false;
                duration = initialDuration;
            }
        }
    }
    public void Unlock()
    {
        transform.position = new Vector3(player.transform.position.x + offset.x, transform.position.y, transform.position.z);
        col1.enabled = false;
        col2.enabled = false;
    }
    public void Lock()
    {
        transform.position = transform.position;
        col1.enabled = true;
        col2.enabled = true;
         
    }
    public bool ShouldShake
    {
        get
        {
            return shoultShake;
        }
        set
        {
            shoultShake = value;
        }
    }
}

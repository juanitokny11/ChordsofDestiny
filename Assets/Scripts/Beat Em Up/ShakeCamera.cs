using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    public float power = 0.2f;
    public float duration = 0.2f;
    public float slownDownAmount = 1f;
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
        transform.position = new Vector3 (player.transform.position.x + offset.x,transform.position.y,transform.position.z);
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

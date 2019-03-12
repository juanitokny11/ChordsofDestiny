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

    void Start()
    {
        startPosition = transform.localPosition;
        initialDuration = duration;
    }

    // Update is called once per frame
    void Update()
    {
        Shake();
    }
    public void Shake()
    {
        if (shoultShake)
        {
            if (duration > 0f) { 
                transform.localPosition = startPosition + Random.insideUnitSphere * power;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EaseColor : MonoBehaviour
{
    Image image;
    public Color iniValue;
    public Color endValue;
    public bool play;
     Color deltaValue;
     float currentTime;
    public float durationTime;

    public float delayTime;
    void Start()
    {
        image = GetComponent<Image>();
        deltaValue = endValue- iniValue;

        image.color = iniValue;

        currentTime = 0;
    }
    void Update()
    {
        if (play)
        {
            if (delayTime <= 0)
            {
                DoEasing();
            }
            else delayTime -= Time.deltaTime;
        }
    }
    private void DoEasing()
    {
        if (currentTime< durationTime)
        {
            Color value = new Color(
                Easing.ExpoEaseOut(currentTime, iniValue.r, deltaValue.r, durationTime),
                Easing.ExpoEaseOut(currentTime, iniValue.g, deltaValue.g, durationTime),
                Easing.ExpoEaseOut(currentTime, iniValue.b, deltaValue.b, durationTime),
                 Easing.ExpoEaseOut(currentTime, iniValue.a, deltaValue.a, durationTime));

            image.color = value;

            currentTime += Time.deltaTime;
        }
        else
        {
            image.color = endValue;
        }
    }
}

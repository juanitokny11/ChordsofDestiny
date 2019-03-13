using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    private Image HealthBar;
    void Awake()
    {
        HealthBar = GameObject.FindGameObjectWithTag("HealthUI").GetComponent<Image>();
    }

    // Update is called once per frame
    public void DisplayHealth(float value)
    {
        value /= 100;
        if (value < 0)
            value = 0;
        HealthBar.fillAmount = value;
    }
}

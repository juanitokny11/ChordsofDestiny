﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    private Image HealthBar;
    public Image SoloBar;
    void Awake()
    {
        HealthBar = GameObject.FindGameObjectWithTag("PlayerHealth").GetComponent<Image>();
        SoloBar = GameObject.FindGameObjectWithTag("SoloBar").GetComponent<Image>();
    }
    public void DisplayHealth(float value)
    {
        value /= 100;
        if (value < 0)
            value = 0;
        HealthBar.fillAmount = value;
    }
    public void DisplaySolo(float value)
    {
        value /= 100;
        if (value < 0)
            value = 0;
        SoloBar.fillAmount = value;
    }
}

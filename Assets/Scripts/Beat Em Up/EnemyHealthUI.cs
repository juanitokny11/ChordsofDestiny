﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    public Image HealthBar;
    public bool is_Boss;
    public void Awake()
    {
        if(!is_Boss)
            HealthBar = GameObject.FindGameObjectWithTag("HealthUI").GetComponent<Image>();
        else
            HealthBar = GameObject.FindGameObjectWithTag("BossHealth").GetComponent<Image>();
    }
    public void DisplayHealth(float value)
    {
        value /= 100;
        if (value < 0)
            value = 0;
        else
            HealthBar.fillAmount = value;
    }
}

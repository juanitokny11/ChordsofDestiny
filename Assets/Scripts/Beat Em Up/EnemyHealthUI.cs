using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    public Image HealthBar;
    public void DisplayHealth(float value)
    {
        value /= 100;
        if (value < 0)
            value = 0;
        HealthBar.fillAmount = value;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LifeControler : MonoBehaviour
{
    public GameObject enemyLifeObj;
    public Image enemyLife;
    public TextMeshProUGUI EnemyName;

    public void Start()
    {
        enemyLifeObj.SetActive(false);
    }
    public void ShowDamagedUI(float life, string name)
    {
        enemyLifeObj.SetActive(true);
        DisplayHealth(life);
        EnemyName.text = name.ToString();
    }
    public void DisplayHealth(float value)
    {
        value /= 100;
        if (value < 0)
            value = 0;
        else
            enemyLife.fillAmount = value;
    }
}

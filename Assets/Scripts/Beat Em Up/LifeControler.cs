using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LifeControler : MonoBehaviour
{
    public GameObject enemyLifeObj;
    public Image enemyLife;
    public Image enemyLifeDown;
    public Image enemyLifeMark;
    public TextMeshProUGUI EnemyName;
    public bool firstTime=true;

    public void Start()
    {
        enemyLifeObj.SetActive(false);
        firstTime = true;
    }
    public void ShowDamagedUI(float life, string name,float maxLife)
    {
        enemyLifeObj.SetActive(true);
        DisplayHealth(life,maxLife);
        EnemyName.text = name.ToString();
    }
    public void DisplayHealth(float value, float maxLife)
    {
        if (value < 0)
            value = 0;
        enemyLife.fillAmount = value/30;
        enemyLifeDown.fillAmount = maxLife / 30;
        enemyLifeMark.fillAmount = maxLife / 30;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public GameObject options;
    public GameObject audiomenu;
    public GameObject controlsmenu;
    public bool firstTime;
    public int id;
 public TextLoader[] texts;
    public void Start()
    {
        if (LanguageManager.langData.currentLanguage == LangData.Languages.English)
            MyGameSettings.getInstance().id = 1;
        else if (LanguageManager.langData.currentLanguage == LangData.Languages.Spanish)
            MyGameSettings.getInstance().id = 0;
    }
    public void Initialize()
    {
        if (firstTime)
        {
            LanguageManager.langData.currentLanguage = LangData.Languages.English;
            SetLanguage(MyGameSettings.getInstance().id);
            firstTime = false;
        }
        LanguageManager.LoadLanguage();
        texts = GetComponentsInChildren<TextLoader>();
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].Initialize();
        }
    }
    public void SetLanguage(int id)
    {
        if (id < 0 || id > 1) id = 0;

        LanguageManager.langData.currentLanguage = (LangData.Languages)id;

        LanguageManager.SaveLanguage();
        LanguageManager.LoadLanguage();
        LanguageManager.LoadConfigText();

        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].Initialize();
        }
        //LanguageManager.SaveLanguage();
        /*options.SetActive(false);
        audiomenu.SetActive(false);
        controlsmenu.SetActive(false);*/
    }
    public void MoreLanguaje()
    {
        MyGameSettings.getInstance().id++;
        if (MyGameSettings.getInstance().id > 1) MyGameSettings.getInstance().id = 0;
        SetLanguage(MyGameSettings.getInstance().id);
        UpdateTexts();
    }
    public void LessLanguaje()
    {
        MyGameSettings.getInstance().id--;
        if (MyGameSettings.getInstance().id < 0) MyGameSettings.getInstance().id = 1;
        SetLanguage(MyGameSettings.getInstance().id);
        UpdateTexts();
    }

    public void UpdateTexts()
    {
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].Initialize();
        }
    }
}

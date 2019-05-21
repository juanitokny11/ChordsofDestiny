using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public GameObject options;
    public GameObject audiomenu;
    public GameObject controlsmenu;
    public int id;
 public TextLoader[] texts;
    public void Start()
    {
        id = 1;
    }
    public void Initialize()
    {
        LanguageManager.langData.currentLanguage = LangData.Languages.English;
        SetLanguage(1);
        LanguageManager.SaveLanguage();
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

        LanguageManager.LoadConfigText();
        LanguageManager.SaveLanguage();
        LanguageManager.LoadLanguage();

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
        id++;
        if (id > 1) id = 0;
        SetLanguage(id);
        UpdateTexts();
    }
    public void LessLanguaje()
    {
        id--;
        if (id < 0) id = 1;
        SetLanguage(id);
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

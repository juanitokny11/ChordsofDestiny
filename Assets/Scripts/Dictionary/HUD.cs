using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public GameObject options;
    public GameObject audiomenu;
    public GameObject controlsmenu;
 public TextLoader[] texts;
 public void Initialize()
    {
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

        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].Initialize();
        }
        LanguageManager.SaveLanguage();
        options.SetActive(false);
        audiomenu.SetActive(false);
        controlsmenu.SetActive(false);
    }
}

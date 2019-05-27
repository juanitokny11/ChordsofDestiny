using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsTitle : MonoBehaviour
{
    public GameObject OptionsText;
    public Sprite OptionsimgEng;
    public Sprite OptionsimgEsp;
    public GameObject ñ;

    private void Update()
    {
        ChangeButtons();
    }
    private void ChangeButtons()
    {
        if (LanguageManager.langData.currentLanguage == LangData.Languages.English)
        {
            OptionsText.GetComponent<Image>().sprite = OptionsimgEng;
            ñ.GetComponent<Image>().enabled = false;

        }
        else if (LanguageManager.langData.currentLanguage == LangData.Languages.Spanish)
        {
            OptionsText.GetComponent<Image>().sprite = OptionsimgEsp;
            ñ.GetComponent<Image>().enabled = true;
        }
    }
}

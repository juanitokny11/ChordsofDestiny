using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LanguajeControler : MonoBehaviour
{
    public enum Languaje {SPANISH,ENGLISH};
    public Languaje actualLanguaje;
    public int change;
    public TextMeshProUGUI text;
    private void Update()
    {
        ChangeLanguaje();
    }
    public void ChageForwardLanguaje()
    {
        actualLanguaje = Languaje.SPANISH;
        LanguageManager.langData.currentLanguage = LangData.Languages.Spanish;
    }
    public void ChageBackwardLanguaje()
    {
        actualLanguaje = Languaje.ENGLISH;
        LanguageManager.langData.currentLanguage = LangData.Languages.English;
    }
    public void ChangeLanguaje ()
    {
        if (LanguageManager.langData.currentLanguage == LangData.Languages.Spanish)
        {
            text.text = "Español";
        }
        if (LanguageManager.langData.currentLanguage == LangData.Languages.English)
        {
            text.text = "English";
            LanguageManager.langData.currentLanguage = LangData.Languages.English;
        }

    }
 
}

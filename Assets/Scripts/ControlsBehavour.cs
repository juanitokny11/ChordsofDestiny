using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsBehavour : MonoBehaviour
{
    public GameObject[] controles;
    public Image[] xboxImage;
    public Image[] ps4Image;
    public int actualControler=0;
    public Sprite pcEng;
    public Sprite pcEsp;
    public Sprite XboxEng;
    public Sprite XboxEsp;
    public Sprite Xbox2Eng;
    public Sprite Xbox2Esp;
    public Sprite ps4Eng;
    public Sprite ps4Esp;
    public Sprite ps42Eng;
    public Sprite ps42Esp;
    public Text text;
    private void Update()
    {
        ViewControlers();
    }
    public void ChageForwardControler()
    {
        actualControler++;
        if (actualControler > 2)
            actualControler = 0;
    }
    public void ChageBackwardControler()
    {
        actualControler--;
        if (actualControler < 0)
            actualControler = 2;
    }
    public void ViewControlers()
    {
        if (actualControler == 0)
        {
            controles[0].SetActive(true);
            controles[1].SetActive(false);
            controles[2].SetActive(false);
            if(LanguageManager.langData.currentLanguage== LangData.Languages.English)
            {
                text.text = "Keyboard";
                controles[0].GetComponent<Image>().sprite = pcEng;
            }else if (LanguageManager.langData.currentLanguage == LangData.Languages.Spanish)
            {
                text.text = "Teclado";
                controles[0].GetComponent<Image>().sprite = pcEsp;
            }
        }
        else if(actualControler==1)
        {
            controles[0].SetActive(false);
            controles[1].SetActive(true);
            controles[2].SetActive(false);
            if (LanguageManager.langData.currentLanguage == LangData.Languages.English)
            {
                text.text = "Xbox Controller";
                xboxImage[0].sprite = XboxEng;
                xboxImage[1].sprite = Xbox2Eng;
            }
            else if (LanguageManager.langData.currentLanguage == LangData.Languages.Spanish)
            {
                text.text = "Mando de Xbox";
                xboxImage[0].sprite = XboxEsp;
                xboxImage[1].sprite = Xbox2Esp;
            }
        }
        else if (actualControler == 2)
        {
            controles[0].SetActive(false);
            controles[1].SetActive(false);
            controles[2].SetActive(true);
            if (LanguageManager.langData.currentLanguage == LangData.Languages.English)
            {
                text.text = "Ps4 Controller";
                ps4Image[0].sprite = ps4Eng;
                ps4Image[1].sprite = ps42Eng;
            }
            else if (LanguageManager.langData.currentLanguage == LangData.Languages.Spanish)
            {
                text.text = "Mando de Ps4";
                ps4Image[0].sprite = ps4Esp;
                ps4Image[1].sprite = ps42Esp;

            }
        }
    }
}

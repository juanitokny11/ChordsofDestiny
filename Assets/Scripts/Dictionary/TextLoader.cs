using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLoader : MonoBehaviour
{
    private Text text;
    public string key;

    public void Initialize()

    {
        text = GetComponent<Text>();

        string value = "";
        if(!LanguageManager.langData.ConfigDict.TryGetValue(key, out value))
        {
            value = "ERROR";
        }

        text.text = value;
    }
}

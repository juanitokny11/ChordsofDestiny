using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextLoader : MonoBehaviour
{
    private TextMeshProUGUI text;
    public string key;

    public void Initialize()

    {
        text = GetComponent<TextMeshProUGUI>();

        string value = "";
        if(!LanguageManager.langData.ConfigDict.TryGetValue(key, out value))
        {
            value = "ERROR";
        }

        text.text = value;
    }
}

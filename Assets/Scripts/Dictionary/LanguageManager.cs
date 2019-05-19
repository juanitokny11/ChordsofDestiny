using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Xml.Serialization;

[System.Serializable]
public class LangData
{
    public enum Languages { Spanish, English };
    public Languages currentLanguage;

    [XmlIgnore]
    public Dictionary<string, string> ConfigDict;
    public LangData()
    {
        //SystemLanguage systemLang = Application.systemLanguage;

        //if (systemLang == SystemLanguage.Spanish) currentLanguage = Languages.Spanish;
        currentLanguage = Languages.English;

        Debug.Log(currentLanguage);
        ConfigDict = new Dictionary<string, string>();
    }
}
public static class LanguageManager 
{
    public static LangData langData;
   public static void LoadLanguage()
    {
        try
        {
            string path = Application.persistentDataPath + "/Data";
            langData = (LangData)DataManager.LoadFromText<LangData>("Languaje", path);
        }
        catch 
        {
            langData = new LangData();
            SaveLanguage();
        }
        //load all text
        LoadConfigText();
    }
    public static void SaveLanguage()
    {
        try
        {
            string path = Application.persistentDataPath + "/Data";
            DataManager.SaveToText<LangData>(langData, "Languaje", path);
        }
        catch(Exception e)
        {
            Debug.Log("Save lan error:"+ e);
        }
    }
    public static void LoadConfigText()
    {
        try
        {
            langData.ConfigDict = new Dictionary<string, string>();
            langData.ConfigDict.Clear();
            string fullText = LoadTextFromFile("TextData/ConfigText");
            string[] lines = ReadLinesFromString(fullText);

            for (int i = 1; i < lines.Length; i++)
            {
                string[] cols = lines[i].Split('\t');

                switch (langData.currentLanguage)
                {
                    case LangData.Languages.Spanish:
                        langData.ConfigDict.Add(cols[0], cols[1]);
                        break;
                    case LangData.Languages.English:
                        langData.ConfigDict.Add(cols[0], cols[2]);
                        break;
                    default:
                        break;
                }
            }
        }
        catch(Exception e)
        {
            Debug.LogError("LoadconfigText ERROR: "+ e);
        }
    }
    public static string LoadTextFromFile(string pathfile)
    {
        TextAsset asset = Resources.Load<TextAsset>(pathfile);
        return asset.text;
    }
    public static string[] ReadLinesFromString(string text)
    {
        StringReader reader = new StringReader(text);
        List<string> lines = new List<string>();
        while (true)
        {
            string oneLine = reader.ReadLine();
            if (oneLine != null) lines.Add(oneLine);
            else break;
        }

        return lines.ToArray();
    }
}

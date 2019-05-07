using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class GameData
{ 
    public string race;
    public string name;
    public int hp;
    public int armor;
    public int mana;
    public List<InventoryElement> inventory; 


    public GameData()
    {
        race = "None";
        name = "Insert Name";
        hp = 100;
        armor = 0;
        mana = 20;
    }
}
public static class  GameDataManager
{
  public static void Save<T>(object data,string fileName)
    {
        string path = Application.persistentDataPath + "/Data";

        try
        {
            //DataManager.SaveToXML<GameData>(data, fileName, path);
            DataManager.SaveToText<T>(data, fileName, path);
            Debug.Log("[GDM] SAVE SUCCEED!");
        }
        catch(Exception e)
        {
            Debug.LogError("[GDM] SAVE ERROR:" + e);
        }
    }
    public static object Load<T> (string fileName)
    {
        string path = Application.persistentDataPath + "/Data";
        object data;
     
        try
        {
            //data =(GameData)DataManager.LoadFromXML<T>(fileName + "xml", path);
            data = (object)DataManager.LoadFromText<T>(fileName, path);
            Debug.Log("[GDM] LOAD SUCCEED!");
        }
        catch (Exception e)
        {
            Debug.LogError("[GDM] LOAD ERROR:" + e);
            data = NewGame();
        }
        return data;
    }
    public static GameData NewGame()
    {
        return new GameData();
    }
    public static void Delete(string fileName)
    {
        string filePath = Application.persistentDataPath + "/Data/"+ fileName;

        DataManager.DeleteFile(filePath);
    }
    public static bool DataExists (String fileName)
    {
        string filePath = Application.persistentDataPath + "/Data/" + fileName;

         return DataManager.FileExists(filePath);
    }
}

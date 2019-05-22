using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class GameManager : MonoBehaviour
{
    public string saveFileName = "GameData.save";
    // public GameData gameData;
    public MyGameData gameData;
    void Start()
    {
        Debug.Log(Application.persistentDataPath);
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.AltGr))
        {
            if (Input.GetKey(KeyCode.S)) GameDataManager.Save<MyGameData>(gameData, saveFileName);
            if (Input.GetKey(KeyCode.L)) gameData = (MyGameData)GameDataManager.Load<MyGameData>(saveFileName);
           if (Input.GetKey(KeyCode.D)) GameDataManager.Delete(saveFileName);
          // if (Input.GetKey(KeyCode.N)) gameData = GameDataManager.NewGame();            
        }
      
    }
}*/
public class GameManager : MonoBehaviour
{
    private HUD hud;
    public bool actualize;
    void Start()
    {
        LanguageManager.LoadLanguage();
        Debug.Log(LanguageManager.langData.currentLanguage);
        hud = GameObject.FindObjectOfType<HUD>();
        LanguageManager.langData.currentLanguage = LangData.Languages.English;
        hud.Initialize();
    }
    private void Update()
    {
        if (MyGameSettings.getInstance().actualize)
        {
            hud.Initialize();
            MyGameSettings.getInstance().actualize = false;
        }
           
    }
}

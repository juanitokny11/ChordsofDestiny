using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MyGameSettings : MonoBehaviour {

	private static MyGameSettings instance;
    public Dropdown drop;
    public bool Fullscreen;

    void Awake (){
		if (instance == null) {
			instance = this;
            DontDestroyOnLoad (this);
		}
	}
	public static MyGameSettings getInstance(){
		return instance;
	}
    public void ChangeLevel()
    {
        if (drop.value == 0)
        {
            QualitySettings.SetQualityLevel(1);
        }
        else if (drop.value == 1)
        {
            QualitySettings.SetQualityLevel(2);
        }
        else if (drop.value == 2)
        {
            QualitySettings.SetQualityLevel(3);
        }
        else if (drop.value == 3)
        {
            QualitySettings.SetQualityLevel(4);
        }

    }
    public void ChangeResolution()
    {
        if (drop.value == 0)
        {
            Screen.SetResolution(1280, 720, Fullscreen);
        }
        else if (drop.value == 1)
        {
            Screen.SetResolution(1600, 1200, Fullscreen);
        }
        else if (drop.value == 2)
        {
            Screen.SetResolution(1920, 1080, Fullscreen);
        }

    }

}


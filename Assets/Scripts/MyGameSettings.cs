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
   
}


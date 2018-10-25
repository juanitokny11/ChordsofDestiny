using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameSettings : MonoBehaviour {

	private static MyGameSettings instance;
    
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


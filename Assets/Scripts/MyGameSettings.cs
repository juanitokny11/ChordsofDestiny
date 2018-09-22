using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameSettings : MonoBehaviour {

	private static MyGameSettings instance;
    public int scorefinal;
	public int mintotal;
    public int sectotal;

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


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class MyGameSettings : MonoBehaviour {

	private static MyGameSettings instance;
    public Dropdown drop;
	public AudioMixer mixer;
	public float generalVol;
	public float musicVol;
	public float effectsVol;
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


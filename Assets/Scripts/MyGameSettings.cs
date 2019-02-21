using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class MyGameSettings : MonoBehaviour {

	private static MyGameSettings instance;
    public Dropdown drop;
	public AudioMixer mixer;
	public float generalVol=0.5f;
	public float musicVol=0.5f;
	public float effectsVol=0.5f;
    public bool gameStarted;
    public bool logoPlayed;
   
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


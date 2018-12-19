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

	public void SetLevel( ){
    mixer.SetFloat("soundVol",Mathf.Log10(generalVol)*20);
    
	}
	public void SetMusicLevel()
	{
        mixer.SetFloat("MusicVol", Mathf.Log10(musicVol) * 20);
       
	}
	public void SetEffectsLevel()
	{
        mixer.SetFloat("EffectsVol", Mathf.Log10(effectsVol) * 20);
	}	
   
}


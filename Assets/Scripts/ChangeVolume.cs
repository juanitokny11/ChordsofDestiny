using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ChangeVolume : MonoBehaviour {
    public Slider generalVolume;
    
    public Slider musicVolume;
    
    public Slider effectsVolume;
	public AudioMixer mixer;
public void Start(){
    /*PlayerPrefs.GetFloat("volumeG",generalVolume.value);
    PlayerPrefs.GetFloat("volumeM",musicVolume.value);
    PlayerPrefs.GetFloat("volumeE",effectsVolume.value);*/
}
public void SetLevel(float sliderValue){
    mixer.SetFloat("soundVol",Mathf.Log10(sliderValue)*20);
}
    public void SetMusicLevel(float sliderValue)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }
    public void SetEffectsLevel(float sliderValue)
    {
        mixer.SetFloat("EffectsVol", Mathf.Log10(sliderValue) * 20);
    }
public void SaveButtons(){
    PlayerPrefs.SetFloat("volumeG",generalVolume.value);
    PlayerPrefs.SetFloat("volumeM",musicVolume.value);
    PlayerPrefs.SetFloat("volumeE",effectsVolume.value);
}
}

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
}



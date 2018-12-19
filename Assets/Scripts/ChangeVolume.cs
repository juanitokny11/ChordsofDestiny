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
public void SetLevel(){
   // mixer.SetFloat("soundVol",Mathf.Log10(sliderValue)*20);
     MyGameSettings.getInstance().SetLevel();
     MyGameSettings.getInstance().generalVol= generalVolume.value;
}
public void SetMusicLevel()
{
        //mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
        MyGameSettings.getInstance().SetMusicLevel();
        MyGameSettings.getInstance().musicVol= musicVolume.value;
       
}
public void SetEffectsLevel()
{
        //mixer.SetFloat("EffectsVol", Mathf.Log10(sliderValue) * 20);
        MyGameSettings.getInstance().SetEffectsLevel();
        MyGameSettings.getInstance().effectsVol= effectsVolume.value;
}

}



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
    public Text general;
    public Text music;
    public Text efects;
    public float startValue = 0.5f;


    public void Start()
    {
        float v;
        //generalVolume.value = MyGameSettings.getInstance().generalVol;
        
        mixer.GetFloat("MusicVol", out v);
        musicVolume.value = v;

       // effectsVolume.value = MyGameSettings.getInstance().effectsVol;
    }
    private void Update()
    {
        //general.text = (int)(generalVolume.value) + " %";
       /// music.text = (int)(musicVolume.value * 100) + " %";
        //efects.text = (int)(effectsVolume.value * 100) + " %";
        //MyGameSettings.getInstance().generalVol = generalVolume.value;
        //MyGameSettings.getInstance().musicVol = musicVolume.value;
        //MyGameSettings.getInstance().effectsVol = effectsVolume.value;
    }
    public void SetLevel(){
        mixer.SetFloat("soundVol",Mathf.Log10(generalVolume.value) *20);
    }
    public void SetMusicLevel()
    {
        mixer.SetFloat("MusicVol", musicVolume.value);
    }
    public void SetEffectsLevel(float sliderValue)
    {
        mixer.SetFloat("EffectsVol", Mathf.Log10(sliderValue) * 20);
    }
}



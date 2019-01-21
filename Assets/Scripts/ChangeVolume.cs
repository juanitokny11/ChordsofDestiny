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
        generalVolume.value = MyGameSettings.getInstance().generalVol;
        musicVolume.value = MyGameSettings.getInstance().musicVol;
        effectsVolume.value = MyGameSettings.getInstance().effectsVol;
    }
    private void Update()
    {
        general.text = (int)(generalVolume.value * 100) + " %";
        music.text = (int)(musicVolume.value * 100) + " %";
        efects.text = (int)(effectsVolume.value * 100) + " %";
        MyGameSettings.getInstance().generalVol = generalVolume.value;
        MyGameSettings.getInstance().musicVol = musicVolume.value;
        MyGameSettings.getInstance().effectsVol = effectsVolume.value;
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



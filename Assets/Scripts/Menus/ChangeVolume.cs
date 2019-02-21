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
        mixer.GetFloat("soundVol", out v);
        generalVolume.value = v;
        mixer.GetFloat("MusicVol", out v);
        musicVolume.value = v;
        mixer.GetFloat("EffectsVol", out v);
        effectsVolume.value = v;
       }
    private void Update()
    {
        general.text = (int)(generalVolume.value) + " db";
        music.text = (int)(musicVolume.value) + " db";
        efects.text = (int)(effectsVolume.value) + " db";
        //MyGameSettings.getInstance().generalVol = generalVolume.value;
        //MyGameSettings.getInstance().musicVol = musicVolume.value;
        //MyGameSettings.getInstance().effectsVol = effectsVolume.value;
    }
    public void SetLevel(){
        mixer.SetFloat("soundVol",generalVolume.value);
    }
    public void SetMusicLevel()
    {
        mixer.SetFloat("MusicVol", musicVolume.value);
    }
    public void SetEffectsLevel()
    {
        mixer.SetFloat("EffectsVol",effectsVolume.value);
    }
}



﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ChangeVolume : MonoBehaviour {

	public AudioMixer mixer;
public void SetLevel(float sliderValue){
    mixer.SetFloat("musicVol",Mathf.Log10(sliderValue)*20);
}
}

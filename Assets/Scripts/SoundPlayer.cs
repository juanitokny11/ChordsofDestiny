using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public AudioClip[] clips;

    public void Play(int numClip, float volume)
    {
        GameObject obj = new GameObject("Sound_" + clips[numClip].name);
        obj.transform.position = this.transform.position;
        AudioSource source = obj.AddComponent<AudioSource>();
        source.volume = volume;
        source.clip = clips[numClip];
        source.loop = false;

        source.spatialBlend = 0.8f;
        source.playOnAwake = false;
        source.Play();
        Destroy(obj, clips[numClip].length);
    }
}

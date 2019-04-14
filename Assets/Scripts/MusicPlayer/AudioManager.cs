using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public AudioClip[] musicList;
    private int currentTrack;
    private AudioSource source;
    public Text clipTittleText;
    public Text clipTittleTime;
    public Sprite[] group;
    public Image Pantalla;
    private int minutes;
    private int seconds;
    private int playTime;
    private int fullLength;
    private bool playing;
    private int groupId;

    void Start()
    {
        source = GetComponent<AudioSource>();
        playing = false;
    }
    public void Update()
    {
        if (groupId == 0)
            Pantalla.sprite = group[0];
        else if (groupId ==1)
            Pantalla.sprite = group[1];
        else if (groupId == 2)
            Pantalla.sprite = group[2];
        else if (groupId == 3)
            Pantalla.sprite = group[3];
        if (groupId > group.Length-1)
            groupId = 0;
        if (groupId < 0)
            groupId = group.Length - 1;
    }
    public void PlayMusic()
    {
        playing = !playing;
        if (playing)
        { 
            if (source.isPlaying)
                return;
            playing = true;
            currentTrack--;
            groupId--;
            if (currentTrack < 0)
                currentTrack = musicList.Length - 1;
            StartCoroutine("WaitForMusicEnd");
        }
        else if (!playing)
        {
            StopCoroutine("WaitForMusicEnd");
            source.Stop();
        }
    }
    IEnumerator WaitForMusicEnd()
    {
        while(source.isPlaying)
        {
            playTime = (int)source.time;
            //ShowPlayTime();
            yield return null;
        }
        NextTrack();
    }
    public void NextTrack()
    {
        if (!playing)
            return;
        currentTrack++;
        groupId++;
        if (currentTrack > musicList.Length - 1)
            currentTrack = 0;
        source.clip = musicList[currentTrack];
        source.Play();

        //show title
        ShowCurrentTitle();
        //ShowPlayTime();

        StartCoroutine("WaitForMusicEnd");
    }
    public void PreviousTrack()
    {
        if (!playing)
            return;
        currentTrack--;
        groupId--;
        if (currentTrack < 0)
            currentTrack = musicList.Length - 1;
        source.clip = musicList[currentTrack];
        source.Play();

        //show title
        ShowCurrentTitle();
        //ShowPlayTime();

        StartCoroutine("WaitForMusicEnd");
    }
    public void StopMusic()
    {
        StopCoroutine("WaitForMusicEnd");
        source.Stop();
    }
    public void MuteMusic()
    {
        source.mute = !source.mute;
    }
    void ShowCurrentTitle()
    {
        clipTittleText.text = source.clip.name;
        fullLength = (int)source.clip.length;
    }
   /* void ShowPlayTime()
    {
        seconds = playTime % 60;
        minutes = (playTime / 60) % 60;
        clipTittleTime.text = minutes + ":" + seconds.ToString("D2") + "/" + ((fullLength / 60) % 60) + ":" + (fullLength % 60);
    }*/
}


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
    public Image Play;
    public Sprite playSprite;
    public Sprite pauseSprite;
    private int minutes;
    private int seconds;
    private int playTime;
    private int fullLength;
    public bool playing;
    private int groupId;
    public SpriteState playState = new SpriteState();
    public SpriteState pauseState = new SpriteState();

    void Start()
    {
        source = GetComponent<AudioSource>();
        playing = false;
        Play.GetComponent<Button>().spriteState = pauseState;
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
        else if (groupId == 4)
            Pantalla.sprite = group[4];
        if (groupId > group.Length-1)
            groupId = 0;
        if (groupId < 0)
            groupId = group.Length - 1;
        if (playing)
        {
            Play.GetComponent<Image>().sprite = pauseSprite;
            Play.GetComponent<Button>().spriteState = playState;
            playState.pressedSprite = playSprite;
            playState.highlightedSprite = pauseSprite;
            playState.disabledSprite = playSprite;
        }
        if (!playing)
        {
            if (source.isPlaying)
                source.Stop();
            Play.GetComponent<Image>().sprite = playSprite;
            Play.GetComponent<Button>().spriteState = pauseState;
            pauseState.pressedSprite = pauseSprite;
            pauseState.highlightedSprite = playSprite;
            pauseState.disabledSprite = pauseSprite;
        }
            
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
            source.Pause(); ;
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
        //if (!playing)
           // return;
        currentTrack++;
        groupId++;
        if (currentTrack > musicList.Length - 1)
            currentTrack = 0;
        source.clip = musicList[currentTrack];
        source.Play();

        //show title
        ShowCurrentTitle();
        //ShowPlayTime();
        if(playing)
            StartCoroutine("WaitForMusicEnd");
    }
    public void PreviousTrack()
    {
        //if (!playing)
            //return;
        currentTrack--;
        groupId--;
        if (currentTrack < 0)
            currentTrack = musicList.Length - 1;
        source.clip = musicList[currentTrack];
        source.Play();

        //show title
        ShowCurrentTitle();
        //ShowPlayTime();
        if (playing)
            StartCoroutine("WaitForMusicEnd");
    }
    public void StopMusic()
    {
        playing = false;
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


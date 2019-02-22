using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class LogoManager : MonoBehaviour
{
    public VideoPlayer video;
    public GameObject titleText;
    public GameObject menu;
    public GameObject fondomenu;
    public GameObject title;
    public AudioSource musica;
    public GameObject fademusica;
    public GameObject reproductor;
    public bool extras=false;
    bool logo = false;
    void Start()
    {
        Cursor.visible = false;
        video.loopPointReached += EndVideo;
        if (MyGameSettings.getInstance().logoPlayed == true)
        {
            video.gameObject.SetActive(false);
            MainMenu();
            musica.volume = 0.3f;
            Cursor.visible = true;
            //fademusica.SetActive(true);
            musica.Play();
            logo = true;
        }
    }
    private void EndVideo(VideoPlayer source)
    {
        video.gameObject.SetActive(false);
        title.SetActive(true);
        titleText.SetActive(true);
        fademusica.SetActive(true);
        musica.Play();
        logo = true;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && logo==true || Input.GetAxisRaw("AtaqueDebil") != 0 && logo == true)
        {
            MainMenu();
            Cursor.visible = true;
            MyGameSettings.getInstance().logoPlayed = true;
        }
    }
    // Update is called once per frame
    private void MainMenu()
    {
        title.SetActive(false);
        titleText.SetActive(false);
        fondomenu.SetActive(true);
        menu.SetActive(true);
    }
    public void Extras()
    {
        extras = !extras;
        if (!extras)
        {
            reproductor.SetActive(false);
            musica.mute = false;
        }
        else if (extras)
        {
            reproductor.SetActive(true);
            musica.mute = true;
        }
    }

}

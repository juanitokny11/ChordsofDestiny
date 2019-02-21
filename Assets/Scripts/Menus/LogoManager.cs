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
    bool logo = false;
    void Start()
    {
        video.loopPointReached += EndVideo;
        if (MyGameSettings.getInstance().logoPlayed == true)
        {
            video.gameObject.SetActive(false);
            MainMenu();
            fademusica.SetActive(true);
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

}

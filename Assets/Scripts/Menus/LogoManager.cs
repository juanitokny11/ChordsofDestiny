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
    public GameObject title;
    public GameObject Optionsmenu;
    public AudioSource musica;
    public AudioSource musicaoptions;
    public GameObject fademusica;
    public GameObject fademusicaOptions;
    public GameObject reproductor;
    public bool extras=true;
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
        menu.SetActive(true);
    }
    public void Options()
    {
        Invoke("InvokeOptions", 130.0f * Time.deltaTime);
        fademusicaOptions.SetActive(true);
        musica.enabled = false;
        musicaoptions.enabled = true;
        musicaoptions.Play();
    }
    public void ReturnOptions()
    {
        Optionsmenu.SetActive(false);
        fademusicaOptions.SetActive(false);
        musica.enabled = true;
        musicaoptions.enabled = false;
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
            Invoke("InvokeGramola", 130.0f * Time.deltaTime);
            musica.mute = true;
        }
    }
    public void InvokeGramola()
    {
        reproductor.SetActive(true);
    }
    public void InvokeOptions()
    {
        Optionsmenu.SetActive(true);
    }
}

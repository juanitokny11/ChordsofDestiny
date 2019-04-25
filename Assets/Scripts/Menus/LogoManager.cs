using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class LogoManager : MonoBehaviour
{
    public VideoPlayer logoVideo;
    public VideoPlayer cinematicaInicial;
    public GameObject titleText;
    public GameObject menu;
    public GameObject title;
    public GameObject Optionsmenu;
    public OptionsManager options;
    public AudioSource musica;
    //public AudioSource musicaoptions;
    public GameObject fademusica;
    public GameObject fademusicaOptions;
    public GameObject reproductor;
    public MenuAnim optionsAnim;
    public bool extras=true;
    bool logo = false;
    bool cinematica = false;
    private void Awake()
    {
        //menu.SetActive(false);
    }
    void Start()
    {
        Cursor.visible = false;
        logoVideo.loopPointReached += EndCinematica;
        if (MyGameSettings.getInstance().logoPlayed == true && MyGameSettings.getInstance().cinematicaPlayed==true)
        {
            logoVideo.gameObject.SetActive(false);
            cinematicaInicial.gameObject.SetActive(false);
            MainMenu();
            musica.volume = 0.3f;
            Cursor.visible = true;
            //fademusica.SetActive(true);
            musica.Play();
            logo = true;
            cinematica = true;
        }
    }
    private void EndVideo(VideoPlayer source)
    {
        //logoVideo.gameObject.SetActive(false);
        cinematicaInicial.gameObject.SetActive(false);
        title.SetActive(true);
        titleText.SetActive(true);
        fademusica.SetActive(true);
        musica.Play();
        cinematica = true;
    }
    private void EndCinematica(VideoPlayer source)
    {
        logoVideo.gameObject.SetActive(false);
        cinematicaInicial.gameObject.SetActive(true);
        //logoVideo.gameObject.SetActive(false);
        //title.SetActive(true);
        //titleText.SetActive(true);
        //fademusica.SetActive(true);
        //musica.Play();
        logo = true;
    }
    private void Update()
    {
        cinematicaInicial.loopPointReached += EndVideo;
        if (Input.GetKeyDown(KeyCode.Return) && logo == true  || Input.GetAxisRaw("AtaqueDebil") != 0 && logo == true )
        {
            cinematicaInicial.gameObject.SetActive(false);
            title.SetActive(true);
            titleText.SetActive(true);
            fademusica.SetActive(true);
            musica.Play();
            Cursor.visible = true;
            MyGameSettings.getInstance().logoPlayed = true;
            MyGameSettings.getInstance().cinematicaPlayed = true;
            Invoke("CinematicaTrue", 1.0f);
        }
        if (Input.GetKeyDown(KeyCode.Return) && logo==true && cinematica==true || Input.GetAxisRaw("AtaqueDebil") != 0 && logo == true && cinematica==true)
        {
            MainMenu();
            Cursor.visible = true;
            MyGameSettings.getInstance().logoPlayed = true;
        }
        if (options.currentcontrol == 0)
        {
            options.controls[0].SetActive(true);
            options.controls[1].SetActive(false);
            options.controls[2].SetActive(false);
            options.texto.text = "Teclado y Ratón";
        }
        if (options.currentcontrol == 1)
        {
            options.controls[0].SetActive(false);
            options.controls[1].SetActive(true);
            options.controls[2].SetActive(false);
            options.texto.text = "Xbox";
        }
        if (options.currentcontrol == 2)
        {
            options.controls[0].SetActive(false);
            options.controls[1].SetActive(false);
            options.controls[2].SetActive(true);
            options.texto.text = "Ps4";
        }
    }
    void CinematicaTrue()
    {
        cinematica = true;
    }
    // Update is called once per frame
    private void MainMenu()
    {
        title.SetActive(false);
        titleText.SetActive(false);
        menu.GetComponent<Canvas>().enabled = true;
    }
    public void Options()
    {
        Invoke("InvokeOptions", 130.0f * Time.deltaTime);
        //fademusicaOptions.SetActive(true);
        //musica.enabled = false;
       // musicaoptions.enabled = true;
        //musicaoptions.Play();
    }
    public void ReturnOptions()
    {
        Invoke("InvokeOptionsBack", 20.0f * Time.deltaTime);
        optionsAnim.firstTime = true;
        optionsAnim.Anim = false;
        fademusicaOptions.SetActive(false);
        musica.enabled = true;
        //musicaoptions.enabled = false;
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
        optionsAnim.firstTime = true;
        optionsAnim.Anim = true;
    }
    public void InvokeOptionsBack()
    {
        Optionsmenu.SetActive(false);
    }
}

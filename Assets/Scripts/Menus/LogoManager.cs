using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class LogoManager : MonoBehaviour
{
    public VideoPlayer logoVideo;
    public Canvas titleText;
    public GameObject menu;
    public VideoPlayer title;
    public Canvas Optionsmenu;
    public Canvas videomenu;
    public OptionsManager options;
    public MenuManager menuManager;
    public AudioSource musica;
    //public AudioSource musicaoptions;
    public GameObject fademusica;
    public GameObject fademusicaOptions;
    public GameObject reproductor;
    public MenuAnim optionsAnim;
      public VideoPlayer Credits;
    public ParticleSystem niebla;
    public bool extras=true;
    public GameObject pantallanegra;
    public AudioSource creditsMusic;
    public AudioSource EnterStart;
    public GameObject pospo;
    public bool credits;
    bool logo = false;
    public bool cinematica = false;
    private void Awake()
    {
        logoVideo.Prepare();
        title.Prepare();
        pospo.SetActive(false);
        //menu.SetActive(false);
    }
    void Start()
    {
        Cursor.visible = false;
        logoVideo.loopPointReached += EndVideo;
        if (MyGameSettings.getInstance().logoPlayed == true)
        {
            logoVideo.gameObject.SetActive(false);
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
        logoVideo.gameObject.SetActive(false);
        pantallanegra.GetComponent<MeshRenderer>().enabled = true;
        title.gameObject.SetActive(true);
        title.GetComponent<MeshRenderer>().enabled = false; 
        Invoke("OffPantallaNegra",Time.deltaTime*25f);
        niebla.Play();
        pospo.SetActive(true);
        fademusica.SetActive(true);
        musica.Play();
        logo = true;
    }
    void OffPantallaNegra()
    {
        titleText.enabled = true;
        pantallanegra.GetComponent<MeshRenderer>().enabled = false;
        title.GetComponent<MeshRenderer>().enabled = true;
    }
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Return) && logo && !cinematica || Input.GetAxisRaw("AtaqueDebil") != 0 && logo && !cinematica)
        {
            if (!credits)
            {
                EnterStart.Play();
                InvokeRepeating("ParpadeoTextMenu", 0.2f, 0.2f);
            }
            Invoke( "MainMenu",1.0f);
            Cursor.visible = true;
            MyGameSettings.getInstance().logoPlayed = true;
        }
        if (Input.GetKeyDown(KeyCode.Return) && logo && cinematica|| Input.GetAxisRaw("AtaqueDebil") != 0 && logo && cinematica)
        {
            menuManager.cinematicaInicial.Pause();
            //Invoke("CinematicaTrue", 1.0f);
            SceneManager.LoadScene("Gameplay");
            Time.timeScale = 1;
            Cursor.visible = false;
            MyGameSettings.getInstance().gameStarted = true;
            MyGameSettings.getInstance().logoPlayed = true;
            MyGameSettings.getInstance().menuAnim.firstTime = true;
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
    public  void ParpadeoTextMenu()
    {
        if (Time.fixedTime % .5 < .2)
        {
            titleText.enabled = true;
        }
        else
        {
            titleText.enabled = false;
        }
    }
    public void MainMenu()
    {
        CancelInvoke("ParpadeoTextMenu");
        creditsMusic.Stop();
        title.gameObject.SetActive(false);
        titleText.enabled = false;
        menu.GetComponent<Canvas>().enabled = true;
        if (menuManager.endVideo)
        {
            ReturnCreditos();
        }
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
            musica.UnPause();
        }
        else if (extras)
        {
            Invoke("InvokeGramola", 130.0f * Time.deltaTime);
            musica.Pause();
        }
    }
    public void Creditos()
    {
        credits = true;
        optionsAnim.Anim = true;
        optionsAnim.firstTime = true;
    }
    public void ReturnCreditos()
    {
        optionsAnim.Anim = false;
        optionsAnim.firstTime = true;
        musica.UnPause();
    }
    public void InvokeGramola()
    {
        reproductor.SetActive(true);
    }
    public void InvokeOptions()
    {
        Optionsmenu.enabled=true;
        videomenu.enabled = true;
        optionsAnim.firstTime = true;
        optionsAnim.Anim = true;
    }
    public void InvokeOptionsBack()
    {
        Optionsmenu.enabled=false;
    }
}

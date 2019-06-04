using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MenuManager : MonoBehaviour {

    public Animator cam_Anim;
    public GameObject options;
    public GameObject video;
    public GameObject controles;
    public LogoManager logoManager;
    public MenuAnim menuAnim;
    public bool is_MainMenu = false;
    public VideoPlayer cinematicaInicial;
    public VideoClip VideoEng;
    public VideoClip VideoEsp;
    public Canvas skipcanvas;
    public float counter=0;
    public float countStart = 0;
    public VideoPlayer Credits;
    public AudioSource creditsMusic;
    public bool enableCredits;
    public bool endVideo = false;

    public void Start()
    {
        if (is_MainMenu)
        {
            enableCredits = false;
            endVideo = false;
            Credits.Prepare();
            Cursor.visible = true;
            cinematicaInicial.gameObject.SetActive(false);
            cinematicaInicial.Prepare();
        } 
    }
    public void Update()
    {  
            if (is_MainMenu)
        {
            if (enableCredits)
            {
                Credits.loopPointReached += EndVideo;
                skipcanvas.enabled = true;
            }
            if (enableCredits && Input.GetKeyDown(KeyCode.Return) || Input.GetAxisRaw("AtaqueDebil") != 0 && enableCredits)
            {
                Credits.gameObject.SetActive(false);
                Cursor.visible = true;
                ReturnCreditos();
                logoManager.MainMenu();
                endVideo = true;
                skipcanvas.enabled = false;
            }
            if (skipcanvas.enabled)
                counter++;
            if (counter >= 1500)
            {
                skipcanvas.enabled = false;
            }
            if (LanguageManager.langData.currentLanguage == LangData.Languages.English)
            {
                cinematicaInicial.clip = VideoEng;
            }
            else if (LanguageManager.langData.currentLanguage == LangData.Languages.Spanish)
            {
                cinematicaInicial.clip = VideoEsp;
            }
        }
    }
    private void EndVideo(VideoPlayer source)
    {
        Credits.gameObject.SetActive(false);
        Cursor.visible = true;
        ReturnCreditos();
        logoManager.MainMenu();
        endVideo = true;
        skipcanvas.enabled = false;
        //logoManager.optionsAnim.firstTime = true;
    }
    IEnumerator waitForMovieEnd()
    {

        while (cinematicaInicial.isPlaying) // while the movie is playing
        {
           
            yield return new WaitForEndOfFrame();
        }
        // after movie is not playing / has stopped.
        onMovieEnded();
    }

    void onMovieEnded()
    {
        cinematicaInicial.Pause();
        Time.timeScale = 1;
        Cursor.visible = false;
        MyGameSettings.getInstance().gameStarted = true;
        MyGameSettings.getInstance().logoPlayed = true;
        MyGameSettings.getInstance().menuAnim.firstTime = true;
        MyGameSettings.getInstance().actualize = true;
        SceneManager.LoadScene("Gameplay");
    }
    public void PlayGame()
    {
        if (is_MainMenu)
        {
            cinematicaInicial.gameObject.SetActive(true);
            Cursor.visible = false;
            cinematicaInicial.Play();
            skipcanvas.enabled = true;
            StartCoroutine("waitForMovieEnd");
            logoManager.cinematica = true;
            MyGameSettings.getInstance().menuAnim.Anim = true;
            MyGameSettings.getInstance().menuAnim.firstTime = true;
        }
        else
        {
            Time.timeScale = 1;
            Cursor.visible = false;
            MyGameSettings.getInstance().gameStarted = true;
            MyGameSettings.getInstance().logoPlayed = true;
            //MyGameSettings.getInstance().menuAnim.firstTime = true;
            MyGameSettings.getInstance().actualize = true;
            SceneManager.LoadScene("Gameplay");
        }
    }
    public void EndGame()
    {
        Application.Quit();
    }
    public void Options()
    {
        cam_Anim.SetTrigger("Options");
        Cursor.visible = true;
        MyGameSettings.getInstance().menuAnim.firstTime = true;
        MyGameSettings.getInstance().menuAnim.Anim = true;
    }
    public void Extras()
    {
        cam_Anim.SetTrigger("Extras");
        Cursor.visible = true;
        MyGameSettings.getInstance().menuAnim.Anim = true;
        MyGameSettings.getInstance().menuAnim.firstTime = true;
    }
    public void Options_to_Menu()
    {
        cam_Anim.SetTrigger("Options_to_menu");
        Cursor.visible = true;
        MyGameSettings.getInstance().menuAnim.Anim = false;
    }
    public void Extra_to_Menu()
    {
        Cursor.visible = true;
        cam_Anim.SetTrigger("Extra_to_menu");
        MyGameSettings.getInstance().menuAnim.Anim = false;
    }
    public void Creditos()
    { 
        Credits.gameObject.SetActive(true);
        enableCredits = true;
        Cursor.visible = false;
        MyGameSettings.getInstance().menuAnim.Anim = true;
        MyGameSettings.getInstance().menuAnim.firstTime = true;
        //Time.timeScale = 1;
    }
    public void ReturnCreditos()
    {
        Credits.gameObject.SetActive(false);
        enableCredits = false;
        Cursor.visible = true;
        MyGameSettings.getInstance().menuAnim.Anim = false;
        MyGameSettings.getInstance().menuAnim.firstTime = true;
        //Time.timeScale = 1;
    }
    public void Title(){
        SceneManager.LoadScene("New Scene");
        Cursor.visible = true;
        Time.timeScale=1;
        //cinematicaInicial.gameObject.SetActive(false);
        MyGameSettings.getInstance().menuAnim.firstTime = true;
        MyGameSettings.getInstance().menuAnim.Anim = true;
        MyGameSettings.getInstance().gameStarted = true;
        MyGameSettings.getInstance().logoPlayed = true;
        MyGameSettings.getInstance().actualize = true;
    }
	public void Victory(){
		SceneManager.LoadScene("Victoria");
        Cursor.visible = true;
        MyGameSettings.getInstance().actualize = true;
    }
    public void Pause()
    {
        BeatEmupManager.instance.Pausa();
    }
    public void ChangeScene()
    {
            SceneManager.LoadScene("Gameplay");
            Time.timeScale = 1;
            Cursor.visible = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class BeatEmupManager : MonoBehaviour
{
    public static BeatEmupManager instance;
    public GameObject Player;
    public bool notacogida;
    public TextMeshProUGUI score;
    public float counternotas;
    public GameObject notas;
    public AudioSource music;
    public AudioSource musicGameplay;
    public AudioSource openPause;
    public AudioSource closePause;
    public Canvas optionsCanvas;
    public Canvas confExit;
    public Canvas confMenu;
    public Canvas videoCanvas;
    public Canvas audioCanvas;
    public Canvas controlsCanvas;
    public GameObject mainMenu;
    public bool firstTime;
    public Transform Tppos;
    public bool godmode = true;
    public Canvas pausaMenu;
    public RectTransform optionsMenu;
    public Animator pauseMenuPrincial;
    public bool pause = false;
    public int numScore;
    public EaseColor startFade;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        //Get Joystick Names
        string[] temp = Input.GetJoystickNames();

        //Check whether array contains anything
        if (temp.Length > 0)
        {
            //Iterate over every element
            for (int i = 0; i < temp.Length; ++i)
            {
                //Check if the string is empty or not
                if (!string.IsNullOrEmpty(temp[i]))
                {
                    //Not empty, controller temp[i] is connected
                    Debug.Log("Controller " + i + " is connected using: " + temp[i]);
                }
                else
                {
                    //If it is empty, controller i is disconnected
                    //where i indicates the controller number

                    Debug.Log("Controller: " + i + " is disconnected.");

                }
            }
        }
        startFade.play = true;
        Player = GameObject.FindGameObjectWithTag("Player");
        Cursor.visible = false;
        firstTime = true;
        if (BeatEmupManager.instance.pause == true)
        {
            //ESTO ES PARA CAMBIAR EL VOLUMEN DE LA MUSICA DE GAMEPLAY
            musicGameplay.DOFade(0.15f, 2f);
            musicGameplay.Play();
        }
    }
    void Update()
    {
        score.text = numScore.ToString();
        if (Input.GetKeyDown(KeyCode.F10))
        {
            GodMode();
        }
        if (Input.GetKeyDown(KeyCode.F8))
        {
            Player.transform.position = Tppos.position;
        }
        if (godmode == true)
        {

            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Pause"))
            {
                Pausa();
            }
        }
    }
    public void Pausa()
    {
        if (!pause)
        {
            Time.timeScale = 1;
            closePause.Play();
            firstTime = true;
            //pauseMenuPrincial.SetBool("Pausa", true);
            Cursor.visible = false;
            musicGameplay.mute = false;
            optionsCanvas.enabled = false;
            confExit.enabled = false;
            confMenu.enabled = false;
            videoCanvas.enabled = false;
            audioCanvas.enabled = false;
            controlsCanvas.enabled = false;
            //notas.SetActive(false);
            Invoke("TakeoFFMenu", 0.2f);
            pause = true;
        }
        else if (pause)
        {
            openPause.Play();
            //pauseMenuPrincial.SetBool("Pausa", false);
            //notas.SetActive(true);
            if (firstTime)
            {
                mainMenu.SetActive(true);
                firstTime = false;
            }
            musicGameplay.mute = true;
            Cursor.visible = true;
            Invoke("TakeONMenu", 0.2f);
            pause = false; 
        }
    }
    private void TakeoFFMenu()
    {
        pausaMenu.enabled = false;
        //optionsMenu.gameObject.SetActive(false);
        music.Stop();
    }
    private void TakeONMenu()
    {
        pausaMenu.enabled = true;
        //firstTime = true;
        music.Play();
    }
    public void Time0()
    {
        Time.timeScale = 0;
    }
    public void GodMode()
    {
        if (!godmode)
        {
            godmode = true;
            //Player.GetComponent<CapsuleCollider>().enabled = true;
            Player.gameObject.layer = 0;
            //Cursor.visible = false;
        }
        else if (godmode)
        {
            godmode = false;
            //Player.GetComponent<CapsuleCollider>().enabled = true;
            Player.gameObject.layer = 8;
            //Cursor.visible = true;
        }
    }
}

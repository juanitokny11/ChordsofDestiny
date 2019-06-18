using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public Image tutoblock;
    public Sprite tutoblockPc;
    public Sprite tutoblockXbox;
    public Image tutodebil;
    public Sprite tutodebilPc;
    public Sprite tutodebilXbox;
    public Image tutofuerte;
    public Sprite tutofuertePc;
    public Sprite tutofuerteXbox;
    public Image tutosolo;
    public Sprite tutosoloPc;
    public Sprite tutosoloXbox;
    public Image tutoMove;
    public Sprite tutoMovePc;
    public Sprite tutoMoveXbox;
    public Canvas UI;
    public Canvas UI2;
    public GameObject mainMenu;
    public bool firstTime;
    public Transform Tppos;
    public Canvas skipCanvas;
    public bool godmode = true;
    public int puaCounter=0;
    public Canvas pausaMenu;
    public GameObject infopua;
    public bool seeinfopua;
    public bool notSound;
    public RectTransform optionsMenu;
    public Animator pauseMenuPrincial;
    public bool pause = false;
    public int numScore;
    public Canvas lifePlayer;
    public Canvas infoPlayer;
    public EaseColor startFade;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        notSound = false;
        seeinfopua = false;
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
                    tutoMove.sprite = tutoMoveXbox;
                    tutodebil.sprite = tutodebilXbox;
                    tutofuerte.sprite = tutofuerteXbox;
                    tutoblock.sprite = tutoblockXbox;
                    tutosolo.sprite = tutosoloXbox;
                    Debug.Log("Controller " + i + " is connected using: " + temp[i]);
                }
                else
                {
                    //If it is empty, controller i is disconnected
                    //where i indicates the controller number
                    tutoMove.sprite = tutoMovePc;
                    tutodebil.sprite = tutodebilPc;
                    tutofuerte.sprite = tutofuertePc;
                    tutoblock.sprite = tutoblockPc;
                    tutosolo.sprite = tutosoloPc;
                    Debug.Log("Controller: " + i + " is disconnected.");

                }
            }
        }
        score.text = numScore.ToString();
        if (seeinfopua)
        {
            Player.gameObject.GetComponent<PlayerAttack2>().canPause = false;
            lifePlayer.enabled = false;
            infoPlayer.enabled = false;
            notSound = false;
            Time.timeScale= 0;
            BeatEmupManager.instance.infopua.SetActive(true);
            skipCanvas.enabled = true;
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetAxisRaw("Jump")!=0)
            {
                BeatEmupManager.instance.infopua.SetActive(false);
                seeinfopua = false;
                notSound = true;
                Time.timeScale = 1;
                skipCanvas.enabled = false;
                Invoke("UIenabled",0.5f);
                UI.enabled = false;
                UI2.enabled = false;
                Player.gameObject.GetComponent<PlayerAttack2>().canPause = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.F10))
        {
            GodMode();
        }
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.F8))
        {
            Player.transform.position = Tppos.position;
        }
#endif
        if (godmode == true && Player.GetComponent<PlayerAttack2>().canPause)
        {

            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Pause"))
            {
                Pausa();
            }
        }
    }
    public void UIenabled()
    {
        UI.enabled = false;
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

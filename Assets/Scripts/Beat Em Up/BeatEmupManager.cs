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
    public bool godmode = true;
    public RectTransform pausaMenu;
    public Animator pauseMenuPrincial;
    public bool pause = false;
    public int numScore;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Cursor.visible = false;
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
            //pauseMenuPrincial.SetBool("Pausa", true);
            Cursor.visible = false;
            musicGameplay.mute = false;
            //notas.SetActive(false);
            Invoke("TakeoFFMenu", 0.2f);
            pause = true;
        }
        else if (pause)
        {
            openPause.Play();
            //pauseMenuPrincial.SetBool("Pausa", false);
            //notas.SetActive(true);
            musicGameplay.mute = true;
            Cursor.visible = true;
            Invoke("TakeONMenu", 0.2f);
            pause = false; 
        }
    }
    private void TakeoFFMenu()
    {
        pausaMenu.gameObject.SetActive(false);
        music.Stop();
    }
    private void TakeONMenu()
    {
        pausaMenu.gameObject.SetActive(true);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using DG.Tweening;
using UnityEngine.Rendering.PostProcessing;

public class TptoBossBattle : MonoBehaviour
{
    public Transform tppoint;
    public VideoPlayer cinematicaBoss;
    public ShakeCamera camera;
    public PlayerMovementBeat Player;
    public Animator player_Anim;
    public BattleZone BossZone;
    public GameObject reverbZone;
    public GameObject Pospo;
    public Canvas BossLife;
    public Canvas ui;
    public Canvas ui2;
    public bool cinematica=false;
    public AudioSource musicBoss;
    //public GameObject musicGameplay;
    public bool changeMusic=false;
    public VideoClip VideoEng;
    public VideoClip VideoEsp;
    public float counter = 0;
    public float countStart = 0;
    public void Start()
    {
        cinematicaBoss.Prepare();
        //camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ShakeCamera>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementBeat>();
        player_Anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }
    private void Update()
    {
        cinematicaBoss.loopPointReached += onMovieEnded;
        if (LanguageManager.langData.currentLanguage == LangData.Languages.English)
        {
            cinematicaBoss.clip = VideoEng;
        }
        else if (LanguageManager.langData.currentLanguage == LangData.Languages.Spanish)
        {
            cinematicaBoss.clip = VideoEsp;
        }
        if (Input.GetKeyDown(KeyCode.Return) && cinematica || Input.GetAxisRaw("AtaqueDebil") != 0 && cinematica)
        {
            Player.gameObject.GetComponent<PlayerAttack2>().canPause = true;
            cinematicaBoss.gameObject.SetActive(false);
            changeMusic = true;
            cinematica = false;
            BossZone.bossZone = true;
            Player.enabled = true;
            cinematicaBoss.Pause();
            ActivateBossMusic();
            ui.enabled = true;
            ui2.enabled = true;
            reverbZone.SetActive(true);
            Pospo.SetActive(true);
            camera.gameObject.GetComponent<PostProcessLayer>().enabled = true;
        }
            if (changeMusic == true)
        {
            if(BossLife!=null)
            BossLife.enabled = true;
            camera.GetComponent<Camera>().fieldOfView = 47;
        }
        /*if (BeatEmupManager.instance.pause == true)
        {
            //musicBoss.mute = false;
        }
        else if(BeatEmupManager.instance.pause == false)
            musicBoss.mute = true;*/
    }
    /*IEnumerator waitForMovieEnd()
    {

        while (cinematicaBoss.isPlaying) // while the movie is playing
        {
            yield return new WaitForEndOfFrame();
        }
        // after movie is not playing / has stopped.
        onMovieEnded();
    }*/

    void onMovieEnded(VideoPlayer source)
    {
        Player.gameObject.GetComponent<PlayerAttack2>().canPause = true;
        cinematicaBoss.gameObject.SetActive(false);
        changeMusic = true;
        cinematica = false;
        ui.enabled = true;
        ui2.enabled = true;
        camera.lockCamera = false;
        BossZone.bossZone = true;
        Player.enabled = true;
        cinematicaBoss.Pause();
        ActivateBossMusic();
        reverbZone.SetActive(true);
        camera.gameObject.GetComponent<PostProcessLayer>().enabled = true;
    }
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player.gameObject.GetComponent<PlayerAttack2>().canPause = false;
            cinematicaBoss.gameObject.SetActive(true);
            cinematicaBoss.Play();
            cinematica = true;
            Player.running = false;
            ui.enabled = false;
            ui2.enabled = false;
            Player.walk = false;
            player_Anim.SetBool("Walk", false);
            player_Anim.SetBool("Run", false);
            other.gameObject.transform.position = tppoint.position;
            camera.lockCamera = false;
            camera.gameObject.GetComponent<PostProcessLayer>().enabled = false;
            Player.enabled = false;
            Pospo.SetActive(false);
            BeatEmupManager.instance.musicGameplay.Stop();
            //changeMusic = true;
        }
    }
    void ActivateBossMusic()
    {
        //ESTO CONTROLA EL VOLUMEN DE LA MUSICA DEL BOSS
        musicBoss.DOFade(0.3f, 2f);
        musicBoss.Play();
    }
}

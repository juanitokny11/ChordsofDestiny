using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using DG.Tweening;

public class TptoBossBattle : MonoBehaviour
{
    public Transform tppoint;
    public VideoPlayer cinematicaBoss;
    public ShakeCamera camera;
    public PlayerMovementBeat Player;
    public Animator player_Anim;
    public BattleZone BossZone;
    public Canvas BossLife;
    public AudioSource musicBoss;
    //public GameObject musicGameplay;
    public bool changeMusic=false;
    public void Start()
    {
        cinematicaBoss.Prepare();
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ShakeCamera>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementBeat>();
        player_Anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }
    private void Update()
    {
        cinematicaBoss.loopPointReached += onMovieEnded;
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
        cinematicaBoss.gameObject.SetActive(false);
        changeMusic = true;
        BossZone.bossZone = true;
        Player.enabled = true;
        cinematicaBoss.Pause();
        ActivateBossMusic();
    }
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            cinematicaBoss.gameObject.SetActive(true);
            cinematicaBoss.Play();
            Player.running = false;
            Player.walk = false;
            player_Anim.SetBool("Walk", false);
            player_Anim.SetBool("Run", false);
            other.gameObject.transform.position = tppoint.position;
            camera.lockCamera = false;
            Player.enabled = false;
            BeatEmupManager.instance.musicGameplay.Stop();
            //changeMusic = true;
        }
    }
    void ActivateBossMusic()
    {
        //ESTO CONTROLA EL VOLUMEN DE LA MUSICA DEL BOSS
        musicBoss.DOFade(0.7f, 2f);
        musicBoss.Play();
    }
}

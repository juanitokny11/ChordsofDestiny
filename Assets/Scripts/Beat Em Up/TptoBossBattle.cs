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
    public BattleZone BossZone;
    public Canvas BossLife;
    public AudioSource musicBoss;
    //public GameObject musicGameplay;
    public bool changeMusic=false;
    public void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ShakeCamera>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementBeat>();
    }
    private void Update()
    {
        cinematicaBoss.loopPointReached += onMovieEnded;
        if (changeMusic == true)
        {
            //musicGameplay.GetComponent<AudioSource>().enabled = false;
            //ActivateBossMusic();
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
    }
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            cinematicaBoss.gameObject.SetActive(true);
            cinematicaBoss.Play();
            other.gameObject.transform.position = tppoint.position;
            camera.lockCamera = false;
            Player.enabled = false;
            //changeMusic = true;
        }
    }
    void ActivateBossMusic()
    {
        musicBoss.DOFade(0.7f, 2f);
        musicBoss.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VictoryManager : MonoBehaviour
{
    public VideoPlayer Creditos;
    public AudioSource creditosMusic;
    public Canvas victorycanvas;
    public AudioSource victoryMusic;
    void Start()
    {
        Creditos.loopPointReached += EndVideo;
        Creditos.Prepare();
        creditosMusic.Play();
        //transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }
    private void EndVideo(VideoPlayer source)
    {
        Creditos.gameObject.SetActive(false);
        victorycanvas.enabled = true;
        Time.timeScale = 1;
        Cursor.visible = true;
    }
    private void Update()
    {
        if (!creditosMusic.isPlaying)
        {
            creditosMusic.Stop();
            Invoke("PlayVictoryMusic", 0.2f);
        }
    }
    private void PlayVictoryMusic()
    {
        victoryMusic.Play();
    }
}

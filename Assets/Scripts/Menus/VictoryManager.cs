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
        creditosMusic.Stop();
        Invoke("PlayVictoryMusic", 0.2f);
        Creditos.gameObject.SetActive(false);
        victorycanvas.enabled = true;
        Time.timeScale = 1;
        Cursor.visible = true;
    }
    private void PlayVictoryMusic()
    {
        victoryMusic.Play();
    }
}

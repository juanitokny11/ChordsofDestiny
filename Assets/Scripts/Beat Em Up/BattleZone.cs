using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleZone : MonoBehaviour
{
    //private BoxCollider colider;
    public new GameObject camera;
    public GameObject UI;
    public GameObject score;
    public GameObject EnemyUI;
    public HealthScript Player;
    public bool bossZone;
   // public Text[] namesEnemies;
    //public Image[] lifeBars;
    public Image goImage;
    public AudioSource musica;
    public BossIA boss;
    public List<EnemyMovement> enemies;
    public int id;
    public int enemiescounter;
    private void Start()
    {
        //colider = GetComponent<BoxCollider>();
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        enemiescounter = enemies.Count;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthScript>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        { 
            UI.SetActive(true);
            score.SetActive(true);
            GetComponent<BoxCollider>().enabled = false;
            if (!bossZone)
            {
                enemies[1].gameObject.SetActive(true);
                enemies[2].gameObject.SetActive(true);
                camera.GetComponent<ShakeCamera>().lockCamera = true;
            }
            if (Player.health < 100)
            {
                UI.GetComponent<Image>().fillAmount = Player.health + 10/100;
                Player.health = Player.health + 10;
            }
            if (BeatEmupManager.instance.pause == true)
            {
                musica.Play();
            }
        }
    }
    void Update()
    {
        if (BeatEmupManager.instance.pause == true)
            musica.mute=false;
        if (BeatEmupManager.instance.pause == false)
            musica.mute=true;
        if (!bossZone)
        {
            if (enemiescounter <= 0)
            {
                EnemyUI.SetActive(false);
                Invoke("UnlockCamera", 1f);
                musica.Pause();
                SetGo();
                Invoke("StopGo", 3f);
                Invoke("Destroy", 4f);
            }
        }
        else if (bossZone)
        {
            if (enemiescounter <= 1)
            {
                EnemyUI.SetActive(false);
            }
        }
    }
    void UnlockCamera()
    {
        camera.GetComponent<ShakeCamera>().lockCamera = false;
        // camera.GetComponent<ShakeCamera>().enemiesdied = true;
    }
    void SetGo()
    {
        InvokeRepeating("Blink", 0.1f, 0.1f);
    }
    void Blink()
    {
        if (Time.fixedTime % .5 < .2)
        {
            goImage.enabled = false;
        }
        else
        {
            goImage.enabled = true;
        }
    }
    void StopGo()
    {
        CancelInvoke("Blink");
        UI.SetActive(false);
        score.SetActive(false);
        goImage.enabled = false;
    }
    private void Destroy()
    {
        Destroy(gameObject);
    }
}

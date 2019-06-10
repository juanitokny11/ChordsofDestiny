using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BattleZone : MonoBehaviour
{
    //private BoxCollider colider;
    public new ShakeCamera camera;
    public GameObject UI;
    public GameObject UI2;
    public GameObject score;
    public GameObject EnemyUI;
    public HealthScript Player;
    public BoxCollider enemyBlocker;
    public AudioSource goSound;
    public bool bossZone;
   // public Text[] namesEnemies;
    //public Image[] lifeBars;
    public Image goImage;
    public AudioSource musica;
    public BossIA boss;
    public GameObject pua;
    public Transform puaPos;
    public List<EnemyMovement> enemies;
    public Image tutoFuerte;
    public Image tutoDebil;
    public Image imageblocktuto;
    public PlayerAttackList attackList;
    public bool tutoBlock;
    public bool tutoAtack;
    public bool tutoAtack2;
    public int id;
    public int enemiescounter;
    private void Start()
    {
        tutoAtack = false;
        //colider = GetComponent<BoxCollider>();
        //camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ShakeCamera>();
        if (!bossZone)
            enemiescounter = enemies.Count;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthScript>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (id == 0 )
            {
                tutoAtack = true;
            }
            if (id == 1)
            {
                tutoBlock= true;
            }
            UI.GetComponent<Canvas>().enabled = true;
            UI2.GetComponent<Canvas>().enabled = true;
            enemyBlocker.enabled = false;
            score.SetActive(true);
            GetComponent<BoxCollider>().enabled = false;
            if (!bossZone)
            {
                if(enemies.Count>=2)
                    enemies[1].gameObject.SetActive(true);
                if (enemies.Count >= 3)
                    enemies[2].gameObject.SetActive(true);
                camera.GetComponent<ShakeCamera>().lockCamera = true;
            }
        }
    }
    void Update()
    {
        if (id == 0 && tutoAtack)
        {
            if (!tutoAtack2)
            {
                tutoDebil.gameObject.SetActive(true);
                Time.timeScale = 0;
                if (Input.GetAxisRaw("AtaqueDebil") != 0)
                {
                    Time.timeScale = 1;
                    tutoAtack2 = true;
                }
            }
            else if (tutoAtack2)
            {
                tutoDebil.gameObject.SetActive(false);
                tutoFuerte.gameObject.SetActive(true);
                Time.timeScale = 0;
                if (Input.GetAxisRaw("AtaqueFuerte") != 0)
                {
                    tutoFuerte.gameObject.SetActive(false);
                    Time.timeScale = 1;
                    tutoAtack2 = false;
                    tutoAtack = false;
                    attackList.RemoveAllList();
                }
            }
        }
            if (id == 1 && tutoBlock)
            {
                imageblocktuto.gameObject.SetActive(true);
                Time.timeScale = 0;
                if (Input.GetAxisRaw("Evadir") != 0)
                {
                    Time.timeScale = 1;
                    imageblocktuto.gameObject.SetActive(false);
                    tutoBlock = false;
                }
            }
        if (BeatEmupManager.instance.pause == true)
            musica.mute=false;
        if (BeatEmupManager.instance.pause == false)
            musica.mute=true;
        if (!bossZone)
        {
            if (enemiescounter <= 0)
            {
                EnemyUI.SetActive(false);
                UI.GetComponent<Canvas>().enabled = false;
                Invoke("UnlockCamera", 1f);
                //musica.DOFade(0.4f, 5f);
                //musica.Pause();
                SetGo();
                Invoke("StopGo", 2.5f);
                Invoke("Destroy", 2.8f);
                //onTime = true;
            }
        }
        else if (bossZone)
        {
            if (enemiescounter <= 1)
            {
                EnemyUI.SetActive(false);
                enemiescounter = enemies.Count+1;
            }
        }
    }
    void UnlockCamera()
    {
        //camera.ToUnlock();
        //Invoke("UnlockCam",2f);
        camera.enemiesdied = true;
        camera.lockCamera = false;
        
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
            goSound.Play();
        }
    }
    void StopGo()
    {
        CancelInvoke("Blink");
        UI2.GetComponent<Canvas>().enabled = false;
        score.SetActive(false);
        goImage.enabled = false;
    }
    private void Destroy()
    {
        if (id == 5 || id == 7)
        {
            Instantiate(pua, enemies[0].transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}

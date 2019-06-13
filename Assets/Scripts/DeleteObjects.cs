using UnityEngine;
using System.Collections;

public class DeleteObjects: MonoBehaviour {

    public int vida = 2;
    private Vector3 inipos;
    public bool puaInstance;
    public GameObject pua;
    public ParticleSystem p1;
    public ParticleSystem p2;
    public ParticleSystem p3;
    public Canvas scoreUI;
    Vector3 puaSpawn;
    public bool firsttime;
    public HealthScript playerHealth;
    public Canvas UI;
    public GameObject score;
    public AudioSource fuego;
    public BeatEmupManager gameManager;
    public ParticleCulling particleCulling;

    private void Start()
    {
        firsttime = true;
        fuego = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<BeatEmupManager>();
        particleCulling = FindObjectOfType<ParticleCulling>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthScript>();
        inipos = transform.position;
        puaSpawn = inipos;
    }
    private void Update()
    {
        if (vida <= 0)
        {
            Invoke("Explode", Time.deltaTime * 15f);
            scoreUI.enabled = true;
            score.SetActive(true);
            if (p1!=null)
                 particleCulling.RemoveParticle(p1.GetComponent<ParticlesBevahavour>());
            if (p2!= null)
                particleCulling.RemoveParticle(p2.GetComponent<ParticlesBevahavour>());
            if (p3 != null)
                particleCulling.RemoveParticle(p3.GetComponent<ParticlesBevahavour>());
            Destroy(p1);
            Destroy(p2);
            Destroy(p3);
        }
        else
        {
            if(!BeatEmupManager.instance.pause)
            fuego.Play();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ligero" || other.tag =="pesado")
        {
            vida--;
        }
    }
    void Blink()
    {
        if (Time.fixedTime % .4 < .2)
        {
           gameObject.transform.parent.gameObject.SetActive(false);
        }
        else
        {
           gameObject.transform.parent.gameObject.SetActive(true);
        }
    }
    public void Explode()
    {
        InvokeRepeating("Blink", 0.1f, 0.1f); 
        if (playerHealth.health <= 100)
            Invoke("LifeOn", 2f);
        Invoke("StopBlink", 2f);
    }
    public void StopBlink()
    {
        CancelInvoke("Blink");
        score.SetActive(false);
        if(firsttime)
        {
            gameManager.numScore += 5;
            if (puaInstance)
            {
                Instantiate(pua, puaSpawn, Quaternion.identity);
            }
            firsttime = false;
        }
        if (playerHealth.health <= 100)
            Invoke("LifeOff", 2f);
        scoreUI.enabled = false;
        Invoke("Destroy", 0.5f);
    }
    public void Destroy()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
    public void LifeOn()
    {
        UI.enabled = true;
    }
    public void LifeOff()
    {
        UI.enabled = false;
    }
}

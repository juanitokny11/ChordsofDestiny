using UnityEngine;
using System.Collections;

public class DeleteObjects: MonoBehaviour {

    public int vida = 2;
    private Vector3 inipos;
    public bool puaInstance;
    public GameObject pua;
    Vector3 puaSpawn;
    public HealthScript playerHealth;
    public GameObject UI;
    public GameObject score;
    public BeatEmupManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<BeatEmupManager>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthScript>();
        inipos = transform.position;
        puaSpawn = inipos;
    }
    private void Update()
    {
        if (vida <= 0)
        {
            Invoke("Explode", Time.deltaTime * 18f);
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
        if (Time.fixedTime % .5 < .2)
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
        gameManager.numScore += 5;
        score.SetActive(true);
        if (playerHealth.health <= 100)
            Invoke("LifeOn", 2f);
        Invoke("StopBlink", 3f);
    }
    public void StopBlink()
    {
        CancelInvoke("Blink");
        score.SetActive(false);
        if (puaInstance)
            Instantiate(pua, puaSpawn, Quaternion.identity);
        if (playerHealth.health <= 100)
            Invoke("LifeOff", 2f);
        Invoke("Destroy", 0.1f);
    }
    public void Destroy()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
    public void LifeOn()
    {
        UI.SetActive(true);
    }
    public void LifeOff()
    {
        UI.SetActive(false);
    }
}

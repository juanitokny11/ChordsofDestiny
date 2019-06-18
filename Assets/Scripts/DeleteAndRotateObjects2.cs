using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DeleteAndRotateObjects2: MonoBehaviour {

    public float CoinRotateSpeed = 5;
    public ParticleSystem heal;

    public AudioSource vida;
    public HealthScript player;
    public Image UI;
    public bool sound;
    public GameObject LifeBar;
    public int random;
    private void Awake()
    {
        sound = false;
        UI = GameObject.FindGameObjectWithTag("PlayerHealth").GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthScript>();
        heal = GameObject.Find("Heal").GetComponent<ParticleSystem>();
        vida = GameObject.FindGameObjectWithTag("vida").GetComponent<AudioSource>();
    }
    void Start()
    {
        //transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Explode();
            sound = true; ;
        }
    }
    void Update()
    {
        random = Random.Range(10,30);
        if (player.GetComponent<HealthScript>().health <100 && Time.timeScale == 1 && sound)
        {
            player.health = player.health + random;
            UI.fillAmount = player.health / 100;
            heal.Play();
            sound = false;
            vida.PlayDelayed(0.4f);
            //Player.health = Player.health + 10;
            //LifeBar.SetActive(false);
        }
    }
    void FixedUpdate()
    {
        transform.Rotate(Vector3.down * CoinRotateSpeed);
    }

    void Explode() {
        if (BeatEmupManager.instance.puaCounter < 1)
        {
            BeatEmupManager.instance.seeinfopua = true;
        }
        BeatEmupManager.instance.puaCounter++;
        Invoke("DestroyGameobject", 0.2f);
     
	}
    void DestroyGameobject()
    {
        Destroy(gameObject);
    }
}

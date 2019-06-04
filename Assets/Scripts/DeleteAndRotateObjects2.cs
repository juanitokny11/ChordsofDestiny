using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DeleteAndRotateObjects2: MonoBehaviour {

    public float CoinRotateSpeed = 5;
    public ParticleSystem heal;

    public AudioSource vida;
    public HealthScript player;
    public Image UI;
    public GameObject LifeBar;
    public int random;
    private void Awake()
    {
        UI = GameObject.FindGameObjectWithTag("PlayerHealth").GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthScript>();
        heal = GameObject.Find("Heal").GetComponent<ParticleSystem>();
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
        }
    }
    void Update()
    {
        random = Random.Range(10,30);
    }
    void FixedUpdate()
    {
        transform.Rotate(Vector3.down * CoinRotateSpeed);
    }

    void Explode() {
        if (player.GetComponent<HealthScript>().health < 100)
        {
            player.health = player.health + random;
            UI.fillAmount = player.health / 100;
            heal.Play();
            vida.PlayDelayed(0.4f);
            //Player.health = Player.health + 10;
            //LifeBar.SetActive(false);
        }
        
        Invoke("DestroyGameobject", 0.2f);
     
	}
    void DestroyGameobject()
    {
        Destroy(gameObject);
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DeleteAndRotateObjects2: MonoBehaviour {

    public float CoinRotateSpeed = 5;
    public HealthScript player;
    private float CoinSpeed = 20.0f;
    private Transform playerTransform;
    public Image UI;
    public GameObject score;
    public GameObject LifeBar;
    public int random;
    //public bool is_Key = false;
    //public ChangeCamera changeCamera;

    void Start()
    {
        //changeCamera = GameObject.FindObjectOfType<ChangeCamera>();
        LifeBar = GameObject.FindGameObjectWithTag("UI");
        UI = GameObject.FindGameObjectWithTag("PlayerHealth").GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthScript>();
        playerTransform = player.GetComponent<Transform>();
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
        CoinsPowerup();
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

            //Player.health = Player.health + 10;
            //LifeBar.SetActive(false);
        }
        Invoke("DestroyGameobject", 0.2f);
     
	}
    public void CoinsPowerup()
    {
       /* float distance = Vector3.Distance(playerTransform.position, transform.position);
        //  float distance = playerTransform.position.y - transform.position.y;

        float maxDistance = 5.0f;
        // float maxDistance = 0.030f;
        if (distance < maxDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, CoinSpeed * Time.deltaTime);
        }*/
    }
    void DestroyGameobject()
    {
        Destroy(gameObject);
    }
}

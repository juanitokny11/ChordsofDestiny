using UnityEngine;
using System.Collections;

public class DeleteAndRotateObjects:MonoBehaviour {

    public float CoinRotateSpeed = 5;
    private GameObject player;
    private float CoinSpeed = 20.0f;
    private Transform playerTransform;
    public bool is_Key = false;
    public ChangeCamera changeCamera;

    void Start()
    {
        changeCamera = GameObject.FindObjectOfType<ChangeCamera>();
        player = GameObject.FindGameObjectWithTag("Player");
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
    }
    void FixedUpdate()
    {
        transform.Rotate(Vector3.down * CoinRotateSpeed);
    }

    void Explode() {


        if (is_Key)
        {
            changeCamera.bosscam.enabled = true;
            changeCamera.gameplaycam.enabled = false;
        }

        Destroy(gameObject);
	}
    public void CoinsPowerup()
    {
        /*float distance = Vector3.Distance(playerTransform.position, transform.position);
        //  float distance = playerTransform.position.y - transform.position.y;

        float maxDistance = 7.0f;
        // float maxDistance = 0.030f;
        if (distance < maxDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, CoinSpeed * Time.deltaTime);
        }*/
    }
}

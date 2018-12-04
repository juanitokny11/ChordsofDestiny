using UnityEngine;
using System.Collections;

public class DeleteAndRotateObjects2: MonoBehaviour {

    public float CoinRotateSpeed = 5;
    private GameObject player;
    private float CoinSpeed = 20.0f;
    private Transform playerTransform;

    void Start()
    {

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
        transform.Rotate(Vector3.forward * CoinRotateSpeed);
    }

    void Explode() {
		
		Destroy(gameObject);
	}
    public void CoinsPowerup()
    {
        float distance = Vector3.Distance(playerTransform.position, transform.position);
        //  float distance = playerTransform.position.y - transform.position.y;

        float maxDistance = 10.0f;
        // float maxDistance = 0.030f;
        if (distance < maxDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, CoinSpeed * Time.deltaTime);
        }
    }
}

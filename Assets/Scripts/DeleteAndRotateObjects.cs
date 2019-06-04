using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class DeleteAndRotateObjects:MonoBehaviour {

    public float CoinRotateSpeed = 5;
    private GameObject player;
    private float CoinSpeed = 20.0f;
    private Transform playerTransform;
    public bool is_Key = false;
    public ChangeCamera changeCamera;
    public GameObject Puerta;
    public VideoPlayer Creditos;

    void Start()
    {
        Creditos.Prepare();
        changeCamera = GameObject.FindObjectOfType<ChangeCamera>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Transform>();
        Puerta = GameObject.FindGameObjectWithTag("Salida");
        //transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }
    IEnumerator waitForMovieEnd()
    {

        while (Creditos.isPlaying) // while the movie is playing
        {

            yield return new WaitForEndOfFrame();
        }
        // after movie is not playing / has stopped.
        onMovieEnded();
    }

    void onMovieEnded()
    {
        Creditos.Pause();
        Time.timeScale = 1;
        Cursor.visible = false;
        SceneManager.LoadScene("Victory");
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
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
            Puerta.GetComponent<Animator>().SetTrigger("Abrir");
            Invoke("EndGame",2.5f);
        }
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
    public void EndGame()
    {
        Destroy(gameObject);
        Creditos.gameObject.SetActive(true);
        Creditos.Play();
        //Cursor.visible = true;
        //SceneManager.LoadScene("Victory");
    }
}

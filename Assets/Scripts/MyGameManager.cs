using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MyGameManager : MonoBehaviour
{
    public SoundPlayer sound;
    private GameObject player;
    public GameObject solocarge;
    public GameObject arma;
    public AudioClip[] atacksdebil;
    public float jumpInput;
    public GameObject soloefect;
    public Text tempo;
    public GameObject vinillo;
    public Transform shooterpos;
    public AudioSource openPause;
    public AudioSource closePause;
    private MouseCursor mouseCursor;
    public GameObject cam;
    public int money;
    public Text moneyText;
    public Animator pers;
    private bool debil;
    private bool fuerte;
    public float Damage = 10f;
    public float cargasolo = 15f;
    public Animator soloefectanim;
    public float clavecarga = 25f;
    public bool look = true;
    public bool pause = true;
    public bool godmode = true;
    public bool notacogida;
    public float counternotas;
    public GameObject notas;
    public RectTransform pausaMenu;
    public Animator pauseMenuPrincial;
    public Vector2 inputAxis = Vector2.zero;
    public Image soloBar;
    public GameObject solocollider;
    public float cursolo;
    public float Maxsolo = 100;
    public Image HealthBar;
    public float curHealth;
    public int debilSOUND;
    float verticalVel;
    public float MaxHealth = 100;
    private static MyGameManager instance;
    public SoundPlayer audios;
    public static MyGameManager getInstance()
    {
        return instance;
    }
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        player = GameObject.FindGameObjectWithTag("Player");
        pause = true;
        godmode = true;
        cursolo = 0;
        soloBar.fillAmount = cursolo / Maxsolo;
        curHealth = MaxHealth;
        HealthBar.fillAmount = curHealth / MaxHealth;
        Cursor.visible = false;
    }
    void Update()
    {
        debilSOUND = Random.Range(0, 2);
        if (Input.GetKeyDown(KeyCode.F10))
        {
            GodMode();
        }
        if (godmode == true)
        {
            if (soloBar.fillAmount >= 1)
                solocarge.SetActive(true);
            else
                solocarge.SetActive(false);
            Time.timeScale = 1;
            if (curHealth <= 0)
            {
                Invoke("Dead", 2f);
            }
            if (Input.GetKeyDown(KeyCode.G) || Input.GetAxisRaw("Evadir") != 0 && Input.GetAxisRaw("Disparar") == 0)
            {
                Evadir();
            }
            if (Input.GetKeyDown(KeyCode.F) || Input.GetAxisRaw("Disparar") != 0 && Input.GetAxisRaw("Evadir") == 0)
            {
                Disparar();
            }
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetAxisRaw("Pause") != 0)
            {
                Pausa();
            }
            if (notacogida == true)
            {
                notas.SetActive(true);
                counternotas = counternotas + Time.deltaTime;
                if (counternotas >= 4)
                {
                    counternotas = 0;
                    notacogida = false;
                    notas.SetActive(false);
                }
            }
            if (pause == true)
            {
                if (Input.GetMouseButtonDown(0) || Input.GetAxisRaw("AtaqueDebil") != 0)
                {
                    pers.SetBool("Debil", true);
                    Invoke("DebilFalse", 1f);
                    arma.transform.tag = "ligero";
                    Invoke("ResetTag", 1);
                    AudioSource sonido =player.AddComponent<AudioSource>();
                    sonido.PlayOneShot(atacksdebil[debilSOUND]);

                }
                if (Input.GetMouseButtonDown(1) || Input.GetAxisRaw("AtaqueFuerte") != 0)
                {
                    pers.SetBool("Fuerte", true);
                    Invoke("FuerteFalse", 1f);
                    arma.transform.tag = "pesado";
                    Invoke("ResetTag", 1);
                    audios.Play(3, 1);
                }
                if (Input.GetMouseButtonDown(2) || Input.GetAxisRaw("Evadir") != 0 && Input.GetAxisRaw("Disparar") != 0)
                {
                    if (cursolo >= Maxsolo)
                    {
                        cursolo = 0;
                        soloefect.SetActive(true);
                        soloefectanim.Play("soloanim", -1, 0);
                        Invoke("ResetAnim", 1f);
                        soloBar.fillAmount = cursolo / Maxsolo;
                        Invoke("DesActivarColisiones", 0.1f);
                    }
                    Invoke("ActivarColisiones", 2f);
                }
            }
        }
        else
        {
            Time.timeScale = 0;
            inputAxis.x = Input.GetAxis("Horizontal");
            inputAxis.y = Input.GetAxis("Vertical");
            DesActivarColisiones();
            Vector3 move = new Vector3(inputAxis.x,0,inputAxis.x);
            var forward = pers.transform.forward;
            var right = pers.transform.right;
            Vector3 desiredMoveDirection = forward * inputAxis.x + right * inputAxis.x;
            player.GetComponent<CharacterController>().SimpleMove(desiredMoveDirection);
            if (Input.GetButton("Jump"))
               verticalVel += 0.5f;
           
            Vector3 moveVector = new Vector3(0, verticalVel, 0);

            player.GetComponent<CharacterController>().Move(moveVector);
            //cam.transform.Translate(inputAxis.x, 0, inputAxis.y);
            //cam.transform.Rotate(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
        }
    }
   public void DebilFalse()
    {
        pers.SetBool("Debil", false);
    }
    public void FuerteFalse()
    {
        pers.SetBool("Fuerte", false);
    }
    public void AddMoney(int value)
    {
        money += value;
        moneyText.text = money.ToString();
    }
    void ResetAnim()
    {
        soloefectanim.SetTrigger("Reset");
    }
    void ResetTag()
    {
        arma.transform.tag = "arma";
    }
    public void Daño(float Damage)
    {
        curHealth -= Damage;
        HealthBar.fillAmount = curHealth / MaxHealth;
    }
    public void Carga()
    {
        cursolo += cargasolo;
        soloBar.fillAmount = cursolo / Maxsolo;
    }
    public void CargaClave()
    {
        cursolo += clavecarga;
        soloBar.fillAmount = cursolo / Maxsolo;
    }
    public void Pausa()
    {
        if (!pause)
        {
            closePause.Play();
            pauseMenuPrincial.SetBool("Pausa", true);
            //Time.timeScale = 0;
            Cursor.visible = false;
            //music.mute = false; 
            notas.SetActive(false);
            sound.enabled = false;
            Invoke("TakeoFFMenu", 0.2f);
            pause = true;
        }
        else if (pause)
        {
            openPause.Play();
            pauseMenuPrincial.SetBool("Pausa", false);
            //Time.timeScale = 1;
            notas.SetActive(true);
            Cursor.visible = true;
            //music.mute = true;
            sound.enabled = true;
            Invoke("TakeONMenu", 0.2f);
            pause = false;
        }
    }
    private void TakeoFFMenu() {
        pausaMenu.gameObject.SetActive(false);
    }
    private void TakeONMenu()
    {
        pausaMenu.gameObject.SetActive(true);
    }
    public void GodMode()
    {
        if (!godmode)
        {
            //cam.SetActive(false);
            godmode = true;
            //Cursor.visible = false;
        }
        else if (godmode)
        {
            godmode = false;
            //Cursor.visible = true;
        }
    }
    public void Evadir()
    {
        Debug.Log("esquivo");
        DesActivarColisiones();
        Invoke("ActivarColisiones",0.2f);
    }
    public void Disparar()
    {
        Debug.Log("disparo");
        Instantiate(vinillo, shooterpos.position, vinillo.transform.rotation, null);
    }
    public void Dead()
    {
        SceneManager.LoadScene("GameOver");
        MyGameSettings.getInstance().logoPlayed = true;
        Cursor.visible = true;
    }
    private void ActivarColisiones()
    {
        player.GetComponent<ColisionsPlayer>().enabled = true;
        soloefect.SetActive(false);
        solocollider.SetActive(false);

    }
    private void DesActivarColisiones()
    {
        player.GetComponent<ColisionsPlayer>().enabled = false;
        solocollider.SetActive(true);
    }
    public void OnTempo()
    {
        tempo.text = "Good";
        tempo.color = Color.green;
        Invoke("TextOff", 0.5f);
    }
    public void NoTempo()
    {
        tempo.text = "Wrong";
        tempo.color = Color.red;
        Invoke("TextOff", 0.5f);
    }
    public void TextOff()
    {
        tempo.text = "";
    }

   
}




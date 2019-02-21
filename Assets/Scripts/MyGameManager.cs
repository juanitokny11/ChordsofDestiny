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
    //private LookRotation lookRotation;
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
        //mouseCursor = new MouseCursor();
        //mouseCursor.HideCursor();
        //Cursor.visible = false;
        
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
                    //pers.Play("atque_debil", -1, 0);
                    arma.transform.tag = "ligero";
                    Invoke("ResetTag", 1);
                    AudioSource sonido =player.AddComponent<AudioSource>();
                    sonido.PlayOneShot(atacksdebil[debilSOUND]);
                    Invoke("ResetAttack", 1);
                }
                if (Input.GetMouseButtonDown(1) || Input.GetAxisRaw("AtaqueFuerte") != 0)
                {
                    //pers.Play("ataque_fuerte", -1, 0);
                    arma.transform.tag = "pesado";
                    Invoke("ResetTag", 1);
                    //audios.Play(1, 1);
                    Invoke("ResetAttack", 2);
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
            cam.transform.Translate(inputAxis.x, 0, inputAxis.y);
            cam.transform.Rotate(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
        }
    }
    //Cursor del ratón
    // if (Input.GetMouseButtonDown(0)) mouseCursor.HideCursor();
    // else if (Input.GetKeyDown(KeyCode.Escape)) mouseCursor.ShowCursor();

    public void AddMoney(int value)
    {
        money += value;
        moneyText.text = money.ToString();
    }
    void ResetAttack()
    {
        pers.SetTrigger("Reset");
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
            Time.timeScale = 0;
            Debug.Log(Time.timeScale);
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
            Time.timeScale = 1;
            Debug.Log(Time.timeScale);
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
            DesActivarColisiones();
            //Cursor.visible = false;

        }
        else if (godmode)
        {
            //cam.SetActive(true);
            godmode = false;
            //Cursor.visible = true;
        }
    }
    public void Evadir()
    {
        Debug.Log("esquivo");
        DesActivarColisiones();
        //Invoke("ActivarColisiones",0.2f);
    }
    public void Disparar()
    {
        Debug.Log("disparo");
        Instantiate(vinillo, shooterpos.position, vinillo.transform.rotation, null);
    }
    public void Dead()
    {
        SceneManager.LoadScene("GameOver");
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




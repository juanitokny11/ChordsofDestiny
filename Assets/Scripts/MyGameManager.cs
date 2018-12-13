using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MyGameManager : MonoBehaviour
{

   public SoundPlayer sound;
    private GameObject player;
    public CamaraCollision col;
    public GameObject arma;
    public float jumpInput;
    public CamaraCollision cama;
    public GameObject soloefect;
  //private LookRotation lookRotation;
    private MouseCursor mouseCursor;

    public GameObject cam;
    public int money;
    public Text moneyText;
    public Animator pers;
    private bool debil;
    private bool fuerte;
    public float Damage=10f;
    public float cargasolo=15f;
    public Animator soloefectanim;

    public float clavecarga=50f;
    public bool look=true;
    public bool pause=true;
    public bool godmode=true;
    public bool notacogida;
    public float counternotas;
    public GameObject notas;
    public GameObject pausaMenu;
    public Vector2 inputAxis = Vector2.zero;
    public Image soloBar;
    public GameObject solocollider;
   public float cursolo;
   public float Maxsolo = 100;
   public Image HealthBar;
   public float curHealth;
   public float MaxHealth = 100;
    private static  MyGameManager instance;
    public SoundPlayer audios;
    public static  MyGameManager getInstance()
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
        //lookRotation = playerController.GetComponent<LookRotation>();
        pause=true;
        godmode=true;
        cursolo=0;
        soloBar.fillAmount=cursolo/Maxsolo;
        curHealth=MaxHealth;
        HealthBar.fillAmount=curHealth/MaxHealth;
        //mouseCursor = new MouseCursor();
        //mouseCursor.HideCursor();
        //Cursor.visible = false;
    }

    void Update()
    { 
         if(Input.GetKeyDown(KeyCode.F10)){
          GodMode();
        }
        if (godmode==true){
            if (curHealth<=0){
                Invoke ("Dead",2f);
            }
            if(Input.GetKeyDown(KeyCode.G)){
                 Evadir();
            }
            if(Input.GetKeyDown(KeyCode.F)){
                Disparar();
            }
            if(notacogida==true){
                notas.SetActive(true);
                counternotas= counternotas +Time.deltaTime;
                if(counternotas>=4){
                    counternotas=0;
                    notacogida=false;
                    notas.SetActive(false);
                }
            }
           
        
            if (pause==true){
                if (Input.GetMouseButtonDown(0))
                {
                    pers.Play("atque_debil", -1, 0);
                    arma.transform.tag = "ligero";
                    Invoke("ResetTag", 1);
                    audios.Play(0, 1);
                    //debil = true;
                    Invoke("ResetAttack", 1);
                }
                if (Input.GetMouseButtonDown(1))
                {
                    pers.Play("ataque_fuerte", -1, 0);
                    arma.transform.tag = "pesado";
                    Invoke("ResetTag", 1);
                    audios.Play(1, 1);
                    Invoke("ResetAttack", 2);
                }
                if (Input.GetMouseButtonDown(2))
                { 
                     if(cursolo>=Maxsolo){
                        cursolo=0;
                        soloefect.SetActive(true);
                        soloefectanim.Play("soloanim", -1, 0);
                        Invoke("ResetAnim", 1f);
                        soloBar.fillAmount= cursolo/Maxsolo;
                        Invoke("DesActivarColisiones", 0.1f);
                   
                     }
                 Invoke("ActivarColisiones", 2f);
                }
            }
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                 Pausa();
            }
        }else {
            inputAxis.x = Input.GetAxis("Horizontal");
            inputAxis.y = Input.GetAxis("Vertical");
            cam.transform.Translate(inputAxis.x,0, inputAxis.y);
            cam.transform.Rotate(Input.GetAxis("Mouse Y"),Input.GetAxis("Mouse X"),0);
        }
         if (Input.GetKeyDown(KeyCode.C)){
           LookEnemy();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            cama.numenemies++;
        }

    }
   
    //Cursor del ratón
    // if (Input.GetMouseButtonDown(0)) mouseCursor.HideCursor();
    // else if (Input.GetKeyDown(KeyCode.Escape)) mouseCursor.ShowCursor();

 public void AddMoney(int value){
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
   curHealth -=Damage;
   HealthBar.fillAmount= curHealth/MaxHealth;
}
 public void Carga()
    {
        cursolo +=cargasolo;
        soloBar.fillAmount= cursolo/Maxsolo;
    }
public void CargaClave()
    {
        cursolo +=clavecarga;
        soloBar.fillAmount= cursolo/Maxsolo;
    }

public void Pausa()
    {
    if (!pause)
        {
        pausaMenu.SetActive(false);
        pause = true;
        Time.timeScale = 1;
       Cursor.visible = false;
        AudioListener.pause = false;
        //music.mute = false;
        notas.SetActive(false);
        sound.enabled = false;
        }
        else if (pause)
        {
            Time.timeScale = 0;
            pausaMenu.SetActive(true);
            AudioListener.pause = true;
            notas.SetActive(true);
            pause = false;
            Cursor.visible = true;
            //music.mute = true;
            sound.enabled = true;
        }
   
    }
    public void LookEnemy()
    {
    if (!look)
        {
        look = true;
        col.lookenemy=true;
        }
        else if (look)
        {
            look = false;
            col.lookenemy=false;
        }
   
    }
public void GodMode()
    {
        if (!godmode)
        {
        cam.SetActive(false);
        godmode = true;
       Cursor.visible = false;
      
        }
        else if (godmode)
        {
        cam.SetActive(true);
        godmode = false;
        Cursor.visible = true;
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
}




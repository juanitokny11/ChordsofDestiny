using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MyGameManager : MonoBehaviour
{

   public SoundPlayer sound;
    private PlayerMovementPrueba playerController;
    public GameObject arma;
    public float jumpInput;
    private LookRotation lookRotation;
    private MouseCursor mouseCursor;
    public int money;
    public Text moneyText;
    public Animator pers;
    private bool debil;
    private bool fuerte;
    public float Damage=10f;
    public float cargasolo=20f;
    public bool pause=true;
    public bool notacogida;
    public float counternotas;
    public GameObject notas;
    public GameObject pausaMenu;
    public Vector2 inputAxis = Vector2.zero;
    public Image soloBar;
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
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementPrueba>();
        //lookRotation = playerController.GetComponent<LookRotation>();
        pause=true;
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
        
        if (curHealth<=0){
             Invoke ("Dead",2f);
        }
        
        if(notacogida==true){
            notas.SetActive(true);
        counternotas= counternotas +Time.deltaTime;
        if(counternotas>=10){
            counternotas=0;
            notacogida=false;
            notas.SetActive(false);
        }
        }
         inputAxis.x = Input.GetAxis("Horizontal");
        inputAxis.y = Input.GetAxis("Vertical");
        playerController.SetAxis(inputAxis);
        //El salto del player

        //if (Input.GetButton("Jump")) playerController.StartJump();
        if (pause==true){
        if (Input.GetMouseButtonDown(0))
        {
            pers.Play("atque_debil", -1, 0);
            arma.transform.tag = "ligero";
            Invoke("ResetTag", 1);
            audios.Play(0, 1);
            //debil = true;
            //Invoke("ResetAttack", 1);
        }
        if (Input.GetMouseButtonDown(1))
        {
          pers.CrossFade("ataque_fuerte", 0.1f, 0, 0);
           arma.transform.tag = "pesado";
            Invoke("ResetTag", 1);
            audios.Play(1, 1);
            //Invoke("ResetAttack", 2);
        }
         if (Input.GetMouseButtonDown(2))
        { 
        if(cursolo>=Maxsolo){
            cursolo=0;
            soloBar.fillAmount= cursolo/Maxsolo;
            }
        }
        }
        if(Input.GetKeyDown(KeyCode.P))
    {
        Pausa();
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
    void ResetTag()
    {
        arma.transform.tag = "arma";
    } 
public void Daño()
{
   curHealth -=Damage;
   HealthBar.fillAmount= curHealth/MaxHealth;
}
public void Carga()
{
   cursolo +=cargasolo;
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
        sound.enabled = false;
    }
    else if (pause)
    {
        Time.timeScale = 0;
        pausaMenu.SetActive(true);
        AudioListener.pause = true;
        pause = false;
        Cursor.visible = true;
        //music.mute = true;
        sound.enabled = true;
    }
   
}
 public void Dead(){
        SceneManager.LoadScene("GameOver"); 
    }
}




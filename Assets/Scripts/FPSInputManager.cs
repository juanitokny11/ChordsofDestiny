﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FPSInputManager : MonoBehaviour
{

   public SoundPlayer sound;
    private PlayerMovement playerController;
    public GameObject arma;
    private float sensitivity = 3.0f;
    private LookRotation lookRotation;
    private MouseCursor mouseCursor;
    public int money;
    public Text moneyText;
    public Animator pers;
    private bool debil;
    private bool fuerte;
    public bool pause=true;
    public GameObject pausaMenu;

   // public Scrollbar HealthBar;
   // public float Health = 100;
    private static FPSInputManager instance;
    public SoundPlayer audios;

    public static FPSInputManager getInstance()
    {
        return instance;
    }
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        lookRotation = playerController.GetComponent<LookRotation>();
        pause=true;
        //laser = playerController.GetComponent<Laser> ();
        //ballShoot = playerController.GetComponent<BallShoot>();

        //mouseCursor = new MouseCursor();
        //mouseCursor.HideCursor();
        //Cursor.visible = false;
       /* armas = 1;
        ammo = 30;
        totalammo = 180;
        rocket = 3;
        totalrocket = 9;
        min = 5;
        seg = 0;*/
    }

    void Update()
    { /*if (pause == true)
        {
            Spawners();
            Addtime();
        }
        ammotext.text = ammo.ToString();
        rocketstext.text = rocket.ToString();
            totalammotext.text = totalammo.ToString();
        totalrocketstext.text = totalrocket.ToString();*/
        //El movimiento del player
        Vector2 inputAxis = Vector2.zero;
        inputAxis.x = Input.GetAxis("Horizontal");
        inputAxis.y = Input.GetAxis("Vertical");
        playerController.SetAxis(inputAxis);
        //El salto del player
        if (Input.GetButton("Jump")) playerController.StartJump();
        //Rotación de la cámara
        //Vector2 mouseAxis = Vector2.zero;
        // mouseAxis.x = Input.GetAxis("Mouse X") * sensitivity;
        // mouseAxis.y = Input.GetAxis("Mouse Y") * sensitivity;
        //Debug.Log("Mouse X = " + mouseAxis.x + " Y = " + mouseAxis.y);
        //lookRotation.SetRotation(mouseAxis);
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
        }
        if(Input.GetKeyDown(KeyCode.P))
    {
        Pausa();
    }
    }
    //Cursor del ratón
    // if (Input.GetMouseButtonDown(0)) mouseCursor.HideCursor();
    // else if (Input.GetKeyDown(KeyCode.Escape)) mouseCursor.ShowCursor();
    public void Damage(float value)
{
    //Health -= value;
   // HealthBar.size = Health / 100f;
}
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
/*public void Damage(float value)
{
    Health -= value;
    HealthBar.size = Health / 100f;
}*/
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
}




using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FPSInputManager : MonoBehaviour
{

 //   public SoundPlayer sound;
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


    //if(Input.GetKeyDown(KeyCode.R)) laser.Reload();

    // if (Input.Ge)

    //if (Input.GetMouseButtonDown(0) /*&& armas == 1 && ammo>0 && pause==true*/) {
    //     ballShoot.Shot();
    //ammo--;
    //sound.Play(3, 1);
    /* if (ammo < 30)
     {
         particula.Play();
     }
         }

    if (Input.GetMouseButtonDown(0) && armas == 2 && rocket>0 && pause == true)
    {
//            ballShoot.Shot_rocket();
        rocket--;
        sound.Play(5, 1);
        if (rocket < 3)
        {
            particula2.Play();
        }
    }
    if (Input.GetButtonDown("1"))
    {
        ak_47.SetActive(true);
        lanzacohetes.SetActive(false);
        armas = 1;
        ammotext.enabled = true;
        totalammotext.enabled = true;
        rocketstext.enabled = false;
        totalrocketstext.enabled = false;
    }
    if (Input.GetButtonDown("2"))
    {
        ak_47.SetActive(false);
        lanzacohetes.SetActive(true);
        armas = 2;
        ammotext.enabled = false;
        totalammotext.enabled = false;
        rocketstext.enabled = true;
        totalrocketstext.enabled = true;
    }
    if (HealthBar.size == 0)
    {
        HealthBar.enabled = false;
    }
    if (Input.GetKeyDown(KeyCode.P))
    {
        Pausa();
    }
    if (min == 0 && seg <= 1 ) Invoke("TiempoFinal",0.1f);
    if (HealthBar.enabled==false)
    {
        Cursor.visible = true;
        SceneManager.LoadScene("GameOver");
    }
    if (min2Text.enabled == true && seg2Text.enabled == true )
    {
        Cursor.visible = true;
        SceneManager.LoadScene("Victoria");
    }
    if (ammo == 0 && totalammo>=1)
    { if (counterreloads==true) { 
        recargar.SetTrigger("Reload2");
            counterreloads = false;
            sound.Play(2, 1);
        }
        else if (counterreloads == false)
        {
        recargar.SetTrigger("Reload");
            counterreloads = true;
            sound.Play(2, 1);
        }
        Invoke("Bullets", 2);
        ammo = -1;
        ammotext.enabled = false;
        ammo2text.enabled = true;

    }
    if (rocket == 0 && totalrocket>=1)
    {
        lanzacohetes.SetActive(false);

        Invoke("Activar", 1);
        rocket = -1;
        rocketstext.enabled = false;
        rockets2text.enabled = true;
    }
}
public void Damage(float value)
{
    Health -= value;
    HealthBar.size = Health / 100f;
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
        music.mute = false;
        sound.enabled = false;
    }
    else if (pause)
    {
        Time.timeScale = 0;
        pausaMenu.SetActive(true);
        AudioListener.pause = true;
        pause = false;
        Cursor.visible = true;
        music.mute = true;
        sound.enabled = true;
    }
}
public void Addtime()
{
    mseg++;
    if (mseg >= 60)
    {
        seg--;
        mseg = 0;
    }
    segText.text = seg.ToString();
    if (seg <= 0 )
    {
        min--;
        seg = 59;
    }
    minText.text = min.ToString();
}
void Activar()
{
    lanzacohetes.SetActive(true);
    totalrocket -= 3;
    rocket += 4;
    rocketstext.enabled = true;
    rockets2text.enabled = false;

}
void  Bullets()
{ 
    totalammo -= 30;
    ammo += 31;
    //recargar.SetBool("Reload", false);
    ammotext.enabled = true;
    ammo2text.enabled = false;
}
public void Heal(float value)
{
    Health += value;
    HealthBar.size = Health / 100f;
}
void Spawners()
{
    counter += Time.deltaTime;
    if (counter >= 100)
    {
            counter = 0;
    }
    if (counter >= 60 )
    {
        spawner.stop = true;
    }
    else if (counter < 60 && counter >100 )
    {
        spawner.stop = false;
    }
    }
 void TiempoFinal()
{
    minText.enabled = false;
    min2Text.enabled = true;
    segText.enabled = false;
    seg2Text.enabled = true;
}*/
}


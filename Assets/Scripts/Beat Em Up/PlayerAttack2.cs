using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack2 : MonoBehaviour
{
    public List<ComboState> attacks;
    public enum ComboState
    {
        NONE,
        DEBIL,
        DEBIL2,
        DEBIL3,
        FUERTE,
        FUERTE2,
        FUERTE3,
        GUARD,
        AIRCOMBO1,
        SOLO,
        JUMP,
    }
    public bool blockActivated = false;
    public SphereCollider Solocol;
    public Transform Guitar;
    private Vector3 currentGuitarpose;
    private Quaternion currentGuitarRotation;
    public HealthScript healthScript;
    public HealthUI healthUI;
    public bool enableAttacks;
    public PlayerAttackList attackList;
    public CapsuleCollider mycol;
    public BoxCollider guardCollider;
    public CharacterAnimation player_Anim;
    public PlayerMovementBeat player_Move;
    public ParticleSystem notas;
    public GameObject block_Fx;
    public GameObject shield;
    public bool doSolo;
    public bool rot;
    private bool activateTimerToReset;
    public bool is_Player;
    private float default_Combo_Timer = 0.95f;
    private float current_Combo_Timer;
    public bool canBlock=true;
    public ComboState current_Combo_State;

    // Start is called before the first frame update
    void Awake()
    {
        doSolo = false;
        enableAttacks = false;
        mycol = GetComponentInChildren<CapsuleCollider>();
        attackList = GetComponent<PlayerAttackList>();
        guardCollider =GameObject.FindGameObjectWithTag("Defense").GetComponent<BoxCollider>();
        player_Anim = GetComponent<CharacterAnimation>();
        player_Move = GetComponent<PlayerMovementBeat>();
        healthScript = GetComponent<HealthScript>();
        healthUI = GetComponent<HealthUI>();
    }
    void Start()
    {
        rot = false;
        canBlock = false;
        healthScript.canDoSolo = false;
        Guitar.localPosition = new Vector3(-0.118f, 0.014f, 0.083f);
        Solocol = GameObject.FindGameObjectWithTag("Solo").GetComponent<SphereCollider>();
        Solocol.GetComponent<SphereCollider>().enabled = false;
        guardCollider.enabled = false;
        current_Combo_Timer = default_Combo_Timer;
        current_Combo_State = ComboState.NONE;
    }
    void Update()
    {
            ComboAttacks();
            ResetComboState();
            if (current_Combo_State == ComboState.JUMP)
            {
                attackList.RemoveAllList();
                attackList.Attack = true;
            }
    }
    void ComboAttacks()
    {
       
        if (Input.GetButtonDown("AtaqueDebil"))
        {
            player_Move.attack = true;
            if (attacks.Contains(ComboState.DEBIL3)||current_Combo_State == ComboState.DEBIL3 || current_Combo_State == ComboState.FUERTE2 || current_Combo_State == ComboState.FUERTE3 || current_Combo_State == ComboState.GUARD || current_Combo_State == ComboState.SOLO || current_Combo_State==ComboState.AIRCOMBO1)
                return;
            if (current_Combo_State == ComboState.FUERTE)
                current_Combo_State = ComboState.DEBIL;
            if (!player_Move.inAir ) {
                current_Combo_State++;
                activateTimerToReset = true;
                current_Combo_Timer = default_Combo_Timer;
                if (enableAttacks)
                {
                    if (current_Combo_State == ComboState.DEBIL && !player_Move.inAir)
                    {
                        AddToTheList(ComboState.DEBIL);
                    }
                    if (current_Combo_State == ComboState.DEBIL2 && !player_Move.inAir)
                    {
                        AddToTheList(ComboState.DEBIL2);
                    }
                    if (current_Combo_State == ComboState.DEBIL3 && !player_Move.inAir)
                    {
                        AddToTheList(ComboState.DEBIL3);
                    }
                }   
            }
            else if (player_Move.inAir )
            {
                if (current_Combo_State == ComboState.JUMP)
                {
                    current_Combo_State = ComboState.AIRCOMBO1;
                }
                activateTimerToReset = true;
                current_Combo_Timer = default_Combo_Timer;
                player_Move.comboAereo = true;
                
                    if (current_Combo_State == ComboState.AIRCOMBO1)
                    {
                        AddToTheList(ComboState.AIRCOMBO1);
                    }  
            }
        }
        if (Input.GetButtonDown("AtaqueFuerte"))
        {
            player_Move.attack = true;
            if (attacks.Contains(ComboState.FUERTE3)||current_Combo_State == ComboState.FUERTE3 || current_Combo_State == ComboState.DEBIL3 || current_Combo_State == ComboState.GUARD || current_Combo_State == ComboState.SOLO)
                return;
            if (current_Combo_State == ComboState.NONE)
                current_Combo_State = ComboState.FUERTE;
            else if (current_Combo_State == ComboState.FUERTE || current_Combo_State == ComboState.FUERTE2) 
                current_Combo_State++;
            else if (current_Combo_State == ComboState.DEBIL || current_Combo_State == ComboState.DEBIL2 && !player_Move.inAir)
                current_Combo_State = ComboState.FUERTE2;
            activateTimerToReset = true;
            current_Combo_Timer = default_Combo_Timer;
            if (enableAttacks)
            {
                if (!player_Move.inAir)
                {
                    if (current_Combo_State == ComboState.FUERTE)
                    {
                        AddToTheList(ComboState.FUERTE);
                    }
                    if (current_Combo_State == ComboState.FUERTE2)
                    {
                        AddToTheList(ComboState.FUERTE2);
                    }
                    if (current_Combo_State == ComboState.FUERTE3)
                    {
                        AddToTheList(ComboState.FUERTE3);
                    }
                }
                
            }
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetAxisRaw("Solo") == 1 && Input.GetAxisRaw("Disparar") == 1)
        {
            if ( !player_Move.inAir && healthScript.canDoSolo && doSolo /*&& !player_Move.walk && !player_Move.running*/)
            {
                player_Move.attack = true;
                player_Move.jump = false;
                player_Move.canRotate = false;
                canBlock = false;
                enableAttacks = false;
                player_Move.move = false;
                current_Combo_State = ComboState.SOLO;
                //player_Move.move = false;
                currentGuitarpose = new Vector3(-0.2145597f, 0.1555082f, 1.084099f);
                currentGuitarRotation = Quaternion.Euler(-68.8f, -437.132f, -103.051f);
                Guitar.localPosition = currentGuitarpose;
                Guitar.localRotation = currentGuitarRotation;
                player_Anim.Solo();
                healthScript.solo = 0f;
                healthUI.SoloBar.fillAmount = healthScript.solo / 100;
            }
        }
        if (Input.GetAxisRaw("Evadir") == 1 && !blockActivated && canBlock /* && !player_Move.walk && !player_Move.running*/)
        {
            player_Move.attack = true;
            player_Move.walk = false;
            player_Move.running = false;
            //player_Move.move = false;
            current_Combo_State = ComboState.GUARD;
            if (player_Move.walk || player_Move.running)
            {
                player_Move.run_Speed = 0;
            }
            if (!player_Move.inAir)
            {
                if (current_Combo_State == ComboState.GUARD)
                {
                    player_Move.canRotate = false;
                    player_Move.move = false;
                    player_Anim.Block();
                    guardCollider.enabled = true;
                    blockActivated = true;
                }
            }
            else if (player_Move.inAir)
            {
                current_Combo_State = ComboState.JUMP;
            }
        }
        else if ( Input.GetAxisRaw("Evadir") == 0 && blockActivated)
        {
            current_Combo_State = ComboState.GUARD;
            if (player_Move.walk || player_Move.running)
            {
                if (player_Move.lockrotation == true)
                    transform.rotation = Quaternion.Euler(0, -180, 0);
                else if (player_Move.lockrotation == false)
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                //player_Move.canRotate = true;
            }
            if (!player_Move.inAir)
            {
                if (current_Combo_State == ComboState.GUARD)
                {
                    //Destroy(shield);
                    player_Anim.ResetBlock();
                    player_Move.move = false;
                    guardCollider.enabled = false;
                    blockActivated = false;
                    player_Move.canRotate = true;
                    attackList.Attack = true;
                    attackList.RemoveAllList();
                    current_Combo_State = ComboState.NONE;
                }
            }
            else if (player_Move.inAir)
            {
                current_Combo_State = ComboState.JUMP;
            }
        }
    }
    public void ResetComboState()
    {
        if (activateTimerToReset)
        {
            current_Combo_Timer -= Time.deltaTime;
            if (current_Combo_Timer <= 0f)
            {
                mycol.enabled = true;
                player_Move.move = true;
                player_Move.attack = true;
                player_Move.canRotate = true;
                player_Move.jump = true;
                player_Move.attack = false;
                current_Combo_State = ComboState.NONE;
                activateTimerToReset = false;
                current_Combo_Timer = default_Combo_Timer;
            }
        }
    }
    public void AddToTheList(ComboState state)
    {
        attacks.Add(state);
    }
    public void CanRotate()
    {
        player_Move.canRotate = true;
        player_Move.move = true;
    }
    private void ActivarColisiones()
    {
        this.gameObject.layer = 8;
        Guitar.localPosition = new Vector3(-0.118f, 0.014f, 0.083f);
        Guitar.localRotation = Quaternion.Euler(-61.589f, -631.912f, -92.93501f);
        mycol.enabled = true;
        enableAttacks = true;
        player_Move.move = true;
        player_Move.attack = true;
        player_Move.canRotate = true;
        canBlock = true;
        player_Move.jump = true;
        current_Combo_State = ComboState.NONE;
        Solocol.GetComponent<AttackUniversal>().enabled = false;
       //
    }
    private void DesActivarColisiones()
    {
        this.gameObject.layer = 0;
        mycol.enabled = false;
        player_Move.move = false;
    }
    private void ActiveSoloCol()
    {
        Solocol.GetComponent<AttackUniversal>().enabled = true;
    }
    private void ActivateSolo()
    {
        Solocol.gameObject.SetActive(true);
    }
    public void NotasPlay()
    {
        //notas.gameObject.SetActive(true);
        notas.Play();
        //notas.gameObject.transform.parent = null;
    }
    public void ResetCombo()
    {
        current_Combo_State = ComboState.NONE;
    }
    public void EnableControl()
    {
        rot = false;
        doSolo = true;
        canBlock = true;
        enableAttacks = true;
        healthScript.canDoSolo = true;
        player_Move.enableMovement = true;
        player_Move.jump = true;
        player_Move.canRotate = true;
   
    }
}


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
        AIRCOMBO2,
        AIRCOMBO3,
        AIRCOMBO4,
        AIRCOMBO5,
        SOLO
    }
    public bool blockActivated = false;
    public SphereCollider Solocol;
    public Transform Guitar;
    private Vector3 currentGuitarpose;
    private Quaternion currentGuitarRotation;
    public HealthScript healthScript;
    public HealthUI healthUI;
    public PlayerAttackList attackList;
    public CapsuleCollider mycol;
    public BoxCollider guardCollider;
    public CharacterAnimation player_Anim;
    private PlayerMovementBeat player_Move;
    private bool activateTimerToReset;
    public bool is_Player;
    private float default_Combo_Timer = 0.95f;
    private float current_Combo_Timer;

    public ComboState current_Combo_State;

    // Start is called before the first frame update
    void Awake()
    {
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
    }
    void ComboAttacks()
    {
       
        if (Input.GetButtonDown("AtaqueDebil"))
        {
            if (attacks.Contains(ComboState.DEBIL3)||current_Combo_State == ComboState.DEBIL3 || current_Combo_State == ComboState.FUERTE2 || current_Combo_State == ComboState.FUERTE3 || current_Combo_State == ComboState.GUARD || current_Combo_State == ComboState.SOLO || current_Combo_State == ComboState.AIRCOMBO5)
                return;
            if (current_Combo_State == ComboState.FUERTE)
                current_Combo_State = ComboState.DEBIL;
            if (!player_Move.inAir ) {
                current_Combo_State++;
                activateTimerToReset = true;
                current_Combo_Timer = default_Combo_Timer;
                
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
            else if (player_Move.inAir )
            {
                if (current_Combo_State == ComboState.NONE)
                {
                    current_Combo_State = ComboState.AIRCOMBO1;
                }
                else if (current_Combo_State == ComboState.AIRCOMBO1 || current_Combo_State == ComboState.AIRCOMBO2 || current_Combo_State == ComboState.AIRCOMBO3 || current_Combo_State == ComboState.AIRCOMBO4 && player_Move.inAir)
                    current_Combo_State++;
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
            if (attacks.Contains(ComboState.FUERTE3)||current_Combo_State == ComboState.FUERTE3 || current_Combo_State == ComboState.DEBIL3 || current_Combo_State == ComboState.GUARD || current_Combo_State == ComboState.SOLO)
                return;
            if (current_Combo_State == ComboState.NONE)
                current_Combo_State = ComboState.FUERTE;
            else if (current_Combo_State == ComboState.FUERTE || current_Combo_State == ComboState.FUERTE2 && !player_Move.inAir) 
                current_Combo_State++;
            else if (current_Combo_State == ComboState.DEBIL || current_Combo_State == ComboState.DEBIL2 && !player_Move.inAir)
                current_Combo_State = ComboState.FUERTE2;
            activateTimerToReset = true;
            current_Combo_Timer = default_Combo_Timer;
            
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
        if (Input.GetKeyDown(KeyCode.D) || Input.GetAxisRaw("Solo") == 1 && Input.GetAxisRaw("Disparar") == 1)
        {
            if ( !player_Move.inAir && healthScript.canDoSolo==true)
            {
                current_Combo_State = ComboState.SOLO;
                currentGuitarpose = new Vector3(-0.2145597f, 0.1555082f, 1.084099f);
                currentGuitarRotation = Quaternion.Euler(-68.8f, -437.132f, -103.051f);
                Guitar.localPosition = currentGuitarpose;
                Guitar.localRotation = currentGuitarRotation;
                player_Anim.Solo();
                healthScript.solo = 0f;
                healthUI.SoloBar.fillAmount = healthScript.solo / 100;
            }
        }
        if (Input.GetAxisRaw("Evadir") == 1 && !blockActivated)
        {
            current_Combo_State = ComboState.GUARD;
            if (current_Combo_State == ComboState.GUARD)
            {
                player_Anim.Block();
                guardCollider.enabled = true;
                blockActivated = true;
            } 
        }
        else if ( Input.GetAxisRaw("Evadir") == 0 && blockActivated)
        {
            current_Combo_State = ComboState.GUARD;
            if (current_Combo_State == ComboState.GUARD)
            {
                player_Anim.ResetBlock();
                guardCollider.enabled = false;
                blockActivated = false;
                current_Combo_State = ComboState.NONE;
            }
        }
    }
    void ResetComboState()
    {
        if (activateTimerToReset)
        {
            current_Combo_Timer -= Time.deltaTime;
            if (current_Combo_Timer <= 0f)
            {
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
    private void ActivarColisiones()
    {
        this.gameObject.layer = 8;
        //soloefect.SetActive(false);
        Guitar.localPosition = new Vector3(-0.118f, 0.014f, 0.083f);
        Guitar.localRotation = Quaternion.Euler(-61.589f, -631.912f, -92.93501f);
        mycol.enabled = true;
        current_Combo_State = ComboState.NONE;
        Solocol.GetComponent<AttackUniversal>().enabled = false;
    }
    private void DesActivarColisiones()
    {
        this.gameObject.layer = 0;
        mycol.enabled = false;
    }
    private void ActiveSoloCol()
    {
        Solocol.GetComponent<AttackUniversal>().enabled = true;
    }
}


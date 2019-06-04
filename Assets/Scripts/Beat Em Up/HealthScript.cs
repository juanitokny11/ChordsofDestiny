using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class HealthScript : MonoBehaviour
{
    public float maxHealth;
    public float health = 100f;
    public float solo = 0f;
    public GameObject soloCharged;
    public BattleZone zone;
    public AttackUniversal attack;
    private CharacterAnimation animationScript;
    private EnemyMovement enemyMovement;
    private NpcCulling npcCulling;
    private PlayerMovementBeat player_Move;
    private PlayerAttack2 player_Attack;
    public EnemyHealthUI healthUI;
    public GameObject score;
    private HealthUI health_UI;
    public BeatEmupManager gameManager;
    public TextMeshProUGUI numhits;
    public GameObject hits;
    public GameObject healthBar;
    public BossIA bossIA;
    public EaseColor fade;
    private LifeControler enemy_Health_UI;
    public PlayerAttackList playerAttack_List;
    public bool canDoSolo = false;
    public bool characterDied;
    public bool inAir=false;
    public int hitCounter;
    public int hitsCount;
    public bool oneTime;
    public SoundPlayer audios;
    public bool is_Player,is_Boss,is_Enemy;

    public void Start()
    {
        characterDied = false;
        npcCulling = FindObjectOfType<NpcCulling>();
        gameManager = FindObjectOfType<BeatEmupManager>();
        animationScript = GetComponent<CharacterAnimation>();
        enemyMovement = GetComponent<EnemyMovement>();
        if (is_Player)
        {
            fade.play = false;
            audios = GameObject.FindObjectOfType<SoundPlayer>();
            //attack = GetComponentInChildren<AttackUniversal>();
            score.SetActive(false);
            health_UI = GetComponent<HealthUI>();
            player_Move = GetComponent<PlayerMovementBeat>();
            player_Attack = GetComponent<PlayerAttack2>();
            player_Move.enabled = true;
            player_Attack.enabled = true;
            //healthBar.SetActive(false);
            playerAttack_List = GetComponent<PlayerAttackList>();
        }
        else if (is_Enemy)
        {
            enemy_Health_UI = GameObject.FindObjectOfType<LifeControler>();
        }
        if (is_Boss)
        {
            // bossIA = GetComponent<BossIA>();
            healthUI = GetComponent<EnemyHealthUI>();
        }
    }
    private void Update()
    {
        if (is_Player)
        {
            numhits.text = hitsCount.ToString();
            if (hitsCount != 0)
            {
                hits.SetActive(true);
                numhits.text = hitsCount.ToString();
                attack.counterhits += Time.deltaTime;
                if (attack.counterhits >= 5f)
                    hitsCount = 0;
            }
            else if (hitsCount == 0)
            {
                hits.SetActive(false);
                numhits.text = 00.ToString();
            }
        }
        if (solo >= 200)
        {
            if (is_Player)
            {
                canDoSolo = true;
                soloCharged.SetActive(true);
            }
        }
        else
        {
            if (is_Player)
            {
                canDoSolo = false;
                soloCharged.SetActive(false);
            }
        }
    }
    public void ApplyDamage(float damage,bool knockDown,bool defense)
    {
        if (characterDied)
            return;
        health -= damage;
        if (is_Player)
        { 
            health_UI.DisplayHealth(health);
        }
        else if(is_Enemy)
            enemy_Health_UI.DisplayHealth(health,maxHealth);
        else if (is_Boss)
            healthUI.DisplayHealth(health);
        if (health <= 0)
        {
            if (is_Player)
            {
                if (!is_Boss)
                    GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyMovement>().enabled = false;
                else if (is_Boss)
                    GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossIA>().enabled = false;
                player_Move.is_Dead = true;
                player_Attack.enabled = false;
                player_Move.jump = false;
                animationScript.Death(0);
            }
            else if(is_Enemy)
            {
                enemyMovement.Death();
                zone.enemiescounter--;
                npcCulling.RemoveNPC(GetComponent<NpcBevahavour>());
               gameManager.numScore += enemyMovement.score;
            }
            else if (is_Boss)
            {
                animationScript.Death(0);
                bossIA.Death();
                gameManager.numScore += bossIA.scoref2 ;
            }
            characterDied = true;
            return;
        }
        if (is_Enemy)
        {
            if (knockDown)
            {
                if (inAir)
                    animationScript.Tirar();
                else
                    animationScript.KnockDown();
            }
            else if (!knockDown)
            {
                if (Random.Range(0, 3) > 1)
                    animationScript.Hit(Random.Range(0, 3));
                if (Random.Range(0, 10)< 1)
                    animationScript.Stuned();
            }
        }
        if (is_Boss)
        {
            if (!knockDown)
            {
                hitCounter++;
                if (Random.Range(0, 3) > 1) {
                    if (bossIA.fase == 1)
                    {
                        animationScript.Hit2arms(Random.Range(0, 3));    
                    }
                    else
                    {
                        animationScript.Hit1arm(Random.Range(0, 3));
                    }
                }
            }
            if(hitCounter>=bossIA.porcentajeInvocar)
            {
                if (bossIA.fase == 1)
                {
                    animationScript.Invoke2arms();
                    hitCounter = 0;
                }
                else
                {
                    animationScript.Invoke1arm();
                    hitCounter = 0;
                }
            }
        }
    }
    public void DoFadeToBlack()
    {
        fade.play = true;
    }
}

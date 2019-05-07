using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AttackUniversal : MonoBehaviour
{
    public LayerMask colisionLayer;
    public float radius = 1f;
    public float damage = 2f;
    private Animator anim;
    public EnemyMovement enemy;
    public HealthUI healthUI;
    public HealthScript healthScript;
    public CharacterAnimation enemyAnim;
    public BossIA bossIA;
    public Vector3 puaSpawn;
    public LifeControler lifeControler;
    public HealthScript playerHealth;
    public GameObject UI;
    public bool is_Player, is_Enemy, is_Boss;
    public GameObject hit_Fx_Prefab;
    public GameObject block_Fx_Prefab;
    public GameObject block2_Fx_Prefab;
    public GameObject pua;
    public GameObject score;
    public Collider[] hit;
    public BeatEmupManager gameManager;
    //public bool solo=false;
    public SkinnedMeshRenderer rend;
    public float counterhits = 0f;
    public AudioSource block1;
    
    private void Start()
    {
        gameManager = FindObjectOfType<BeatEmupManager>();
        UI = GameObject.FindGameObjectWithTag("UI");
        lifeControler = GameObject.FindObjectOfType<LifeControler>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthScript>();
        enemy = GetComponentInParent<EnemyMovement>();
        enemyAnim = GetComponentInParent<CharacterAnimation>();
        healthUI = GetComponentInParent<HealthUI>();
        healthScript = GetComponentInParent<HealthScript>();
        if (is_Boss)
            bossIA = GetComponentInParent<BossIA>();
        this.gameObject.SetActive(false);
    }
    void Update()
    {
        DetectColision();
        
    }
    void DetectColision()
    {
        hit = Physics.OverlapSphere(transform.position, radius, colisionLayer);
       
        if (hit.Length > 0)
        {
            if (is_Player)
            {
                if (hit[0].gameObject.tag != "bidon")
                {
                    Quaternion hitFX_Rot = new Quaternion();
                    Vector3 hitFx_Pos = hit[0].transform.position;
                    hitFx_Pos.y += 3f;
                    if (hit[0].transform.forward.x > 0)
                    {
                        hitFx_Pos.x += 0.3f;
                        hitFX_Rot = Quaternion.Euler(0, 0, 0);
                    }
                    else if (hit[0].transform.forward.x < 0)
                    {
                        hitFx_Pos.x -= 0.3f;
                        hitFX_Rot = Quaternion.Euler(0, 180, 0);
                    }
                    Instantiate(hit_Fx_Prefab, hitFx_Pos, hitFX_Rot);
                }
                if (gameObject.CompareTag("Tirar"))
                {
                    //solo = false;
                    healthScript.inAir = false;
                    damage = 4;
                    hit[0].GetComponent<EnemyMovement>().soloHit = false;
                    healthScript.hitsCount++;
                    counterhits = 0;
                    healthScript.solo += damage;
                    healthUI.DisplaySolo(healthScript.solo / 2);
                    rend = hit[0].GetComponentInChildren<SkinnedMeshRenderer>();
                    MaterialPropertyBlock block = new MaterialPropertyBlock();
                    rend.GetPropertyBlock(block);
                    block.SetColor("_Color", Color.red);
                    rend.SetPropertyBlock(block);
                    Invoke("ReturnColor", 0.15f);
                    if (hit[0].gameObject.tag == "Enemy")
                        lifeControler.ShowDamagedUI(hit[0].gameObject.GetComponent<HealthScript>().health,hit[0].gameObject.GetComponent<EnemyMovement>().gname.ToString(), hit[0].gameObject.GetComponent<HealthScript>().maxHealth);
                }
                else if (gameObject.CompareTag("Solo"))
                {
                    //solo = true;
                    damage =30;
                    hit[0].GetComponent<EnemyMovement>().soloHit = true;
                    healthScript.hitsCount++;
                    counterhits = 0;
                    //healthUI.DisplaySolo(healthScript.solo / 2);
                    for (int i = 0; i < hit.Length; i++)
                    {
                        if(hit[i].gameObject.tag=="Enemy")
                            hit[i].GetComponent<EnemyMovement>().soloHit = true;
                        hit[i].GetComponent<HealthScript>().ApplyDamage(damage, true, false);
                    }
                    //healthScript.inAir = true;
                    //hit[0].GetComponent<BoxCollider>().enabled = true;
                    rend = hit[0].GetComponentInChildren<SkinnedMeshRenderer>();
                    MaterialPropertyBlock block = new MaterialPropertyBlock();
                    rend.GetPropertyBlock(block);
                    block.SetColor("_Color", Color.red);
                    rend.SetPropertyBlock(block);
                    Invoke("ReturnColor", 0.15f);
                    if (hit[0].gameObject.tag == "Enemy")
                        lifeControler.ShowDamagedUI(hit[0].gameObject.GetComponent<HealthScript>().health, hit[0].gameObject.GetComponent<EnemyMovement>().gname.ToString(), hit[0].gameObject.GetComponent<HealthScript>().maxHealth);
                }
                else if (gameObject.CompareTag("Levantar"))
                {
                    //solo = false;
                    damage = 4;
                    healthScript.hitsCount++;
                    hit[0].GetComponent<EnemyMovement>().soloHit = false;
                    counterhits = 0;
                    healthUI.DisplaySolo(healthScript.solo / 2);
                    hit[0].GetComponent<HealthScript>().ApplyDamage(damage, true, false);
                    healthScript.inAir = true;
                    //hit[0].GetComponent<BoxCollider>().enabled = true;
                    rend = hit[0].GetComponentInChildren<SkinnedMeshRenderer>();
                    MaterialPropertyBlock block = new MaterialPropertyBlock();
                    rend.GetPropertyBlock(block);
                    block.SetColor("_Color", Color.red);
                    rend.SetPropertyBlock(block);
                    Invoke("ReturnColor", 0.15f);
                    if (hit[0].gameObject.tag == "Enemy")
                        lifeControler.ShowDamagedUI(hit[0].gameObject.GetComponent<HealthScript>().health, hit[0].gameObject.GetComponent<EnemyMovement>().gname.ToString(), hit[0].gameObject.GetComponent<HealthScript>().maxHealth);
                }
                else
                {
                    if (gameObject.CompareTag("pesado"))
                    {
                        damage = 4;
                        healthScript.hitsCount++;
                        counterhits = 0;
                    }
                    else if (gameObject.CompareTag("ligero"))
                    {
                        damage = 3;
                        healthScript.hitsCount++;
                        counterhits = 0;
                    }
                    if (hit[0].gameObject.tag == "bidon")
                    {
                        hit[0].GetComponent<DeleteObjects>().vida--;
                        anim = hit[0].GetComponentInParent<Animator>();
                        hit[0].gameObject.SetActive(false);
                        puaSpawn = hit[0].transform.position;
                        if (hit[0].GetComponent<DeleteObjects>().vida <= 0)
                        {
                            anim.SetTrigger("Break");
                            Invoke("Explode", Time.deltaTime* 18f);
                        }
                    }
                    else if (hit[0].gameObject.tag != "bidon")
                    {
                        //solo = false;
                        hit[0].GetComponent<EnemyMovement>().soloHit = false;
                        healthScript.solo += damage;
                        healthUI.DisplaySolo(healthScript.solo / 2);
                        hit[0].GetComponent<HealthScript>().ApplyDamage(damage, false, false);
                        //hit[0].GetComponent<BoxCollider>().enabled = true;
                        rend = hit[0].GetComponentInChildren<SkinnedMeshRenderer>();
                        MaterialPropertyBlock block = new MaterialPropertyBlock();
                        rend.GetPropertyBlock(block);
                        block.SetColor("_Color", Color.red);
                        rend.SetPropertyBlock(block);
                        Invoke("ReturnColor", 0.15f);
                        if (hit[0].gameObject.tag == "Enemy")
                            lifeControler.ShowDamagedUI(hit[0].gameObject.GetComponent<HealthScript>().health, hit[0].gameObject.GetComponent<EnemyMovement>().gname.ToString(), hit[0].gameObject.GetComponent<HealthScript>().maxHealth);
                    }
                }
            }
            if (is_Enemy)
            {
                Vector3 hitFx_Pos = hit[0].transform.position;
                if (!is_Boss)
                {
                    if (hit[0].gameObject.CompareTag("Defense"))
                    {
                        block1.Play();
                        Quaternion blockFX_Rot = new Quaternion();
                        Vector3 blockFx_Pos = hit[0].transform.position;
                        blockFx_Pos.y += 4f;
                        if (hit[0].transform.forward.x > 0)
                        {
                          
                            blockFx_Pos.x += 2f;
                            blockFX_Rot = Quaternion.Euler(-45, 90, 0);
                        }
                        else if (hit[0].transform.forward.x < 0)
                        {
                            blockFx_Pos.x -= 2f;
                            blockFX_Rot = Quaternion.Euler(-45, -90, 0);
                        }
                        enemyAnim.Block();
                        Instantiate(block_Fx_Prefab, blockFx_Pos, blockFX_Rot);
                        damage = 0;
                        hit[0].GetComponentInParent<HealthScript>().ApplyDamage(damage, false, true);
                    }
                    else
                    {
                        Quaternion hitFFX_Rot = new Quaternion();
                        hitFx_Pos = hit[0].transform.position;
                        hitFx_Pos.y += 1f;
                        if (hit[0].transform.forward.x > 0)
                        {
                            hitFx_Pos.x += 0.3f;
                            hitFFX_Rot = Quaternion.Euler(0, 0, 0);
                        }
                        else if (hit[0].transform.forward.x < 0)
                        {
                            hitFx_Pos.x -= 0.3f;
                            hitFFX_Rot = Quaternion.Euler(0, 180, 0);
                        }
                        Instantiate(hit_Fx_Prefab, hitFx_Pos, hitFFX_Rot);
                        hit[0].GetComponentInParent<HealthScript>().ApplyDamage(damage, false, false);
                        playerHealth.hitsCount = 0;
                    }
                    //damage = 2;
                }
            }
                if (is_Boss)
                {
                Vector3 hitFx_Pos = hit[0].transform.position;
                if (hit[0].gameObject.CompareTag("Defense"))
                    {
                         block1.Play();
                         Quaternion blockFX_Rot = new Quaternion();
                        Vector3 blockFx_Pos = hit[0].transform.position;
                        blockFx_Pos.y += 4f;
                        if (hit[0].transform.forward.x > 0)
                        {
                            blockFx_Pos.x += 2f;
                            blockFX_Rot = Quaternion.Euler(-45, 90, 0);
                        }
                        else if (hit[0].transform.forward.x < 0)
                        {
                            blockFx_Pos.x -= 2f;
                            blockFX_Rot = Quaternion.Euler(-45, -90, 0);
                        }
                        enemyAnim.Block();
                        Instantiate(block_Fx_Prefab, blockFx_Pos, blockFX_Rot);
                        damage = 0;
                        hit[0].GetComponentInParent<HealthScript>().ApplyDamage(damage, false, true);
                    }
                    else
                    {
                        Quaternion hitFFX_Rot = new Quaternion();
                        hitFx_Pos = hit[0].transform.position;
                        hitFx_Pos.y += 1f;
                        if (hit[0].transform.forward.x > 0)
                        {
                            hitFx_Pos.x += 0.3f;
                            hitFFX_Rot = Quaternion.Euler(0, 0, 0);
                        }
                        else if (hit[0].transform.forward.x < 0)
                        {
                            hitFx_Pos.x -= 0.3f;
                            hitFFX_Rot = Quaternion.Euler(0, 180, 0);
                        }
                        Instantiate(hit_Fx_Prefab, hitFx_Pos, hitFFX_Rot);
                        hit[0].GetComponentInParent<HealthScript>().ApplyDamage(damage, false, false);
                        playerHealth.hitsCount = 0;
                    }
                        damage = 3;
                    
                }
                gameObject.SetActive(false);
            }
        }

    void Blink()
    {
        if (Time.fixedTime % .5 < .2)
        {
            hit[0].gameObject.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            hit[0].gameObject.transform.parent.gameObject.SetActive(true);
        }
    }
        public void Explode()
    {
        InvokeRepeating("Blink", 0.1f, 0.1f);
       gameManager.numScore += 5;
        score.SetActive(true);
        if (playerHealth.health <= 100)
            Invoke("LifeOn", 2f);
        Invoke("StopBlink",3f);
    }
    public void StopBlink()
    {
        CancelInvoke("Blink");
        score.SetActive(false);
        Instantiate(pua, puaSpawn, Quaternion.identity);
        if (playerHealth.health <= 100)
            Invoke("LifeOff", 2f);
        Invoke("Destroy", 0.1f);
    }
    public void Destroy()
    {
        Destroy(hit[0].gameObject.transform.parent.gameObject);
    }
    private void OnDrawGizmos()
    {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, radius);
    }
    private void ReturnColor()
    {
        MaterialPropertyBlock block = new MaterialPropertyBlock();
        rend.GetPropertyBlock(block);
        block.Clear();
        rend.SetPropertyBlock(block);
    }
    public void LifeOn()
    {
        UI.SetActive(true);
    }
    public void LifeOff()
    {
        UI.SetActive(false);
    }
}


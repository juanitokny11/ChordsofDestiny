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
    public EnemyMovement enemy;
    public HealthUI healthUI;
    public HealthScript healthScript;
    public CharacterAnimation enemyAnim;
    public BossIA bossIA;
    public LifeControler lifeControler;
    public HealthScript playerHealth;
    public bool is_Player, is_Enemy, is_Boss;
    public GameObject hit_Fx_Prefab;
    public GameObject block_Fx_Prefab;
    public GameObject block2_Fx_Prefab;
    public Collider[] hit;
    public SkinnedMeshRenderer rend;
    public float counterhits = 0f;
    public AudioSource block1;
    
    private void Start()
    {
        lifeControler = GameObject.FindObjectOfType<LifeControler>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthScript>();
        enemy = GetComponentInParent<EnemyMovement>();
        enemyAnim = GetComponentInParent<CharacterAnimation>();
        healthUI = GetComponentInParent<HealthUI>();
        healthScript = GetComponentInParent<HealthScript>();
        if (is_Boss)
            bossIA = GetComponentInParent<BossIA>();
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
                    Vector3 hitFx_Pos = hit[0].transform.position;
                    hitFx_Pos.y += 3f;
                    if (hit[0].transform.forward.x > 0)
                        hitFx_Pos.x += 0.3f;
                    else if (hit[0].transform.forward.x < 0)
                        hitFx_Pos.x -= 0.3f;
                    Instantiate(hit_Fx_Prefab, hitFx_Pos, Quaternion.identity);
                }
                if (gameObject.CompareTag("Tirar"))
                {
                    healthScript.inAir = false;
                    damage = 4;
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
                        lifeControler.ShowDamagedUI(hit[0].gameObject.GetComponent<HealthScript>().health,hit[0].gameObject.GetComponent<EnemyMovement>().gname.ToString());
                }
                else if (gameObject.CompareTag("Levantar"))
                {
                    damage = 4;
                    healthScript.hitsCount++;
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
                        lifeControler.ShowDamagedUI(hit[0].gameObject.GetComponent<HealthScript>().health, hit[0].gameObject.GetComponent<EnemyMovement>().gname.ToString());
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
                        if (hit[0].GetComponent<DeleteObjects>().vida <= 0)
                        {
                            Explode();
                        }
                    }
                    else if (hit[0].gameObject.tag != "bidon")
                    {
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
                            lifeControler.ShowDamagedUI(hit[0].gameObject.GetComponent<HealthScript>().health, hit[0].gameObject.GetComponent<EnemyMovement>().gname.ToString());
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
            hit[0].gameObject.SetActive(false);
        }
        else
        {
            hit[0].gameObject.SetActive(true);
        }
    }
        public void Explode()
    {
        InvokeRepeating("Blink", 0.1f, 0.1f);
        Invoke("StopBlink",3f);
    }
    public void StopBlink()
    {
        CancelInvoke("Blink");
        Invoke("Destroy", 0.1f);
    }
    public void Destroy()
    {
        Destroy(hit[0].gameObject);
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
}


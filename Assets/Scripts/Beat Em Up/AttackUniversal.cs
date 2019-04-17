using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public bool is_Player, is_Enemy,is_Boss;
    public GameObject hit_Fx_Prefab;
    public GameObject block_Fx_Prefab;
    public GameObject block2_Fx_Prefab;
    private void Start()
    {
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
        Collider[] hit = Physics.OverlapSphere(transform.position,radius,colisionLayer);

        if (hit.Length > 0)
        {
            if (is_Player)
            {
                Vector3 hitFx_Pos = hit[0].transform.position;
                hitFx_Pos.y += 1.3f;
                if (hit[0].transform.forward.x > 0)
                    hitFx_Pos.x += 0.3f;
                else if (hit[0].transform.forward.x < 0)
                    hitFx_Pos.x -= 0.3f;
                //Instantiate(hit_Fx_Prefab, hitFx_Pos, Quaternion.identity);
                if (gameObject.CompareTag("Tirar"))
                {
                    healthScript.inAir = false;
                    damage = 4;
                    healthScript.solo += damage;
                    healthUI.DisplaySolo(healthScript.solo/2);
                    if(is_Enemy && !is_Boss)
                        hit[0].GetComponent<BoxCollider>().enabled = true;
                }
                  else if (gameObject.CompareTag("Levantar"))
                {
                    damage = 4;
                    healthUI.DisplaySolo(healthScript.solo / 2);
                    hit[0].GetComponent<HealthScript>().ApplyDamage(damage, true);
                    if (is_Enemy && !is_Boss)
                    {
                        healthScript.inAir = true;
                        hit[0].GetComponent<BoxCollider>().enabled = true;
                    }
                }
                else
                {
                    if (gameObject.CompareTag("pesado"))
                    {
                        damage = 4;
                    }
                    else if (gameObject.CompareTag("ligero"))
                    {
                        damage = 3;
                    }
                    healthScript.solo += damage;
                    healthUI.DisplaySolo(healthScript.solo / 2);
                    hit[0].GetComponent<HealthScript>().ApplyDamage(damage, false);
                    if(is_Enemy && !is_Boss)
                        hit[0].GetComponent<BoxCollider>().enabled=true;
                }
            }
            if (is_Enemy)
            {
                if (!is_Boss)
                { 
                    if (hit[0].gameObject.CompareTag("Defense"))
                    {
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
                    }
                    else
                    {
                        hit[0].GetComponentInParent<HealthScript>().ApplyDamage(damage, false);
                    }
                    damage = 2;
                }
                if (is_Boss)
                {
                    if (hit[0].gameObject.CompareTag("Defense"))
                    {
                        enemyAnim.Block();
                    }
                    else
                        hit[0].GetComponentInParent<HealthScript>().ApplyDamage(damage, false);
                    damage = 4;
                }
            }
            gameObject.SetActive(false);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

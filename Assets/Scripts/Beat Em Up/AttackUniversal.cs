using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUniversal : MonoBehaviour
{
    public LayerMask colisionLayer;
    public float radius = 1f;
    public float damage = 2f;
    public HealthUI healthUI;
    public HealthScript healthScript;
    public CharacterAnimation enemyAnim;
    public BossIA bossIA;
    public bool is_Player, is_Enemy,is_Boss;
    public GameObject hit_Fx_Prefab;
    private void Start()
    {
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
                        enemyAnim.Block();
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

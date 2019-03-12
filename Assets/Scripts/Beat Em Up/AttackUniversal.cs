using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUniversal : MonoBehaviour
{
    public LayerMask colisionLayer;
    public float radius = 1f;
    public float damage = 2f;

    public bool is_Player, is_Enemy;

    public GameObject hit_Fx_Prefab;

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

                if(gameObject.CompareTag("Levantar"))
                {
                    hit[0].GetComponent<HealthScript>().ApplyDamage(damage, true);
                }
                else
                {
                    hit[0].GetComponent<HealthScript>().ApplyDamage(damage, false);
                }
            }
            if (is_Enemy)
            {
                hit[0].GetComponent<HealthScript>().ApplyDamage(damage, false);
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

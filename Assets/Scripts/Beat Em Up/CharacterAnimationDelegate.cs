using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationDelegate : MonoBehaviour
{
    public GameObject guitar_Attack_Point;
    public float standupTimer = 2.0f;
    private CharacterAnimation animationScript;
    private EnemyMovement enemy_Movement;
    private void Awake()
    {
        animationScript = GetComponent<CharacterAnimation>();
        if(gameObject.CompareTag("Enemy"))
            enemy_Movement = GetComponent<EnemyMovement>();
    }

    void Guitar_Attack_Point_ON()
    {
        guitar_Attack_Point.SetActive(true);
    }
    void Guitar_Attack_Point_OFF()
    {
        if (guitar_Attack_Point.activeInHierarchy)
            guitar_Attack_Point.SetActive(false);
    }
    void Tag_Fuerte()
    {
        guitar_Attack_Point.tag = "pesado";
    }
    void Tag_Fuerte2()
    {
        guitar_Attack_Point.tag = "Levantar";
    }
    void Tag_Debil()
    {
        guitar_Attack_Point.tag = "ligero";
    }
    void Untag_Guitar()
    {
        guitar_Attack_Point.tag = "Untagged";
    }
    void Enemy_StandUp()
    {
        StartCoroutine(StandUpAfterTime());
    }
    IEnumerator StandUpAfterTime()
    {
        yield return new WaitForSeconds(standupTimer);
        animationScript.StandUp();
    }
    void EnableMovement()
    {
        enemy_Movement.enabled = true;
    }
    void DisableMovement()
    {
        enemy_Movement.enabled = false;
    }
    void NodamageforEnemy()
    {
        transform.gameObject.layer = 0;
    }
    void DamageforEnemy()
    {
        transform.gameObject.layer = 11;
    }
}

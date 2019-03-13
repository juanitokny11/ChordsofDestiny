using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationDelegate : MonoBehaviour
{
    public GameObject guitar_Attack_Point;
    public GameObject groupie_Attack_Point;
    public float standupTimer = 2.0f;
    public HealthScript damage;
    private CharacterAnimation animationScript;
    private EnemyMovement enemy_Movement;
    private ShakeCamera shakeCamera;
    private void Awake()
    {
        damage = GetComponent<HealthScript>();
        animationScript = GetComponent<CharacterAnimation>();
        shakeCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ShakeCamera>();
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
    void Groupie_Attack_Point_ON()
    {
        groupie_Attack_Point.SetActive(true);
    }
    void Groupie_Attack_Point_OFF()
    {
        if (groupie_Attack_Point.activeInHierarchy)
            groupie_Attack_Point.SetActive(false);
    }
    void Tag_EnemyDamage()
    {
        groupie_Attack_Point.tag = "EnemyDamage";
    }
    void Tag_Fuerte()
    {
        guitar_Attack_Point.tag = "pesado";
    }
    void Tag_Fuerte2()
    {
        guitar_Attack_Point.tag = "Levantar";
    }
    void Tag_Tirar()
    {
        guitar_Attack_Point.tag = "Tirar";
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
    void ShakeOnFall()
    {
        shakeCamera.ShouldShake=true;
    }
    void CharacterDied()
    {
        Invoke("DeleteGameobject", Random.Range(2f, 5f));
    }
    void DeleteGameobject()
    {
        EnemyManager.instance.SpawnEnemy();
        Destroy(gameObject);
    }

}

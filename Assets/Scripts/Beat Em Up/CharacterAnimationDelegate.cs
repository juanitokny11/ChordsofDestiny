using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationDelegate : MonoBehaviour
{
    public GameObject guitar_Attack_Point;
    public GameObject groupie_Attack_Point;
    public SkinnedMeshRenderer groupie;
    public CapsuleCollider groupiecol;
    public EnemyMovement groupieEnemy;
    public Rigidbody groupieBody;
    public float standupTimer = 2.0f;
    private CharacterAnimation animationScript;
    private EnemyMovement enemy_Movement;
    private ShakeCamera shakeCamera;
    private void Awake()
    {
        groupie =GetComponentInChildren<SkinnedMeshRenderer>();
        groupieEnemy = GetComponent<EnemyMovement>();
        groupieBody = GetComponent<Rigidbody>();
        groupiecol = GetComponent<CapsuleCollider>();
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
        groupiecol.enabled = false;
        groupieBody.useGravity = false;
        groupieEnemy.enabled = false;
        Invoke("DeleteGameobject",3.0f);
    }
    void DeleteGameobject()
    {
        InvokeRepeating("Blink", 0f, 0.2f);
        //EnemyManager.instance.SpawnEnemy();
        Invoke("DestroyGameobject", 4.0f);
    }
     void Blink()
    {
        if (Time.fixedTime % .5 < .2)
        {
            groupie.enabled = false;
        }
        else
        {
            groupie.enabled = true;
        }
    }
    void DestroyGameobject()
    {
        Destroy(gameObject);
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CharacterAnimationDelegate : MonoBehaviour
{
    public SoundPlayer clips;
    public ParticleSystem jumpefect;
    public GameObject guitar_Attack_Point;
    public GameObject Boss_Attack_Point1;
    public GameObject Boss_Attack_Point2;
    public GameObject groupie_Attack_Point;
    public SkinnedMeshRenderer[] fan;
    public SkinnedMeshRenderer bate;
    public MeshRenderer espada;
    public CapsuleCollider mycol;
    public GameObject espadaanimada;
    public SkinnedMeshRenderer groupie;
    public CapsuleCollider groupiecol;
    public EnemyMovement groupieEnemy;
    public BossIA bossIA;
    public PlayerMovementBeat player_Move;
    public Rigidbody groupieBody;
    public float standupTimer = 2.0f;
    private CharacterAnimation animationScript;
    private EnemyMovement enemy_Movement;
    private ShakeCamera shakeCamera;
    public bool isGroupie,is_Boss,isFan;

    private void Awake()
    {
        if (isGroupie)
        { 
            groupie = GetComponentInChildren<SkinnedMeshRenderer>();
            groupieEnemy = GetComponent<EnemyMovement>();
        }
        else if (isFan)
        {
            fan = GetComponentsInChildren<SkinnedMeshRenderer>();
            bate = fan[0];
            groupie= fan[1];
            groupieEnemy = GetComponent<EnemyMovement>();
        }
        else if (is_Boss)
        {
            groupie = GetComponentInChildren<SkinnedMeshRenderer>();
            //espada = GetComponentInChildren<MeshRenderer>();
            bossIA = GetComponent<BossIA>();
        }
        else
        {
            player_Move = GetComponent<PlayerMovementBeat>();
            jumpefect = GetComponentInChildren<ParticleSystem>();
            clips = GetComponentInChildren<SoundPlayer>();
        }
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
    void Boss_2armsAttack_Point_ON()
    {
        Boss_Attack_Point1.SetActive(true);
        Boss_Attack_Point2.SetActive(true);
    }
    void Boss_2armsAttack_Point_OFF()
    {
        if (Boss_Attack_Point1.activeInHierarchy)
            Boss_Attack_Point1.SetActive(false);
        if (Boss_Attack_Point2.activeInHierarchy)
            Boss_Attack_Point2.SetActive(false);
    }
    void Boss_1armAttack_Point_ON()
    {
        Boss_Attack_Point1.SetActive(true);
    }
    void Boss_1armAttack_Point_OFF()
    {
        if (Boss_Attack_Point1.activeInHierarchy)
            Boss_Attack_Point1.SetActive(false);
    }
    void PlayerDisableMovement()
    {
        player_Move.enabled = false;
    }
    void PlayerEnableMovement()
    {
        player_Move.enabled = true;
    }
    void In_Air()
    {
        player_Move.inAir = true;
    }
    void Not_In_Air()
    {
        player_Move.inAir = false;
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
        groupiecol.enabled = false;
    }
    void DamageforEnemy()
    {
        transform.gameObject.layer = 11;
        groupiecol.enabled = true;
    }
    void DamageforPlayer()
    {
        transform.gameObject.layer = 8;
    }
    void ShakeOnFall()
    {
        shakeCamera.ShouldShake=true;
    }
    void CharacterInFloor()
    {
        groupieBody.isKinematic = true;
    }
    void CharacterUp()
    {
        groupieBody.isKinematic = false;
    }
   
    void CharacterDied()
    {
        groupiecol.enabled = false;
        groupieBody.useGravity = false;
        if (!is_Boss)
            groupieEnemy.enabled = false;
        else
            bossIA.enabled = false;
        Invoke("DeleteGameobject", 2.0f);
    }
    void DeleteGameobject()
    {
        InvokeRepeating("Blink", 0f, 0.05f);
        Invoke("DestroyGameobject", 4.0f);
    }
    public void EnableEspadaAnim()
    {
        espadaanimada.gameObject.transform.parent = null;
        espadaanimada.GetComponent<Animator>().enabled = true;
    }
    void Blink()
    {
        if (Time.fixedTime % .5 < .2)
        {
            groupie.enabled = false;
             if (isFan)
            {
                bate.enabled = false;
            }
            if (is_Boss)
            {
                espada.enabled = false;
            }
        }
        else
        {
            groupie.enabled = true;
            if (isFan)
            {
                bate.enabled = true;
            }
            if (is_Boss)
            {
                espada.enabled = true;
            }
        }
    }
    void DestroyGameobject()
    {
        Destroy(gameObject);
    }
    public void JumpSound()
    {
        clips.Play(4, 1);
        jumpefect.Play();
    }
}

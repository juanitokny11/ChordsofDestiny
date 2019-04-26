using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class CharacterAnimationDelegate : MonoBehaviour
{
    public SoundPlayer clips;
    public ParticleSystem jumpefect;
    public ParticleSystem soloParticle;
    public GameObject guitar_Attack_Mesh;
    public GameObject guitar_Attack_Point;
    public GameObject Boss_Attack_Point1;
    public GameObject Boss_Attack_Point2;
    public AttackUniversal groupie_Attack_Point;
    public SkinnedMeshRenderer fan;
    public SkinnedMeshRenderer bate;
    public MeshRenderer espada;
    public CapsuleCollider mycol;
    public GameObject espadaanimada;
    public SkinnedMeshRenderer groupie;
    public PlayerAttack2 playerAttack;
    public CapsuleCollider groupiecol;
    public EnemyMovement groupieEnemy;
    public BossIA bossIA;
    public PlayerMovementBeat player_Move;
    public Rigidbody groupieBody;
    public float standupTimer = 2.0f;
    private CharacterAnimation animationScript;
    private EnemyMovement enemy_Movement;
    private ShakeCamera shakeCamera;
    public GameObject particleHacha;
    public bool isGroupie,is_Boss,isFan;

    private void Awake()
    {
        if (isGroupie)
        { 
            groupie = GetComponentInChildren<SkinnedMeshRenderer>();
            groupieEnemy = GetComponent<EnemyMovement>();
            groupie_Attack_Point = GetComponentInChildren<AttackUniversal>();
            groupie_Attack_Point.gameObject.SetActive(false);
        }
        else if (isFan)
        {
            groupie = GetComponentInChildren<SkinnedMeshRenderer>();
            bate = groupie;
            groupieEnemy = GetComponent<EnemyMovement>();
            groupie_Attack_Point = GetComponentInChildren<AttackUniversal>();
            groupie_Attack_Point.gameObject.SetActive(false);
        }
        else if (is_Boss)
        {
            groupie = GetComponentInChildren<SkinnedMeshRenderer>();
            //espada = GetComponentInChildren<MeshRenderer>();
            bossIA = GetComponent<BossIA>();
        }
        else
        {
            playerAttack = GetComponent<PlayerAttack2>();
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
        //guitar_Attack_Mesh.GetComponent<MeshCollider>().enabled = true;
    }
    void Guitar_Attack_Point_OFF()
    {
        if (guitar_Attack_Point.activeInHierarchy)
            guitar_Attack_Point.SetActive(false);
        //guitar_Attack_Mesh.GetComponent<MeshCollider>().enabled = false;
    }
    void Groupie_Attack_Point_ON()
    {
        groupie_Attack_Point.gameObject.SetActive(true);
    }
    void Groupie_Attack_Point_OFF()
    {
        if (groupie_Attack_Point.gameObject.activeInHierarchy)
            groupie_Attack_Point.gameObject.SetActive(false);
    }
    void Boss_2armsAttack_Point_ON()
    {
        Boss_Attack_Point1.SetActive(true);
        Boss_Attack_Point2.SetActive(true);
        groupieBody.isKinematic = true;
    }
    void Boss_2armsAttack_Point_OFF()
    {
        if (Boss_Attack_Point1.activeInHierarchy)
            Boss_Attack_Point1.SetActive(false);
        if (Boss_Attack_Point2.activeInHierarchy)
            Boss_Attack_Point2.SetActive(false);
        groupieBody.isKinematic = false;
    }
    void Boss_1armAttack_Point_ON()
    {
        Boss_Attack_Point1.SetActive(true);
        groupieBody.isKinematic = true;
    }
    void Boss_1armAttack_Point_OFF()
    {
        if (Boss_Attack_Point1.activeInHierarchy)
            Boss_Attack_Point1.SetActive(false);
        groupieBody.isKinematic = false;
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
        //groupiecol.enabled = false;
    }
    void DamageforEnemy()
    {
        transform.gameObject.layer = 11;
        //groupiecol.enabled = true;
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
            if (is_Boss)
            {
                espada.enabled = false;
            }
        }
        else
        {
            groupie.enabled = true;
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
        playerAttack.current_Combo_State = PlayerAttack2.ComboState.NONE;
    }
    public void Lost()
    {
        SceneManager.LoadScene("GameOver");
    }
    public void ParticleHachaOn()
    {
        particleHacha.SetActive(true);
    }
    public void ParticleHachaOff()
    {
        particleHacha.SetActive(false);
    }
    public void ParticleSoloOn()
    {
        soloParticle.Play();
    }
    public void ParticleSoloOff()
    {
        soloParticle.Stop();
    }
}

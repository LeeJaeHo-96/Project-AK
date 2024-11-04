using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HYJ_Boss_Stage2 : MonoBehaviour
{
    [Header("�÷��̾�")]
    [SerializeField] GameObject player;
    [Header("���� ����")]
    [SerializeField] Animator animator;
    [SerializeField] GameObject monster;
    [SerializeField] public float nowHp;
    [SerializeField] public float SetHp;
    //[SerializeField] float 
    [SerializeField] public float monsterShieldAtkPower;
    [SerializeField] public float monsterHpAtkPower;
    [SerializeField] public float monsterMoveSpeed;

    [Header("��� ������Ʈ")]
    [SerializeField] GameObject fireBallPrefab;
    [SerializeField] GameObject silentBallPrefab;
    [SerializeField] GameObject stonePaPrefab;

    [Header("���� ����")]
    [SerializeField] public bool hitFlag;
    public bool HitFlag { get { return hitFlag; } set { hitFlag = value; } }
    public bool isAttack;
    public bool nowAttack;
    public bool isDie;
    Coroutine hitFlagCoroutine;
    WaitForSeconds hitFlagWaitForSeconds = new WaitForSeconds(0.1f);
    public float fireBallCoolTime = 10;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameObject.tag = "Boss";
        SetHp = 4000f;
        nowHp = SetHp;


    }

    void Update()
    {
        if(nowHp < 50)
        {
            fireBallCoolTime = 4f;
        }
    }

    IEnumerator MagicCircleDestroy()
    {

        yield return new WaitForSeconds(6f);
    }

    IEnumerator FireBall()
    {
        monsterShieldAtkPower = 1f;
        monsterHpAtkPower = 3000f;
        //FireBall ������Ʈ ����
        Instantiate(fireBallPrefab, new Vector3(monster.transform.position.x, monster.transform.position.y + 2f, monster.transform.position.z + 0.5f), Quaternion.LookRotation(player.transform.position));
        Instantiate(fireBallPrefab, new Vector3(monster.transform.position.x + 2f, monster.transform.position.y + 2f, monster.transform.position.z + 0.5f), Quaternion.LookRotation(player.transform.position));
        Instantiate(fireBallPrefab, new Vector3(monster.transform.position.x - 2f, monster.transform.position.y + 2f, monster.transform.position.z + 0.5f), Quaternion.LookRotation(player.transform.position)); 
        yield return new WaitForSeconds(fireBallCoolTime);
    }

    IEnumerator SilentBall()
    {
        // ��ô �Ź� ��ũ��Ʈ�� Ȱ���Ͽ� SilentBall ����
        Instantiate(silentBallPrefab, new Vector3(monster.transform.position.x, monster.transform.position.y + 2f, monster.transform.position.z + 0.5f), Quaternion.LookRotation(player.transform.position));
        yield return new WaitForSeconds(10f);
    }

    IEnumerator StonePa()
    {
        monsterShieldAtkPower = 1f;
        monsterHpAtkPower = 1000f;
        // ������ ������Ʈ ����
        Instantiate(stonePaPrefab);
        yield return new WaitForSeconds(10f);
    }

    public void Defenseless()
    {

    }

    public void MonsterTakeDamageCalculation(float damage)
    {
        nowHp -= damage;
    }

    public void StartHitFlagCoroutine()
    {
        if (hitFlagCoroutine != null)
        {
            StopCoroutine(hitFlagCoroutine);
        }
        hitFlagCoroutine = StartCoroutine(HitFlagCoroutine());
    }

    IEnumerator HitFlagCoroutine()
    {
        yield return hitFlagWaitForSeconds;
        hitFlag = false;
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class HYJ_Boss_Stage1 : MonoBehaviour
{
    [Header("�÷��̾�")]
    [SerializeField] GameObject player;
    [Header("���� ����")]
    [SerializeField] Animator animator;
    [SerializeField] GameObject monster;
    [SerializeField] float nowHp;
    [SerializeField] float SetHp;
    //[SerializeField] float 
    [SerializeField] public float monsterShieldAtkPower;
    [SerializeField] public float monsterHpAtkPower;
    [SerializeField] public float monsterMoveSpeed;

    [Header("���� ����")]
    [SerializeField] public bool hitFlag;
    public bool HitFlag { get { return hitFlag; } set { hitFlag = value; } }
    public bool isAttack;
    public bool nowAttack;
    public bool isDie;
    Coroutine hitFlagCoroutine;
    WaitForSeconds hitFlagWaitForSeconds = new WaitForSeconds(0.1f);
    private bool firstBattleEnd=false;
    private bool pFirst = false;
    private bool pSecond = false;
    private bool p10 = false;
    private bool p40 = false;
    private bool p70 = false;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameObject.tag = "Boss";
        SetHp = 3500f;
        monsterMoveSpeed = 1.5f;
        nowHp = SetHp;
        
    }

    private void Update()
    {
        MonsterDie();
        BossMove();
        if (!firstBattleEnd && !pFirst && !pSecond)
        {
            //StartCoroutine(BossBattleStart());
        }
        else if (firstBattleEnd && pFirst && pSecond)
        {
            //StartCoroutine(BossAI());
        }
    }

    // Comment : ��彺�� ����
    private void PatternHeadSpin()
    {
        Debug.Log("��彺��");
        monsterShieldAtkPower = 4000f;
        monsterHpAtkPower = 5f;
        animator.SetTrigger("HeadSpin");

    }

    // Comment : �극��ũ�� ����
    private void PatternBreakDance()
    {
        Debug.Log("�극��ũ��");
        monsterShieldAtkPower = 1000;
        monsterHpAtkPower = 3;
        animator.SetTrigger("BreakDance");

    }

    // Comment : ������� ����
    private void PatternSiiuuuu()
    {
        Debug.Log("�����Ӵ�");
        monsterShieldAtkPower = 3000f;
        monsterHpAtkPower = 1f;
        animator.SetTrigger("Siuu");
        Vector3 bossPos = monster.transform.position;
        //monster.transform.
    }

    // Comment : ���� ���� ����
    private void MonsterDie()
    {
        if (nowHp <= 0)
        {
            //��� �ִϸ��̼�
            //monsterAnimator.SetTrigger("Die");
            Debug.Log("���");
            Destroy(gameObject, 2f);
        }
    }

    // Comment : ���� ���� ���� 
    IEnumerator BossBattleStart()
    {
        Debug.Log("����");
        if (!pFirst)
        {
            // Comment : 
            Debug.Log("���� �н��� ù �������� �극��ũ ���� �Ѵ�.");
            PatternBreakDance();
            pFirst = true;
            yield return new WaitForSeconds(4f);
        }
        if (pFirst && !pSecond)
        {
            // Comment : 
            Debug.Log("���� �н��� �ι�° �������� ������� ������ �Ѵ�.");
            PatternSiiuuuu();
            pSecond = true;
            yield return new WaitForSeconds(4f);
            firstBattleEnd = true;
        }
    }


    // Comment : 
    IEnumerator BossAI()
    {
        if (nowHp < 2450f && !p70)
        {
            // Comment : ���� HP�� ó������ 70�� �Ʒ��� �Ǿ� ��彺���� ����Ѵ�.
            p70 = true;
            PatternHeadSpin();
            Debug.Log("���� HP�� ó������ 70�� �Ʒ��� �Ǿ� ��彺���� ����Ѵ�.");
            yield return new WaitForSeconds(4f);
        }
        else if (nowHp < 1400f && !p40)
        {
            // Comment : ���� HP�� ó������ 40�� �Ʒ��� �Ǿ� ��彺���� ����Ѵ�."
            p40 = true;
            PatternHeadSpin();
            Debug.Log("���� HP�� ó������ 40�� �Ʒ��� �Ǿ� ��彺���� ����Ѵ�.");
            yield return new WaitForSeconds(4f);
        }
        else if (0<nowHp&&nowHp < 350f && !p10)
        {
            // Comment : ���� HP�� ó������ 10�� �Ʒ��� �Ǿ� ��彺���� ����Ѵ�.
            p10 = true;
            PatternHeadSpin();
            Debug.Log("���� HP�� ó������ 10�� �Ʒ��� �Ǿ� ��彺���� ����Ѵ�.");
            yield return new WaitForSeconds(4f);
        }
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

    void BossMove()
    {
        Debug.Log("�̵�");
        float xMoveDirection = Time.deltaTime;
        float xMax = 8f;
        float xMin = -8f;
        float xNow = 0f;

        xNow += xMoveDirection;
        monster.transform.position = new Vector3(xNow, monster.transform.position.y, monster.transform.position.z);

        if (xNow >= xMax)
        {
            xMoveDirection = -Time.deltaTime;
            //monster.transform.position = Vector3.MoveTowards(monster)
        }
        else if(xNow <= xMin)
        {
            xMoveDirection = Time.deltaTime;
        }
    }
}

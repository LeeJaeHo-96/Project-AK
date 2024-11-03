using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class HYJ_Boss_Stage1 : MonoBehaviour
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
    [SerializeField] float xNow = 0;
    [SerializeField] float xMoveDirection = 0.1f;
    private bool isSiuu = false;
    bool isPattern = false;

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
            StartCoroutine(BossBattleStart());
        }
        else if (firstBattleEnd && pFirst && pSecond)
        {
            StartCoroutine(BossAI());
        }
    }

    // Comment : ��彺�� ����
    private void PatternHeadSpin()
    {
        Debug.Log("��彺��");
        monsterHpAtkPower = 4000f;
        monsterShieldAtkPower = 5f;
        animator.SetTrigger("HeadSpin");
        nowAttack = true;


    }

    // Comment : �극��ũ�� ����
    private void PatternBreakDance()
    {
        Debug.Log("�극��ũ��");
        monsterHpAtkPower = 1000;
        monsterShieldAtkPower = 3;
        animator.SetTrigger("BreakDance");
        isSiuu =true;
        nowAttack = true;

    }

    // Comment : ������� ����
    private void PatternSiiuuuu()
    {
        isSiuu = true;
        Debug.Log("�����Ӵ�");
        monsterHpAtkPower = 3000f;
        monsterShieldAtkPower = 1f;
        animator.SetTrigger("Siuu");
        nowAttack = true;
        Vector3 beforeBossPos = monster.transform.position;
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
            LSY_SceneManager.Instance.GameClear();
        }
    }

    // Comment : ���� ���� ���� 
    IEnumerator BossBattleStart()
    {
        if (!pFirst)
        {
            // Comment : ù �������� �극��ũ ���� �Ѵ�.
            Debug.Log("���� �н��� ù �������� �극��ũ ���� �Ѵ�.");
            PatternBreakDance();
            pFirst = true;
            yield return new WaitForSeconds(4f);
        }
        if (pFirst && !pSecond)
        {
            // Comment : �극��ũ ���� �� ��, ������� ������ �Ѵ�.
            Debug.Log("���� �н��� �ι�° �������� ������� ������ �Ѵ�.");
            PatternSiiuuuu();
            pSecond = true;
            yield return new WaitForSeconds(4f);
            firstBattleEnd = true;
        }
    }


    // Comment : ������ ���� AI
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

    // Comment : ���� �̵�
    void BossMove()
    {
        float xMax = 8f;
        float xMin = -8f;
        

        xNow += xMoveDirection;
        monster.transform.position = new Vector3(xNow, monster.transform.position.y, monster.transform.position.z);

        if (xNow >= xMax)
        {
            Debug.Log("���� ��ȯ");
            Debug.Log(xMoveDirection);
            xMoveDirection = -Time.deltaTime * 3f;
        }
        else if(xNow <= xMin)
        {
            xMoveDirection = Time.deltaTime * 3f;
        }
    }

    
}

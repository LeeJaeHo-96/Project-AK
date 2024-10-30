using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

public class HYJ_Enemy : MonoBehaviour
{
    [Header("�÷��̾�")]
    [SerializeField] GameObject player;

    [Header("���� ����")]
    [SerializeField] GameObject monster;
    //[SerializeField] GameObject damageText;
    //[SerializeField] Transform damagePos;
    [SerializeField] public MonsterType monsterType;
    [SerializeField] public MonsterAttackType monsterAttackType;
    [SerializeField] public float monsterShieldAtkPower;
    [SerializeField] public float monsterHpAtkPower;
    [SerializeField] public float monsterAttackRange;
    [SerializeField] public float score;
    //[SerializeField] public float setBossHp;
    [SerializeField] public float monsterHp;
    [SerializeField] public float monsterMoveSpeed;
    [SerializeField] public float playerDistance;

    [Header("�ִϸ��̼�")]
    [SerializeField] public float aniTime;
    [SerializeField] Animator monsterAnimator;

    [Header("���� �Ҵ� ����")]
    public bool isAttack;
    public bool nowAttack;
    public bool isDie;
    
    public UnityEvent<Collider> OnEnemyDied;

    //------------------------���� ����---------------------------//
    [Header("���� ����")]
    public float playerAttackPower=20;
    public bool isShield;

    [SerializeField] public bool hitFlag;
    public bool HitFlag { get { return hitFlag; } set { hitFlag = value; } }

    Coroutine hitFlagCoroutine;
    WaitForSeconds hitFlagWaitForSeconds = new WaitForSeconds(0.1f);
    
    public enum MonsterType
    {
        Nomal,
        Elite,
        Boss
    }

    public enum MonsterAttackType
    {
        shortAttackRange,
        longAttackRange
    }

    void Start()
    {
        isAttack = false;
        nowAttack = false;
        isDie = false;
        MonsterTagSet(monsterType);
        //MonsterSetHp();
        MonsterSetAttackRange();
    }

    void Update()
    {
        MonsterDie();
        MonsterMover();
    }

    /*
    // Comment : ����Ÿ�Կ� ���� ������ ü���� �����Ѵ�.
    public void MonsterSetHp()
    {
        if (monsterType == MonsterType.Boss) // Comment : ������ Ÿ���� Boss��� ������ BossHp�� Hp�� �����Ѵ�.
        {
            monsterHp = setBossHp;
        }
        else // Comment : Boss�� �ƴ� Nomal, Elite ���ʹ� Hp�� 100���� �����Ѵ�.
        {
            monsterHp = 100;
        }
    }
    */

    // Comment : ���� ���ݹ����� �����Ѵ�.
    public void MonsterSetAttackRange()
    {
        if(monsterAttackType == MonsterAttackType.shortAttackRange) // Comment : �ٰŸ� Ÿ���̶�� ���ݹ����� 3���� �����Ѵ�.
        {
            monsterAttackRange = 3;
        }
        else if(monsterAttackType == MonsterAttackType.longAttackRange) // Commnet : ���Ÿ� Ÿ���̶�� ���� ������ 7�� �����Ѵ�.
        { 
            monsterAttackRange = 7;
        }
    }

    // Comment : Player �±��� ������Ʈ�� ã�� �ش� ������Ʈ�� Monster�� �̵��Ѵ�.
    public void MonsterMover()
    {
        if (player != null && monsterHp>0)
        {
            playerDistance = Vector3.Distance(monster.transform.position, player.transform.position); // Comment : �÷��̾�� ������ �Ÿ�

            if (playerDistance > monsterAttackRange) // Comment : �÷��̾���� �Ÿ��� ���ݹ��� ���� ��
            {
                Debug.Log("�̵� ��");
                monsterAnimator.SetBool("Run Forward",true);
                monster.transform.position = Vector3.MoveTowards(monster.transform.position, new Vector3(player.transform.position.x,0,player.transform.position.z), monsterMoveSpeed/50);
                monster.transform.LookAt(new Vector3(player.transform.position.x,0,player.transform.position.z)); // Comment : ���� 
            }
            else if(playerDistance <= monsterAttackRange && isAttack==false && monsterHp > 0) //Comment : �÷��̾ ������ ���ݹ����� ������ ��
            {
                monsterAnimator.SetBool("Run Forward", false);
                isAttack = true;
                StartCoroutine(MonsterAttackCo());
            }
            else
            {

            }
        }
    }

    // Comment : ��Ʈ���� ���͸� �̿��Ͽ� �Ѿ˰��� �浹 ���θ� Ȯ��, �浹 ��, ĳ������ ���ݷ� or ������ ���ݷ��� �Ϸ�Ǹ� ���� �ǰ� �Լ��� �����Ų��.
    public void MonsterTakeDamageCalculation(float damage)
    {
        // float -> 
        // Comment : ����� ���Ƿ� playerAttackPower ������ Ȱ���Ͽ� �ۼ��ߴ�.
        if (monsterType == MonsterType.Nomal)
        {
            monsterHp -= damage;
        }
        else if(monsterType == MonsterType.Elite)
        {
            if(playerAttackPower-15 > 0)
            {
                monsterHp -= damage - 15;
            }
        }
    }

    public void StartHitFlagCoroutine()
    {
        if(hitFlagCoroutine != null)
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

    // Comment : ���� ���� �ڷ�ƾ
    IEnumerator MonsterAttackCo()
    {
        monsterAnimator.SetTrigger("Attack");
        Debug.Log("���� ����");
        yield return new WaitForSeconds(aniTime);
        nowAttack = true;
        nowAttack = false;        
        yield return new WaitForSeconds(1f);
        isAttack = false;
    }

    // Comment : ���� ���
    public void MonsterDie()
    {
        if(monsterHp <= 0) // Comment : ������ Hp�� 0�� �Ǹ� ���� ������Ʈ�� �����Ѵ�.
        {
            Debug.Log("���� ���");
            monsterAnimator.SetTrigger("Die");
            isDie = true;
            OnEnemyDied?.Invoke(GetComponent<Collider>());
            Destroy(gameObject.GetComponent<BoxCollider>());
            Destroy(gameObject.GetComponent<Rigidbody>());
            Destroy(gameObject,2f);
        }
    }

    // Comment : �� ��� ������ ���� �±� ����
    public void MonsterTagSet(MonsterType monsterType)
    {
        if(monsterType == MonsterType.Nomal)
        {
            gameObject.tag = "Enemy";
        }
        else if (monsterType == MonsterType.Elite)
        {
            gameObject.tag = "EliteEnemy";
        }
        else if(monsterType == MonsterType.Boss)
        {
            gameObject.tag = "Boss";
        }
    }

    // TODO : ���� ��޿� ���� ����Ʈ �����
    public void MonsterEffect()
    {

    }

    // Comment : �ٸ� ������Ʈ�� �浹 ��
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            //MonsterTakeDamageCalculation();
            // TODO : �浹 ������ �ޱ�
            
            //other.transform.position -> �浹 ����
            // TODO : ���� �浹 ������ �Ӹ� / ���� ������� �Ǻ��ϱ�
            // TODO : �����̸� ���, �Ӹ��� ���������� ������ ǥ��
        }
    }
    /*
    public void DamgeText(float damage)
    {
        GameObject text = Instantiate(damageText);
        text.transform.position = damagePos.position;
        text.GetComponent<HYJ_DamageText>().damage = damage;
    }
    */
}
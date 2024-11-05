using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Tilemaps.Tile;

public class HYJ_Enemy : MonoBehaviour
{
    [Header("�÷��̾�")]
    [SerializeField] GameObject player;

    [Header("���� ����")]
    [SerializeField] GameObject monster;
    [SerializeField] public MonsterType monsterType;
    [SerializeField] public MonsterAttackType monsterAttackType;
    [SerializeField] public float monsterShieldAtkPower;
    [SerializeField] public float monsterHpAtkPower;
    [SerializeField] public float monsterAttackRange;
    [SerializeField] public float monsterNowHp;
    [SerializeField] public float monsterSetHp;
    [SerializeField] public float monsterMoveSpeed;
    [SerializeField] public float playerDistance;

    [Header("�ִϸ��̼�")]
    [SerializeField] public float aniTime;
    [SerializeField] Animator monsterAnimator;

    [Header("���� �Ҵ� ����")]
    public bool isAttack;
    public bool nowAttack;
    public bool isDie;


    //public MonsterCountUI hyj_monsterCount;

    [Header("������ �Ŵ���")]
    [SerializeField] GameObject damageManager;

    [Header("��� ����")]
    [SerializeField] AudioClip clip;

    //------------------------���� ����---------------------------//
    [Header("���� ����")]
    [SerializeField] public bool hitFlag;
    public bool HitFlag { get { return hitFlag; } set { hitFlag = value; } }

    Coroutine hitFlagCoroutine;
    WaitForSeconds hitFlagWaitForSeconds = new WaitForSeconds(0.05f);

    [SerializeField] EachTimeLine eachTimeLine;
    [SerializeField] bool isReady;
    [SerializeField] bool isKeyEnemy;
    [SerializeField] MonsterSpecies monsterSpecies;
    public enum MonsterSpecies
    {
        None,
        Wolf,
        Boar,
        Spider,
        Spawn,
        Elite,
        Banshee,
        Raider,
        ArmoredKnight
    }
    public enum MonsterType
    {
        Nomal,
        Elite,
        Boar
    }

    public enum MonsterAttackType
    {
        shortAttackRange,
        longAttackRange
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        damageManager = GameObject.FindGameObjectWithTag("DamagerManager");
        isAttack = false;
        nowAttack = false;
        isDie = false;
        monsterNowHp = monsterSetHp;
        MonsterTagSet(monsterType);
        //MonsterSetHp();
        MonsterSetAttackRange();
        monster.transform.LookAt(new Vector3(player.transform.position.x, 0, player.transform.position.z)); // Comment : ���Ͱ� �÷��̾ �ٶ󺻴�.
    }

    void Update()
    {
        MonsterDie();
        MonsterMover();
    }

    // Comment : ���� ���ݹ����� �����Ѵ�.
    public void MonsterSetAttackRange()
    {
        if (monsterAttackType == MonsterAttackType.shortAttackRange) // Comment : �ٰŸ� Ÿ���̶�� ���ݹ����� 3���� �����Ѵ�.
        {
            monsterAttackRange = 3;
            if (monsterSpecies == MonsterSpecies.ArmoredKnight)
            {
                monsterAttackRange = 6.1f;
            }
        }
        else if (monsterAttackType == MonsterAttackType.longAttackRange) // Commnet : ���Ÿ� Ÿ���̶�� ���� ������ 7�� �����Ѵ�.
        {
            monsterAttackRange = 10000;
        }
    }

    // Comment : Player �±��� ������Ʈ�� ã�� �ش� ������Ʈ�� Monster�� �̵��Ѵ�.
    public void MonsterMover()
    {
        if (isReady)
        {
            return;
        }


            if (player != null && monsterNowHp > 0)
        {
            if (monsterType == MonsterType.Boar)
            {
                monsterAnimator.SetBool("Run Forward", true);
                return;
            }

            playerDistance = Vector3.Distance(new Vector3(monster.transform.position.x, 0, monster.transform.position.z), new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z)); // Comment : �÷��̾�� ������ �Ÿ�(x, z�ุ ���)

            if (playerDistance > monsterAttackRange) // Comment : �÷��̾���� �Ÿ��� ���ݹ��� ���� ��
            {
                Debug.Log("�̵� ��");
                monsterAnimator.SetBool("Run Forward", true);
                monster.transform.position = Vector3.MoveTowards(monster.transform.position, new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z), monsterMoveSpeed / 50);
            }
            else if (playerDistance <= monsterAttackRange && isAttack == false && monsterNowHp > 0) //Comment : �÷��̾ ������ ���ݹ����� ������ ��
            {
                monsterAnimator.SetBool("Run Forward", false);
                isAttack = true;
                AttackCo = StartCoroutine(MonsterAttackCo());
            }
            else
            {

            }
        }
    }
    Coroutine AttackCo;
    // Comment : ��Ʈ���� ���͸� �̿��Ͽ� �Ѿ˰��� �浹 ���θ� Ȯ��, �浹 ��, ĳ������ ���ݷ� or ������ ���ݷ��� �Ϸ�Ǹ� ���� �ǰ� �Լ��� �����Ų��.
    public void MonsterTakeDamageCalculation(float damage)
    {
        monsterNowHp -= damage;
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

    // Comment : ���� ���� �ڷ�ƾ

    IEnumerator MonsterAttackCo()
    {
        monsterAnimator.SetTrigger("Attack");
        Debug.Log("���� ����");
        
        yield return new WaitForSeconds(aniTime);
        isAttack = true;
        nowAttack = true;
        damageManager.GetComponent<LJH_DamageManager>().TakeDamage(this);
        if (monsterAttackType == MonsterAttackType.shortAttackRange)
            yield return new WaitForSeconds(1f);
        else
            yield return new WaitForSeconds(3f);
        isAttack = false;
    }

    // Comment : ���� ���
    public void MonsterDie()
    {
        if (monsterNowHp <= 0 && !isDie) // Comment : ������ Hp�� 0�� �Ǹ� ���� ������Ʈ�� �����Ѵ�.
        {
            if (AttackCo != null)
                StopCoroutine(AttackCo);
            // if (hyj_monsterCount != null)
            // {
            //     if (hyj_monsterCount.Enemies.ContainsKey(this))
            //     {
            //         if (hyj_monsterCount.isEnter[this] == true)
            //         {
            //             ColliderType col = hyj_monsterCount.Enemies[this];
            //             hyj_monsterCount.counters[(int)col]--;
            //         }
            //         hyj_monsterCount.Enemies.Remove(this);
            //     }
            //     if (hyj_monsterCount.isEnter.ContainsKey(this))
            //     {
            //         hyj_monsterCount.isEnter[this] = false;
            //         //this.gameObject.GetComponent<UnitToScreenBoundary>().image.color = Color.white;
            //     }
            // }
            // 

            if (monsterType == MonsterType.Nomal)
            {
               
                ScoreUIManager.Instance.AddScore(100);
            }
            else if (monsterType == MonsterType.Elite)
            {
               ScoreUIManager.Instance.AddScore(500);
            }

            if (eachTimeLine == null && monsterSpecies != MonsterSpecies.Spider && monsterSpecies != MonsterSpecies.Spawn && monsterSpecies != MonsterSpecies.Boar)
            {
                WaveTimeline.Instance.DecreaseWaveCount();
            }
            else
            {
                EnemyDecreaseWaveCount();
            }

            if (clip != null)
            {
                AudioManager.Instance.PlaySE(clip);
            }

            Debug.Log("���� ���");
            isDie = true;
            monsterAnimator.SetTrigger("Die");
            transform.SetParent(null);
            //Destroy(gameObject.GetComponent<SphereCollider>());
            // Destroy(gameObject,2f);

            // ���� ��� �� �����, ������ ����
            WHS_TransparencyController.Instance.StartFadeOut(gameObject, 1);
            WHS_ItemManager.Instance.SpawnItem(gameObject.transform.position, monsterSpecies.ToString());

        }
    }

    // Comment : �� ��� ������ ���� �±� ����
    public void MonsterTagSet(MonsterType monsterType)
    {
        if (monsterType == MonsterType.Nomal)
        {
            gameObject.tag = "Enemy";
        }
        else if (monsterType == MonsterType.Elite)
        {
            gameObject.tag = "EliteEnemy";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (monsterType == MonsterType.Boar)
            {
                damageManager.GetComponent<LJH_DamageManager>().TakeDamage(this);
            }
            //MonsterTakeDamageCalculation();
            // TODO : �浹 ������ �ޱ�

            //other.transform.position -> �浹 ����
            // TODO : ���� �浹 ������ �Ӹ� / ���� ������� �Ǻ��ϱ�
            // TODO : �����̸� ���, �Ӹ��� ���������� ������ ǥ��
        }
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
    }*/
    


    public void EnemyDecreaseWaveCount()
    {
        if (eachTimeLine != null)
        {
            eachTimeLine.DecreaseWaveCount(isKeyEnemy);

        }
    }

    public void ReadyEnemy()
    {
        isReady = false;
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LSY_Enemy : MonoBehaviour
{
    [Header("�÷��̾�")]
    [SerializeField] GameObject lsy_player;

    [Header("���� ����")]
    [SerializeField] GameObject lsy_monster;
    //[SerializeField] GameObject damageText;
    //[SerializeField] Transform damagePos;
    [SerializeField] public lsy_MonsterType lsy_monsterType;
    [SerializeField] public lsy_MonsterAttackType lsy_monsterAttackType;
    [SerializeField] public float lsy_monsterShieldAtkPower;
    [SerializeField] public float lsy_monsterHpAtkPower;
    [SerializeField] public float lsy_monsterAttackRange;
    [SerializeField] public float lsy_score;
    //[SerializeField] public float setBossHp;
    [SerializeField] public float lsy_monsterHp;
    [SerializeField] public float lsy_monsterMoveSpeed;
    [SerializeField] public float lsy_playerDistance;

    [Header("�ִϸ��̼�")]
    //[SerializeField] public float aniTime;
    //[SerializeField] Animator monsterAnimator;

    [Header("���� �Ҵ� ����")]
    public bool lsy_isAttack;
    public bool lsy_nowAttack;
    public bool lsy_isDie;


    public UnityEvent<Collider> lsy_OnEnemyDied; // �����ؾ���

    //------------------------���� ����---------------------------//
    [Header("���� ����")]
    public float lsy_playerAttackPower = 20;
    public bool lsy_isShield;

    [SerializeField] public bool lsy_hitFlag;
    public bool lsy_HitFlag { get { return lsy_hitFlag; } set { lsy_hitFlag = value; } }

    Coroutine lsy_hitFlagCoroutine;
    WaitForSeconds lsy_hitFlagWaitForSeconds = new WaitForSeconds(0.1f);

    //---------------lsy
    public MonsterCountUI lsy_monsterCount;

    public enum lsy_MonsterType
    {
        Nomal,
        Elite,
        Boss
    }

    public enum lsy_MonsterAttackType
    {
        shortAttackRange,
        longAttackRange
    }

    void Start()
    {
        lsy_player = GameObject.FindGameObjectWithTag("Player");
        lsy_isAttack = false;
        lsy_nowAttack = false;
        lsy_isDie = false;
        lsy_MonsterTagSet(lsy_monsterType);
        //MonsterSetHp();
        lsy_MonsterSetAttackRange();
        //StartCoroutine(MonsterDied());
    }

    //IEnumerator MonsterDied()
    //{
    //    yield return new WaitForSeconds(3f);
    //   //monsterHp = 0;
    //}

    void Update()
    {
        if (lsy_isDie == false)
        {
            lsy_MonsterDie();
        }
        //MonsterMover();
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
    public void lsy_MonsterSetAttackRange()
    {
        if (lsy_monsterAttackType == lsy_MonsterAttackType.shortAttackRange) // Comment : �ٰŸ� Ÿ���̶�� ���ݹ����� 3���� �����Ѵ�.
        {
            lsy_monsterAttackRange = 3;
        }
        else if (lsy_monsterAttackType == lsy_MonsterAttackType.longAttackRange) // Commnet : ���Ÿ� Ÿ���̶�� ���� ������ 7�� �����Ѵ�.
        {
            lsy_monsterAttackRange = 7;
        }
    }

    // Comment : Player �±��� ������Ʈ�� ã�� �ش� ������Ʈ�� Monster�� �̵��Ѵ�.
    //public void MonsterMover()
    //{
    //    if (player != null && monsterHp > 0)
    //    {
    //        playerDistance = Vector3.Distance(monster.transform.position, player.transform.position); // Comment : �÷��̾�� ������ �Ÿ�

    //        if (playerDistance > monsterAttackRange) // Comment : �÷��̾���� �Ÿ��� ���ݹ��� ���� ��
    //        {
    //            Debug.Log("�̵� ��");
    //            monsterAnimator.SetBool("Run Forward", true);
    //            monster.transform.position = Vector3.MoveTowards(monster.transform.position, new Vector3(player.transform.position.x, 0, player.transform.position.z), monsterMoveSpeed / 50);
    //            monster.transform.LookAt(new Vector3(player.transform.position.x, 0, player.transform.position.z)); // Comment : ���� 
    //        }
    //        else if (playerDistance <= monsterAttackRange && isAttack == false && monsterHp > 0) //Comment : �÷��̾ ������ ���ݹ����� ������ ��
    //        {
    //            monsterAnimator.SetBool("Run Forward", false);
    //            isAttack = true;
    //            StartCoroutine(MonsterAttackCo());
    //        }
    //        else
    //        {

    //        }
    //    }
    //}

    // Comment : ��Ʈ���� ���͸� �̿��Ͽ� �Ѿ˰��� �浹 ���θ� Ȯ��, �浹 ��, ĳ������ ���ݷ� or ������ ���ݷ��� �Ϸ�Ǹ� ���� �ǰ� �Լ��� �����Ų��.
    public void lsy_MonsterTakeDamageCalculation(float damage)
    {
        // float -> 
        // Comment : ����� ���Ƿ� playerAttackPower ������ Ȱ���Ͽ� �ۼ��ߴ�.
        if (lsy_monsterType == lsy_MonsterType.Nomal)
        {
            lsy_monsterHp -= damage;
        }
        else if (lsy_monsterType == lsy_MonsterType.Elite)
        {
            if (lsy_playerAttackPower - 15 > 0)
            {
                lsy_monsterHp -= damage - 15;
            }
        }
    }

    public void lsy_StartHitFlagCoroutine()
    {
        if (lsy_hitFlagCoroutine != null)
        {
            StopCoroutine(lsy_hitFlagCoroutine);
        }
        lsy_hitFlagCoroutine = StartCoroutine(lsy_HitFlagCoroutine());
    }

    IEnumerator lsy_HitFlagCoroutine()
    {
        yield return lsy_hitFlagWaitForSeconds;
        lsy_hitFlag = false;
    }

    // Comment : ���� ���� �ڷ�ƾ
    IEnumerator lsy_MonsterAttackCo()
    {
        //monsterAnimator.SetTrigger("Attack");
        Debug.Log("���� ����");
        //yield return new WaitForSeconds(aniTime);
        lsy_nowAttack = true;
        lsy_nowAttack = false;
        yield return new WaitForSeconds(1f);
        lsy_isAttack = false;
    }

    // Comment : ���� ���
    public void lsy_MonsterDie()
    {
        if (lsy_monsterHp <= 0) // Comment : ������ Hp�� 0�� �Ǹ� ���� ������Ʈ�� �����Ѵ�.
        {
            //----------------------------------lsy
            if (lsy_monsterCount != null)
            {
                if (lsy_monsterCount.Enemies.ContainsKey(this))
                {
                    if (lsy_monsterCount.isEnter[this] == true)
                    {
                        ColliderType col = lsy_monsterCount.Enemies[this];
                        lsy_monsterCount.counters[(int)col]--;
                    }
                    lsy_monsterCount.Enemies.Remove(this);
                }

                if (lsy_monsterCount.isEnter.ContainsKey(this))
                {
                    lsy_monsterCount.isEnter[this] = false;
                }

            }

            Debug.Log("���� ���");
            //monsterAnimator.SetTrigger("Die");
            lsy_isDie = true;
            lsy_OnEnemyDied?.Invoke(GetComponent<Collider>());
            Destroy(gameObject.GetComponent<BoxCollider>());
            Destroy(gameObject.GetComponent<Rigidbody>());
            Destroy(gameObject, 2f);
        }
    }

    // Comment : �� ��� ������ ���� �±� ����
    public void lsy_MonsterTagSet(lsy_MonsterType monsterType)
    {
        if (monsterType == lsy_MonsterType.Nomal)
        {
            gameObject.tag = "Enemy";
        }
        else if (monsterType == lsy_MonsterType.Elite)
        {
            gameObject.tag = "EliteEnemy";
        }
        else if (monsterType == lsy_MonsterType.Boss)
        {
            gameObject.tag = "Boss";
        }
    }

    // TODO : ���� ��޿� ���� ����Ʈ �����
    public void lsy_MonsterEffect()
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

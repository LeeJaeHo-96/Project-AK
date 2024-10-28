using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class HYJ_Eneme : MonoBehaviour
{
    [SerializeField] GameObject monster;
    [SerializeField] MonsterType monsterType;
    [SerializeField] MonsterAttackType monsterAttackType;
    [SerializeField] float monsterAttackPower;
    [SerializeField] float monsterAttackRange;
    [SerializeField] float setBossHp;


    [SerializeField] float monsterHp;
    [SerializeField] float monsterMoveSpeed;
    [SerializeField] float playerDistance;

    [SerializeField] Animator monsterAnimator;

    //------------------------���� ����---------------------------//
    public float playerAttackPower=20;

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
        MonsterSetHp();
        MonsterSetAttackRange();
    }

    void Update()
    {
        MonsterDie();
        MonsterMover();
    }

    // Comment : ����Ÿ�Կ� ���� ������ ü���� �����Ѵ�.
    private void MonsterSetHp()
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

    // Comment : ���� ���ݹ����� �����Ѵ�.
    private void MonsterSetAttackRange()
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
    private void MonsterMover()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // TODO : �±�Ž���� ���̾�� �����Ű��!
        if (player != null)
        {
            playerDistance = Vector3.Distance(monster.transform.position, player.transform.position); // Comment : �÷��̾�� ������ �Ÿ�

            if (playerDistance > monsterAttackRange) // Comment : �÷��̾���� �Ÿ��� ���ݹ��� ���� ��
            {
                Debug.Log("�̵� ��");
                monsterAnimator.SetBool("Run Forward",true);
                monster.transform.position = Vector3.MoveTowards(monster.transform.position, new Vector3(player.transform.position.x,0,player.transform.position.z), monsterMoveSpeed/50);
                monster.transform.LookAt(new Vector3(player.transform.position.x,0,player.transform.position.z)); // Comment : ���� 
            }
            else // Comment : �÷��̾ ������ ���ݹ����� ������ ��
            {
                MonsterAttack();
            }
        }
    }

    // Comment : ��Ʈ���� ���͸� �̿��Ͽ� �Ѿ˰��� �浹 ���θ� Ȯ��, �浹 ��, ĳ������ ���ݷ� or ������ ���ݷ��� �Ϸ�Ǹ� ���� �ǰ� �Լ��� �����Ų��.
    private void MonsterTakeMamage()
    {
        // TODO : ������ ���ݷ°� �Ѿ� ������ �Ϸ�Ǹ� ���� �ǰ� �Լ��� �����Ų��.
        // Comment : ����� ���Ƿ� playerAttackPower ������ Ȱ���Ͽ� �ۼ��ߴ�.
        if (monsterType == MonsterType.Nomal)
        {
            monsterHp -= playerAttackPower;
        }
        else if(monsterType == MonsterType.Elite)
        {
            if(playerAttackPower-15 > 0)
            {
                monsterHp -= playerAttackPower - 15;
            }
        }
    }

    //Comment : ���Ͱ� �÷��̾ ���� ���� �Լ�
    private void MonsterAttack()
    {
        if (monsterHp > 0)
        {
            monsterAnimator.SetTrigger("Pound Attack");
            Debug.Log("���Ͱ� ����!");
            // TODO : ���� ������ �����

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet")){
            MonsterTakeMamage();
            
        }
    }

    // Comment : ���� ���
    private void MonsterDie()
    {
        if(monsterHp <= 0) // Comment : ������ Hp�� 0�� �Ǹ� ���� ������Ʈ�� �����Ѵ�.
        {
            Debug.Log("���� ���");
            //TODO : ���� Die �ִϸ��̼� ����
            monsterAnimator.SetTrigger("Die");
            Destroy(gameObject.GetComponent<BoxCollider>());
            Destroy(gameObject.GetComponent<Rigidbody>());
            Destroy(gameObject,2f);
        }
    }
}

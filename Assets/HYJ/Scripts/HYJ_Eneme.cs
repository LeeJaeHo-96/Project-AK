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
    [SerializeField] float playerDistance;

    //---------------------------------------------------//

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
        MonsterMover();
    }

    // Comment : ����Ÿ�Կ� ���� ������ ü���� �����Ѵ�.
    private void MonsterSetHp()
    {
        if (monsterType == MonsterType.Boss)
        {
            monsterHp = setBossHp;
        }
        else
        {
            monsterHp = 100;
        }
    }

    private void MonsterSetAttackRange()
    {
        if(monsterAttackType == MonsterAttackType.shortAttackRange)
        {
            monsterAttackRange = 3;
        }
        else{
            monsterAttackRange = 7;
        }
    }

    // Comment : Player �±��� ������Ʈ�� ã�� �ش� ������Ʈ�� Monster�� �̵��Ѵ�.
    private void MonsterMover()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // �±׸� ���̾�� �����Ű��!
        if (player != null)
        {
            playerDistance = Vector3.Distance(monster.transform.position, player.transform.position);
            if (playerDistance > monsterAttackRange)
            {
                Debug.Log(playerDistance);
            }
            else
            {
                MonsterAttack();
                //StartCoroutine(MonsterAttackCo());
            }
        }
    }

    //Comment : ��Ʈ���� ���͸� �̿��Ͽ� �Ѿ˰��� �浹 ���θ� Ȯ��, �浹 ��, ĳ������ ���ݷ� or ������ ���ݷ��� �Ϸ�Ǹ� ���� �ǰ� �Լ��� �����Ų��.
    private void MonsterHit()
    {
        // TODO : ĳ����or ������ ���ݷ°� �Ѿ� ������ �Ϸ�Ǹ� ���� �ǰ� �Լ��� �����Ų��.
        // ����� ���Ƿ� playerAttackPower ������ Ȱ���Ͽ� �ۼ��ߴ�.
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

    private void MonsterAttack()// �ڷ�ƾ����?
    {
        Debug.Log("���Ͱ� ����!");
    }

    IEnumerator MonsterAttackCo()
    {
        yield return new WaitForSeconds(10f);
        Debug.Log("���Ͱ� ���� Co!");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet")){ // �±׸� ���̾�� �����Ű��!
            MonsterHit();
            //Destroy(other.gameObject);
        }
    }
}

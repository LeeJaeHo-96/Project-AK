using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LJH_MonsterSearcher : MonoBehaviour
{
    [Header("��ũ��Ʈ")]
    [Header("���� ���� ��ũ��Ʈ")]
    [SerializeField] HYJ_Enemy monsterStats;
    [Header("������ ��� ��ũ��Ʈ")]
    [SerializeField] LJH_DamageManager searchMonster;

    [SerializeField] bool isNowAttack;

    
    private void OnTriggerEnter(Collider other)
    {
        // Comment: Ʈ���ſ��Ϳ� ������ ohter�� �����϶�,
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Comment: ���� ���� ������ �ش� ���� ������Ʈ�� �־���
            monsterStats = other.GetComponentInParent<HYJ_Enemy>();
        }
        // Comment: ���� �ȵ��� ���� Ȯ�� ���ð� �ؾ���, �� �� Ȯ�� ŭ
    }

    private void OnTriggerStay(Collider other)
    {
        // Comment: ���Ͱ� ������ �ߵ����� ��,
        if (other.gameObject.CompareTag("Enemy"))
        {
            isNowAttack = monsterStats.nowAttack;
            if (isNowAttack)
            {
                // Comment: TakeDamage�� ���� ���� ������ �ִ� ������ �̾Ƽ� �־���
                searchMonster.TakeDamage(monsterStats);
                monsterStats.nowAttack = false;
            }
        }
    }
}

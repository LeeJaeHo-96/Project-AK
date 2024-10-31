using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LJH_ShieldRecover : MonoBehaviour
{
    [Header("������Ʈ")]
    [Header("���� ������Ʈ")]
    [SerializeField] GameObject shield;

    [Header("��ũ��Ʈ")]
    [Header("UIManager ��ũ��Ʈ")]
    [SerializeField] LJH_UIManager test;

    [Header("����")]
    [Header("������ ȸ����(�ʴ�)")]
    [SerializeField] const float REPAIR = 1;
    [Header("���� �ִ� ������")]
    [SerializeField] const float MAXDURABILITY = 5;
    [Header("���� ������")]
    [SerializeField] public float durability;
    [Header("���� �ı�/���ı� ����")]
    [SerializeField] public bool isBreaked;
    [Header("���� ������ ����")]
    [SerializeField] public bool isRecover;
    [Header("���� Ȱ��ȭ ����")]
    [SerializeField] public bool isShield;

    void Awake()
    {
        gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        durability = shield.GetComponent<LJH_Shield>().durability;
        isBreaked = shield.GetComponent<LJH_Shield>().isBreaked;
        isRecover = shield.GetComponent<LJH_Shield>().isRecover;
        isShield = shield.GetComponent<LJH_Shield>().isShield;

        if (durability != MAXDURABILITY)
        {
            Coroutine recovery = StartCoroutine(RecoveryShield());

            if (!isRecover)
            {
                StopCoroutine(recovery);
            }
        }
    }

    

    // Comment: ���� ���� �ڷ�ƾ
    // ToDo: ������ ȸ�� �ð� �����ؾ���
    IEnumerator RecoveryShield()
    {
        yield return new WaitForSecondsRealtime(1f);

        while (true)
        {
        yield return new WaitForSecondsRealtime(0.5f);
            durability += REPAIR;
            test.UpdateShieldUI(durability); //�̰���
            if (durability == MAXDURABILITY)
            {
                isRecover = false;
                isBreaked = false;

                shield.GetComponent<LJH_Shield>().isRecover = isRecover;
                shield.GetComponent<LJH_Shield>().isBreaked = isBreaked;
                break;
            }
        }
    }
}

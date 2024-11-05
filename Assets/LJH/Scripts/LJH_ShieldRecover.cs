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
    [Header("������ �Ŵ���")]
    [SerializeField] GameObject damageManager;

    [Header("��ũ��Ʈ")]
    [Header("UIManager ��ũ��Ʈ")]
    [SerializeField] LJH_UIManager uiManager;

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

    [Header("�ڷ�ƾ")]
    private Coroutine recovery;

    void Start()
    {
        gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        durability = damageManager.GetComponent<LJH_DamageManager>().durability;
        isBreaked = shield.GetComponent<LJH_Shield>().isBreaked;
        isRecover = shield.GetComponent<LJH_Shield>().isRecover;
        isShield = shield.GetComponent<LJH_Shield>().isShield;

        if (durability != MAXDURABILITY)
        {
            recovery = StartCoroutine(RecoveryShield());
        }

        if (durability < 0)
        {
            durability = 0;
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
            uiManager.UpdateShieldUI(durability);
            if (durability == MAXDURABILITY)
            {
                isRecover = false;
                isBreaked = false;

                shield.GetComponent<LJH_Shield>().durability = durability;
                damageManager.GetComponent<LJH_DamageManager>().durability = durability;
                shield.GetComponent<LJH_Shield>().isRecover = isRecover;
                shield.GetComponent<LJH_Shield>().isBreaked = isBreaked;
                StopCoroutine(recovery);
                break;
            }
        }
    }
}

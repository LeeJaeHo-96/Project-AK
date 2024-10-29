using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LJH_ShieldRecover : MonoBehaviour
{
    [Header("������Ʈ")]
    [SerializeField] GameObject shield;

    [SerializeField] LJHTest test;

    [Header("������ ȸ����(�ʴ�)")]
    [SerializeField] const float REPAIR = 1;

    [Header("����")]
    [SerializeField] const float MAXDURABILITY = 5;
    [SerializeField] public float durability;
    [SerializeField] public bool isBreaked;
    [SerializeField] public bool isRecover;
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
        
        Debug.Log("�ǵ� ȸ�� ����");
        yield return new WaitForSecondsRealtime(1f);

        while (true)
        {
        yield return new WaitForSecondsRealtime(0.5f);
            Debug.Log("������ 1 ȸ��");
            durability += REPAIR;
            test.UpdateShieldUI(durability); //�̰���
            if (durability == MAXDURABILITY)
            {
                Debug.Log("ȸ������");
                isRecover = false;
                isBreaked = false;

                shield.GetComponent<LJH_Shield>().isRecover = isRecover;
                shield.GetComponent<LJH_Shield>().isBreaked = isBreaked;
                break;
            }
        }
    }

    


    
}

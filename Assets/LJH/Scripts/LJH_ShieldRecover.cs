using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LJH_ShieldRecover : MonoBehaviour
{
    [Header("������Ʈ")]
    [SerializeField] GameObject shield;

    [Header("������ ȸ����(�ʴ�)")]
    [SerializeField] const float REPAIR = 1;

    [Header("����")]
    [SerializeField] const float MAXDURABILITY = 5;
    [SerializeField] public float durability;
    [SerializeField] public bool isBreaked;
    [SerializeField] public bool isRecover;
    [SerializeField] public bool isShield;

    int loopNum = 0;                            // ToDo: ���ѷ��� üŷ�� �������
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

        Coroutine recovery = StartCoroutine(RecoveryShield());

            if (!isRecover)
            {
                StopCoroutine(recovery);
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
            if (loopNum++ > 10000)
                throw new Exception("��������");
        

        yield return new WaitForSecondsRealtime(0.5f);
            Debug.Log("������ 1 ȸ��");
            durability += REPAIR;
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

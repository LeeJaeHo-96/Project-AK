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
    [SerializeField] float durability;
    [SerializeField] bool isBreaked;
    [SerializeField] bool isRecover;
    [SerializeField] bool isShield;

    [SerializeField] bool coCheckA;             // Comment: �ڷ�ƾ ���� ���� ������ bool ����
    [SerializeField] bool coCheckB;
    [SerializeField] bool coCheckC;
    [SerializeField] bool coCheckD;

    int loopNum = 0;                            // ToDo: ���ѷ��� üŷ�� �������
    void Start()
    {
        coCheckA = false;                       // Comment: �ڷ�ƾ ���� ���� ������ bool ���� False�� ��ŸƮ
        coCheckB = false;
        coCheckC = false;
        coCheckD = false;
    }

    private void Update()
    {
       durability = shield.GetComponent<LJH_Shield>().durability;
       isBreaked = shield.GetComponent<LJH_Shield>().isBreaked;
       isRecover = shield.GetComponent<LJH_Shield>().isRecover;
       isShield = shield.GetComponent<LJH_Shield>().isShield;

        if (!isShield)
        {


            
            
        }
    }

    // Comment: ���� ���� �ڷ�ƾ
    // ToDo: ������ ȸ�� �ð� �����ؾ���
    IEnumerator RecoveryShield()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Debug.Log("�ǵ� ȸ�� ����");
        coCheckB = true;
        yield return new WaitForSecondsRealtime(1f);
        durability += REPAIR;

    }

    // Comment: ���� �ı� ��ٿ� �ڷ�ƾ
    // IEnumerator ShieldCoolDown()
    // {
    //     coCheckA = true;
    //     Debug.Log("���� ��Ȱ��ȭ ī��Ʈ");
    //     // 2�ʰ� ���� Ȱ��ȭ �Ұ�
    //     yield return new WaitForSecondsRealtime(2.0f);
    //
    //     
    //     // Comment: 2�� �� RecoveryBreakedShield �ڷ�ƾ ����
    //     Coroutine recoveryB = StartCoroutine(RecoveryBreakedShield());        //Comment: ��Ȯ ���濡 ���� �ʿ� ������ �ּ�ó��
    //     
    //     if (durability == MAXDURABILITY)
    //     {
    //         StopCoroutine(recoveryB);
    //     }
    // }

    // Comment: �ı��� ���� ���� �ڷ�ƾ
    // IEnumerator RecoveryBreakedShield()
    // {
    //     Debug.Log("���� ���� ����");
    //     // ���� Ȱ��ȭ �Ұ� + 3�� �� ���� ���� �Ϸ�
    //     yield return new WaitForSecondsRealtime(3.0f);
    //     isBreaked = false;
    //     Debug.Log("���� ���� �Ϸ�");
    //     durability = 5;
    //     Debug.Log(shield.GetComponent<LJH_Shield>().durability);              //Comment: ��Ȯ ���濡 ���� �ʿ� ������ �ּ�ó��
    //     
    // }

}

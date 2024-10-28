using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LJH_ShieldRecover : MonoBehaviour
{
    [Header("������Ʈ")]
    [SerializeField] GameObject shield;

    [Header("����")]
    [SerializeField] float durability;
    [SerializeField] bool isBreaked;
    [SerializeField] bool isRecover;
    void Start()
    {
        //gameObject.SetActive(false); Ȱ��ȭ ���� ������ ���� ���� ����
    }

    private void Update()
    {
       durability = shield.GetComponent<LJH_Shield>().durability;
       isBreaked = shield.GetComponent<LJH_Shield>().isBreaked;
       isRecover = shield.GetComponent <LJH_Shield>().isRecover;

        if (isRecover)
        {
            Debug.Log("���帮Ŀ������");
            //durability = shield.GetComponent<LJH_Shield>().durability;
            //isBreaked = shield.GetComponent<LJH_Shield>().isBreaked;

            // Comment: ������ �ı� ������ ��, ���� �ı� ��ٿ� �ڷ�ƾ ����
            if (isBreaked)
            {
                Coroutine breaked = StartCoroutine(ShieldCoolDown());
                if (!isBreaked)
                {
                    Debug.Log("������ٿ��ڷ�ƾ����");
                    StopCoroutine(breaked);
                }
            }

            // Comment: ������ �ı� ���°� �ƴ� ��, ���� ���� �ڷ�ƾ ����
            else if (!isBreaked)
            {
                while (durability < 5)
                {
                    Coroutine recovery = StartCoroutine(RecoveryShield());
            
                    if (durability >= 5)
                    {
                        StopCoroutine(recovery);
                    }
                }
            }
        }
    }
    

    

    // Comment: ���� �ı� ��ٿ� �ڷ�ƾ
    IEnumerator ShieldCoolDown()
    {
        Debug.Log("���� ��Ȱ��ȭ ī��Ʈ");
        // 2�ʰ� ���� Ȱ��ȭ �Ұ�
        yield return new WaitForSecondsRealtime(2.0f);

        
        // Comment: 2�� �� RecoveryBreakedShield �ڷ�ƾ ����
        Coroutine recovery = StartCoroutine(RecoveryBreakedShield());
        
        if (durability >= 5)
        {
            StopCoroutine(recovery);
        }
    }

    // Comment: �ı��� ���� ���� �ڷ�ƾ
    IEnumerator RecoveryBreakedShield()
    {
        Debug.Log("���� ���� ����");
        // ���� Ȱ��ȭ �Ұ� + 3�� �� ���� ���� �Ϸ�
        yield return new WaitForSecondsRealtime(3.0f);
        isBreaked = false;
        Debug.Log("���� ���� �Ϸ�");
        durability = 5;
        Debug.Log(shield.GetComponent<LJH_Shield>().durability);
        
    }

    // Comment: ���� ���� �ڷ�ƾ
    // ToDo: ������ ȸ�� �ð� �����ؾ���
    IEnumerator RecoveryShield()
    {
        Debug.Log("�ǵ� ȸ�� ����(���ı�)");
        yield return new WaitForSecondsRealtime(1f);
        durability += 1;

    }
}

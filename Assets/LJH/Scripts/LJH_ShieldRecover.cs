using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LJH_ShieldRecover : MonoBehaviour
{
    [Header("������Ʈ")]
    [SerializeField] GameObject shield;

    [Header("����")]
    float durability;
    bool isBreaked;
    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        // Comment: ���� �ı� �ڷ�ƾ ����
        Coroutine braked = StartCoroutine(ShieldCoolDown());
    }

    private void OnEnable()
    {
        durability = shield.GetComponent<LJH_Shield>().durability;
        isBreaked = shield.GetComponent<LJH_Shield>().isBreaked;
    }

    IEnumerator ShieldCoolDown()
    {
        Debug.Log("���� ��Ȱ��ȭ ī��Ʈ");
        // 2�ʰ� ���� Ȱ��ȭ �Ұ�
        yield return new WaitForSeconds(2.0f);


        // 2�� �� RecoveryShield �ڷ�ƾ ����
        Coroutine recovery = StartCoroutine(RecoveryBreakedShield());

        //ToDo : �ڷ�ƾ �����ؾ���
    }

    IEnumerator RecoveryBreakedShield()
    {
        Debug.Log("���� ���� ����");
        // ���� Ȱ��ȭ �Ұ� + 3�� �� ���� ���� �Ϸ�
        yield return new WaitForSeconds(3.0f);
        isBreaked = false;
        Debug.Log("���� ���� �Ϸ�");
        durability = 5;
        Debug.Log(shield.GetComponent<LJH_Shield>().durability);
        
        //ToDo : �ڷ�ƾ �����ؾ���
        
    }
}

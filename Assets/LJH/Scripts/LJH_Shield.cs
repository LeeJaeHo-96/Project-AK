using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class LJH_Shield : MonoBehaviour
{
    [Header("������Ʈ")]
    [SerializeField] GameObject shieldRecover;

    [Header("�÷��̾� ��ġ")]
    [SerializeField] GameObject playerPos;

    [Header("Ű�Է�")]
    [SerializeField] InputActionReference shieldOnOff;
    [SerializeField] InputActionReference damageTest; // �׽�Ʈ ������ ��������

    [Header("����")]
    public bool isShield;          // Comment: ���� Ȱ��ȭ ����
    public bool isBreaked;                // Comment: ���� �ı� ����
    public float durability;       // Comment: ���� ������

    private void Start()
    {
        

        gameObject.SetActive(false);

        isShield = false;
        isBreaked = false;
        durability = 5;
    }
    // Comment: ������ Ȱ��ȭ �� ��
    private void OnEnable()
    {
        // Comment: Ʈ���� ��ư���� ShieldOn ����
        shieldOnOff.action.performed -= ShieldOn;
        // Comment: Ʈ���� ��ư���� ShiledOff �߰�
        shieldOnOff.action.performed += ShieldOff;

        damageTest.action.performed += DamagedShield; // �׽�Ʈ ������ ��������
    }

    // Comment: ������ ��Ȱ��ȭ �� ��
    private void OnDisable()
    {
        // Comment: Ʈ���� ��ư���� ShieldOn �߰�
        shieldOnOff.action.performed += ShieldOn;
        // Comment: Ʈ���� ��ư���� ShiledOff ����
        shieldOnOff.action.performed -= ShieldOff;

        damageTest.action.performed -= DamagedShield; // �׽�Ʈ ������ ��������
    }

    private void Update()
    {
        // Comment: ������ ��ġ�� �÷��̾� ��ġ�� ����ٴϰ�
        transform.position = playerPos.transform.position;

            if (durability < 1)
            {
                BreakedShield();
            }
        

       // if () 
       // {
       //     DamagedShield();
       // }

        
    }


    // Comment: ���� Ȱ��ȭ
    public void ShieldOn(InputAction.CallbackContext obj)
    {
        gameObject.SetActive(true);
        isShield = true;
    }

    // Comment: ���� ��Ȱ��ȭ
    public void ShieldOff(InputAction.CallbackContext obj)
    {
        gameObject.SetActive(false);
        isShield = false;
    }

    // Comment: �ǰݽ� ���� ������ 1 ����
    // ToDo:    ������ Ÿ�� ��Ŀ� ���� ���� ���� �ʿ�
    public void DamagedShield(InputAction.CallbackContext obj)// �μ� ��������
    {
        // ToDo : �ǰݽ� ���� �����ؾ���
        durability -= 1;
        Debug.Log(durability);
    }

    // Comment: ���� �ı�, ������ ��Ȱ��ȭ�Ǹ� isBreaked ������ �� ����
    public void BreakedShield()
    {
        gameObject.SetActive(false);
        shieldRecover.SetActive(true);
        isBreaked = true;
        Debug.Log("������ �ı��Ǿ����ϴ�.");
        
    }

    
}

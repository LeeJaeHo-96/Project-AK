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
    [SerializeField] InputActionReference fire;

    [Header("�����")]
    [SerializeField] AudioSource damaged;
    [SerializeField] AudioSource breaked;

    [Header("����")]
    public bool isShield;          // Comment: ���� Ȱ��ȭ ����   �ʿ������ ���� ����
    public bool isBreaked;         // Comment: ���� �ı� ����
    public bool isRecover;         // Comment: ȸ�� ���� ����
    public float durability;       // Comment: ���� ������

    private void Start()
    {
        
        gameObject.SetActive(false);
        isRecover = false;
        isShield = false;
        isBreaked = false;
        durability = 5;
    }
    // Comment: ������ Ȱ��ȭ �� ��
    private void OnEnable()
    {
        if (!isBreaked)
        {
            isRecover = false;
            // Comment: Ʈ���� ��ư���� ShieldOn ����
            shieldOnOff.action.performed -= ShieldOn;

            // Comment: Ʈ���� ��ư���� ShiledOff �߰�
            shieldOnOff.action.performed += ShieldOff;

            // Comment: ���� Ȱ��ȭ�� �� ��� ��� ��Ȱ��ȭ
            //fire.action.performed -= GetComponent<PlayerInputWeapon>().OnFire;        �ѱ�� ���� �����̶� ���� ���� �ּ�ó�� ����
            //fire.action.performed -= Getcomponent<PlayerInputWeapon>().OffFire;

            damageTest.action.performed += DamagedShield; // �׽�Ʈ ������ ��������
        }
    }

    // Comment: ������ ��Ȱ��ȭ �� ��
    private void OnDisable()
    {
        if (!isBreaked)
        {
            isRecover = true;
            // Comment: Ʈ���� ��ư���� ShieldOn �߰�
            shieldOnOff.action.performed += ShieldOn;

            // Comment: Ʈ���� ��ư���� ShiledOff ����
            shieldOnOff.action.performed -= ShieldOff;

            // Comment: ���� ��Ȱ��ȭ�� �� ��� ��� Ȱ��ȭ
            //fire.action.performed += GetComponent<PlayerInputWeapon>().OnFire;         �ѱ�� ���� �����̶� ���� ���� �ּ�ó�� ����
            //fire.action.performed += Getcomponent<PlayerInputWeapon>().OffFire;

            damageTest.action.performed -= DamagedShield; // �׽�Ʈ ������ ��������
        }
    }

    private void Update()
    {
        // Comment: ������ ��ġ�� �÷��̾� ��ġ�� ����ٴϰ�
        transform.position = playerPos.transform.position;

            if (durability < 1)
            {
                BreakedShield();
            }
        
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
        Debug.Log("���� ��������");
        // ToDo : �ǰݽ� ���� �����ؾ���
        durability -= 1;

        damaged.Play();
        Debug.Log(durability);
    }

    // Comment: ���� �ı�, ������ ��Ȱ��ȭ�Ǹ� isBreaked ������ �� ����
    public void BreakedShield()
    {
        gameObject.SetActive(false);
        shieldRecover.SetActive(true);
        isBreaked = true;

        breaked.Play();
        Debug.Log("������ �ı��Ǿ����ϴ�.");
        
    }

    
}

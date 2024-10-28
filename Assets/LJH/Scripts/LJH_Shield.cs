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
    [SerializeField] GameObject invincibility;

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
    public bool isShield;                         // Comment: ���� Ȱ��ȭ ����   �ʿ������ ���� ����
    public bool isBreaked;                        // Comment: ���� �ı� ����
    public bool isRecover;                        // Comment: ȸ�� ���� ����
    public bool isInvincibility;

    public float durability;                      // Comment: ���� ������
    public const float MAXDURABILITY = 5;         // Comment: ���� �ִ� ������
    public float damage = 1;                      // Comment: ���� ���ط�                                ToDo: ������ �������� �����ؾ���


    private void Awake()
    {
        gameObject.SetActive(false);
        isRecover = false;
        isShield = false;
        isBreaked = false;
        isInvincibility = false;
        durability = MAXDURABILITY;
    }
    
    // Comment: ������ Ȱ��ȭ �� ��
    private void OnEnable()
    {
        durability = MAXDURABILITY;
       //isBreaked = shieldRecover.GetComponent<LJH_ShieldRecover>().isBreaked;
       //isRecover = shieldRecover.GetComponent<LJH_ShieldRecover>().isRecover;
       //isShield = shieldRecover.GetComponent<LJH_ShieldRecover>().isShield;

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

    // Comment: ������ ��Ȱ��ȭ �� ��
    private void OnDisable()
    {
        
            // Comment: Ʈ���� ��ư���� ShieldOn �߰�
            shieldOnOff.action.performed += ShieldOn;

            // Comment: Ʈ���� ��ư���� ShiledOff ����
            shieldOnOff.action.performed -= ShieldOff;

            // Comment: ���� ��Ȱ��ȭ�� �� ��� ��� Ȱ��ȭ
            //fire.action.performed += GetComponent<PlayerInputWeapon>().OnFire;         �ѱ�� ���� �����̶� ���� ���� �ּ�ó�� ����
            //fire.action.performed += Getcomponent<PlayerInputWeapon>().OffFire;

            damageTest.action.performed -= DamagedShield; // �׽�Ʈ ������ ��������


        
    }

    private void Update()
    {
        // Comment: ������ ��ġ�� �÷��̾� ��ġ�� ����ٴϰ�
        transform.position = playerPos.transform.position;

        if (durability <= 0)
        {
            BreakedShield();
        }

    }


    // Comment: ���� Ȱ��ȭ
    public void ShieldOn(InputAction.CallbackContext obj)
    {
        if (!isBreaked)
        {
            gameObject.SetActive(true);
            shieldRecover.SetActive(false);
            isShield = true;
        }
    }

    // Comment: ���� ��Ȱ��ȭ
    public void ShieldOff(InputAction.CallbackContext obj)
    {
        isRecover = true;
        shieldRecover.SetActive(true);
        gameObject.SetActive(false);
        isShield = false;
    }

    // Comment: �ǰݽ� ���� ������ 1 ����
    // ToDo:    ������ Ÿ�� ��Ŀ� ���� ���� ���� �ʿ�
    public void DamagedShield(InputAction.CallbackContext obj)// �μ� ��������
    {
        if (durability > 0)
        {
            Debug.Log("���� ��������");
            // ToDo : �ǰݽ� ���� �����ؾ���

            if (isInvincibility)
            {
                damage = 0;
                durability -= damage;
            }
            else if (!isInvincibility)
            {
                durability -= damage;
                Instantiate(invincibility);
            }

            damaged.Play();
            Debug.Log(durability);
        }
    }

    // Comment: ���� �ı�, ������ ��Ȱ��ȭ�Ǹ� isBreaked ������ �� ����
    public void BreakedShield()
    {
        isRecover = true;
        isBreaked = true;
        isShield = false;
        shieldRecover.SetActive(true);
        
        gameObject.SetActive(false);
        

        breaked.Play();
        Debug.Log("������ �ı��Ǿ����ϴ�.");
        
    }

    
}

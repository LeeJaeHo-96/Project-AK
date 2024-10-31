using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class LJH_Shield : MonoBehaviour
{
    [Header("������Ʈ")]
    [Header("���� ���� ������Ʈ")]
    [SerializeField] GameObject shieldRecover;
    [Header("���� ���� ������Ʈ")]
    [SerializeField] GameObject invincibility;

    [Header("��ũ��Ʈ")]
    [Header("UIManager ��ũ��Ʈ")]
    [SerializeField] LJH_UIManager uiManagerScript;
    [Header("monsterTest ��ũ��Ʈ")]
    [SerializeField] LJH_monsterTest monsterScript;

    [Header("�÷��̾� ��ġ")]
    [SerializeField] GameObject playerPos;

    [Header("Ű�Է�")]
    [Header("���� �¿��� Ű�Է�")]
    [SerializeField] InputActionReference shieldOnOff;
    [Header("������ �׽�Ʈ Ű�Է�")]
    [SerializeField] InputActionReference damageTest; // �׽�Ʈ ������ ��������
    [Header("�� �߻� Ű�Է�")]
    [SerializeField] InputActionReference fire;

    [Header("�����")]
    [Header("���� ���ؽ� ����")]
    [SerializeField] AudioSource damaged;
    [Header("���� �ı��� ����")]
    [SerializeField] AudioSource breaked;

    [Header("����")]
    [Header("���� Ȱ��ȭ ����")]
    public bool isShield;                         // Comment: ���� Ȱ��ȭ ����   �ʿ������ ���� ����
    [Header("���� �ı�/���ı� ����")]
    public bool isBreaked;                        // Comment: ���� �ı� ����
    [Header("���� ������ ����")]
    public bool isRecover;                        // Comment: ȸ�� ���� ����
    [Header("���� ���� ���� ����")]
    public bool isInvincibility;
    [Header("���� ������")]
    public float durability;                      // Comment: ���� ������
    [Header("���� �ִ� ������")]
    public const float MAXDURABILITY = 5;         // Comment: ���� �ִ� ������
    [Header("���ط�")]
    public float damage = 1;                      // Comment: ���� ���ط� ToDo: ������ ���� ������ �޾ƿ;���
    [Header("������ ���� �˶��� Bool ����")]
    public bool isNow;


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

            isRecover = false;
            // Comment: Ʈ���� ��ư���� ShieldOn ����
            shieldOnOff.action.performed -= ShieldOn;

            // Comment: Ʈ���� ��ư���� ShiledOff �߰�
            shieldOnOff.action.performed += ShieldOff;

            // Comment: ���� Ȱ��ȭ�� �� ��� ��� ��Ȱ��ȭ
            fire.action.performed -= GetComponent<PlayerInputWeapon>().OnFire;
            fire.action.performed -= GetComponent<PlayerInputWeapon>().OffFire;

            damageTest.action.performed += DamagedShieldTest; // �׽�Ʈ ������ ��������
        
    }

    // Comment: ������ ��Ȱ��ȭ �� ��
    private void OnDisable()
    {
            // Comment: Ʈ���� ��ư���� ShieldOn �߰�
            shieldOnOff.action.performed += ShieldOn;

            // Comment: Ʈ���� ��ư���� ShiledOff ����
            shieldOnOff.action.performed -= ShieldOff;

            // Comment: ���� ��Ȱ��ȭ�� �� ��� ��� Ȱ��ȭ
            fire.action.performed += GetComponent<PlayerInputWeapon>().OnFire;
            fire.action.performed += GetComponent<PlayerInputWeapon>().OffFire;

            damageTest.action.performed -= DamagedShieldTest; // �׽�Ʈ ������ ��������


        
    }

    private void Update()
    {
        // Comment: ������ ��ġ�� �÷��̾� ��ġ�� ����ٴϰ�
        transform.position = playerPos.transform.position;

        
        
        // Comment: �������� 0�� �� ��, ���� �ı�
        if (durability <= 0)
        {
            BreakedShield();
        }

    }


    // Comment: ���� Ȱ��ȭ
    public void ShieldOn(InputAction.CallbackContext obj)
    {
        // Comment: ���� �ı� ���°� �ƴҶ��� �ش� �Լ� �ҷ��� �� �ֵ���
        if (!isBreaked)
        {
            // Comment: ���� > Ȱ��ȭ , ���� ���� > ��Ȱ��ȭ, ���� ���� > Ȱ��ȭ
            gameObject.SetActive(true);
            shieldRecover.SetActive(false);
            isShield = true;
        }
    }

    // Comment: ���� ��Ȱ��ȭ
    public void ShieldOff(InputAction.CallbackContext obj)
    {
        // Comment: ���� > ��Ȱ��ȭ, ���� ���� > Ȱ��ȭ, ���� ���� > ��Ȱ��ȭ 
        isRecover = true;
        shieldRecover.SetActive(true);
        gameObject.SetActive(false);
        isShield = false;
    }

    // Comment: �ǰݽ� ���� ������ 1 ���� (�׽�Ʈ�� �Լ�, ������ �ּ� ó�� �� �׽�Ʈ���� ����ϰų� ����)
    // ToDo:    ������ Ÿ�� ��Ŀ� ���� ���� ���� �ʿ�
    public void DamagedShieldTest(InputAction.CallbackContext obj)// �μ� ��������
    {
        if (durability > 0)
        {
            // ToDo : �ǰݽ� ���� �����ؾ���

            if (isInvincibility)
            {
                // Comment: ���� ������ ��, �������� 0���� ����
                float zeroDamage = 0;

                durability -= zeroDamage;
            }
            else if (!isInvincibility)
            {

                durability -= 1;
                uiManagerScript.UpdateShieldUI(durability);

                isNow = true;
                Debug.Log(isNow);


                invincibility.SetActive(true);
            }

            damaged.Play();
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

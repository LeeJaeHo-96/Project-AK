using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Audio;

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

    [Header("�÷��̾� ��ġ")]
    [SerializeField] GameObject playerPos;

    [Header("Ű�Է�")]
    [Header("���� �¿��� Ű�Է�")]
    [SerializeField] InputActionReference shieldOnOff;
    [Header("�� �߻� Ű�Է�")]
    [SerializeField] InputActionReference fire;

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
    [Header("������ ���� �˶��� Bool ����")]
    public bool isNow;




    private void Awake()
    {
        gameObject.SetActive(false);
        isRecover = false;
        isShield = false;
        isBreaked = false;
        isInvincibility = false;

        durability = 5;

    }

    // Comment: ������ Ȱ��ȭ �� ��
    private void OnEnable()
    {

            isRecover = false;
            // Comment: Ʈ���� ��ư���� ShieldOn ����
            shieldOnOff.action.performed -= ShieldOn;

            // Comment: Ʈ���� ��ư���� ShiledOff �߰�
            shieldOnOff.action.performed += ShieldOff;

    }

    // Comment: ������ ��Ȱ��ȭ �� ��
    private void OnDisable()
    {
            // Comment: Ʈ���� ��ư���� ShieldOn �߰�
            shieldOnOff.action.performed += ShieldOn;

            // Comment: Ʈ���� ��ư���� ShiledOff ����
            shieldOnOff.action.performed -= ShieldOff;

    }

    private void Update()
    {
        if (WHS_StageIndex.curStage == 1)
        {
            transform.position = new Vector3(0, 1, 0);
        }
        else
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
        // Comment: �Ͻ������� ��� �Ұ�
        if (MenuEvent.Instance.IsPause)
            return;

        // Comment: ���� �ı� ���°� �ƴҶ��� �ش� �Լ� �ҷ��� �� �ֵ���
        if (!isBreaked)
        {
            // Comment: ���� > Ȱ��ȭ , ���� ���� > ��Ȱ��ȭ, ���� ���� > Ȱ��ȭ
            if (this == null) return;

                gameObject.SetActive(true);
                shieldRecover.SetActive(false);
                isShield = true;
            


            // Comment: �ѱ� ��ǲ ����
           PlayerInputWeapon.Instance.enabled = false;
           PlayerInputWeapon.Instance.IsShield = isShield;
        }
    }

    // Comment: ���� ��Ȱ��ȭ
    public void ShieldOff(InputAction.CallbackContext obj)
    {
        // Comment: �Ͻ������� ��� �Ұ�
        if (MenuEvent.Instance.IsPause)
            return;

        // Comment: ���� > ��Ȱ��ȭ, ���� ���� > Ȱ��ȭ, ���� ���� > ��Ȱ��ȭ 
        isRecover = true;
        shieldRecover.SetActive(true);
        gameObject.SetActive(false);
        isShield = false;

        // Comment:�ѱ� ��ǲ �ѱ�

        PlayerInputWeapon.Instance.enabled = true;
        PlayerInputWeapon.Instance.IsShield = isShield;
    }

    // Comment: ���� �ı�, ������ ��Ȱ��ȭ�Ǹ� isBreaked ������ �� ����
    public void BreakedShield()
    {
        isRecover = true;
        isBreaked = true;
        isShield = false;
        shieldRecover.SetActive(true);

        PlayerInputWeapon.Instance.enabled = true;
        PlayerInputWeapon.Instance.IsShield = isShield;


        gameObject.SetActive(false);
    }
    

}

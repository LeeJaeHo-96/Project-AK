using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShieldUI : MonoBehaviour
{
    [Header("������Ʈ")]
    [SerializeField] GameObject lsy_shieldRecover;
    [SerializeField] GameObject lsy_invincibility;

    [Header("�÷��̾� ��ġ")]
    [SerializeField] GameObject lsy_playerPos;

    [Header("Ű�Է�")]
    [SerializeField] InputActionReference lsy_shieldOnOff;
    [SerializeField] InputActionReference lsy_damageTest; // �׽�Ʈ ������ ��������
    [SerializeField] InputActionReference lsy_fire;

    [Header("�����")]
    [SerializeField] AudioSource lsy_damaged;
    [SerializeField] AudioSource lsy_breaked;

    [Header("����")]
    public bool lsy_isShield;                         // Comment: ���� Ȱ��ȭ ����   �ʿ������ ���� ����
    public bool lsy_isBreaked;                        // Comment: ���� �ı� ����
    public bool lsy_isRecover;                        // Comment: ȸ�� ���� ����
    public bool lsy_isInvincibility;

    public float lsy_durability;                      // Comment: ���� ������
    public const float lsy_MAXDURABILITY = 5;         // Comment: ���� �ִ� ������
    public float lsy_damage = 1;                      // Comment: ���� ���ط�                                ToDo: ������ �������� �����ؾ���

    //�̽ÿ���
    [Header("UI")]
    int lsy_shieldCount = 4;
    public Image[] shieldImages = new Image[5];


    private void Awake()
    {
        gameObject.SetActive(false);
        lsy_isRecover = false;
        lsy_isShield = false;
        lsy_isBreaked = false;
        lsy_isInvincibility = false;
        lsy_durability = lsy_MAXDURABILITY;
    }

    // Comment: ������ Ȱ��ȭ �� ��
    private void OnEnable()
    {
        lsy_durability = lsy_MAXDURABILITY;
        //isBreaked = shieldRecover.GetComponent<LJH_ShieldRecover>().isBreaked;
        //isRecover = shieldRecover.GetComponent<LJH_ShieldRecover>().isRecover;
        //isShield = shieldRecover.GetComponent<LJH_ShieldRecover>().isShield;

        lsy_isRecover = false;
        // Comment: Ʈ���� ��ư���� ShieldOn ����
        lsy_shieldOnOff.action.performed -= LSY_ShieldOn;

        // Comment: Ʈ���� ��ư���� ShiledOff �߰�
        lsy_shieldOnOff.action.performed += LSY_ShieldOff;

        // Comment: ���� Ȱ��ȭ�� �� ��� ��� ��Ȱ��ȭ
        //fire.action.performed -= GetComponent<PlayerInputWeapon>().OnFire;        �ѱ�� ���� �����̶� ���� ���� �ּ�ó�� ����
        //fire.action.performed -= Getcomponent<PlayerInputWeapon>().OffFire;

        lsy_damageTest.action.performed += LSY_DamagedShield; // �׽�Ʈ ������ ��������

    }

    // Comment: ������ ��Ȱ��ȭ �� ��
    private void OnDisable()
    {

        // Comment: Ʈ���� ��ư���� ShieldOn �߰�
        lsy_shieldOnOff.action.performed += LSY_ShieldOn;

        // Comment: Ʈ���� ��ư���� ShiledOff ����
        lsy_shieldOnOff.action.performed -= LSY_ShieldOff;

        // Comment: ���� ��Ȱ��ȭ�� �� ��� ��� Ȱ��ȭ
        //fire.action.performed += GetComponent<PlayerInputWeapon>().OnFire;         �ѱ�� ���� �����̶� ���� ���� �ּ�ó�� ����
        //fire.action.performed += Getcomponent<PlayerInputWeapon>().OffFire;

        lsy_damageTest.action.performed -= LSY_DamagedShield; // �׽�Ʈ ������ ��������



    }

    private void Update()
    {
        // Comment: ������ ��ġ�� �÷��̾� ��ġ�� ����ٴϰ�
        transform.position = lsy_playerPos.transform.position;

        if (lsy_durability <= 0)
        {
            LSY_BreakedShield();
        }

    }


    // Comment: ���� Ȱ��ȭ
    public void LSY_ShieldOn(InputAction.CallbackContext obj)
    {
        if (!lsy_isBreaked)
        {
            gameObject.SetActive(true);
            lsy_shieldRecover.SetActive(false);
            lsy_isShield = true;
        }
    }

    // Comment: ���� ��Ȱ��ȭ
    public void LSY_ShieldOff(InputAction.CallbackContext obj)
    {
        lsy_isRecover = true;
        lsy_shieldRecover.SetActive(true);
        gameObject.SetActive(false);
        lsy_isShield = false;
    }

    // Comment: �ǰݽ� ���� ������ 1 ����
    // ToDo:    ������ Ÿ�� ��Ŀ� ���� ���� ���� �ʿ�
    public void LSY_DamagedShield(InputAction.CallbackContext obj)// �μ� ��������
    {
        if (lsy_durability > 0)
        {
            Debug.Log("���� ��������");
            // ToDo : �ǰݽ� ���� �����ؾ���

            if (lsy_isInvincibility)
            {
                lsy_damage = 0;
                lsy_durability -= lsy_damage;
            }
            else if (!lsy_isInvincibility)
            {
                lsy_durability -= lsy_damage;
                Instantiate(lsy_invincibility);
            }

            lsy_damaged.Play();
            Debug.Log(lsy_durability);
        }
    }

    // Comment: ���� �ı�, ������ ��Ȱ��ȭ�Ǹ� isBreaked ������ �� ����
    public void LSY_BreakedShield()
    {
        lsy_isRecover = true;
        lsy_isBreaked = true;
        lsy_isShield = false;
        lsy_shieldRecover.SetActive(true);

        gameObject.SetActive(false);


        lsy_breaked.Play();
        Debug.Log("������ �ı��Ǿ����ϴ�.");

    }
}

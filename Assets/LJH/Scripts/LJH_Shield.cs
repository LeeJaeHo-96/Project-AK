using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class LJH_Shield : MonoBehaviour
{
    [SerializeField] GameObject playerPos;
    [SerializeField] InputActionReference shieldOnOff;

    bool isSheild;
    bool isBreaked;
    float durability = 5;

    private void Start()
    {
        gameObject.SetActive(false);

        isSheild = false;
        isBreaked = false;
        durability = 5;
    }
    // ���а� Ȱ��ȭ �� ��
    private void OnEnable()
    {
        // Ʈ���� ��ư���� ShieldOn ����
        shieldOnOff.action.performed -= ShieldOn;
        // Ʈ���� ��ư���� ShiledOff �߰�
        shieldOnOff.action.performed += ShieldOff;
    }

    // ���а� ��Ȱ��ȭ �� ��
    private void OnDisable()
    {
        // Ʈ���� ��ư���� ShieldOn �߰�
        shieldOnOff.action.performed += ShieldOn;
        // Ʈ���� ��ư���� ShiledOff ����
        shieldOnOff.action.performed -= ShieldOff;
    }

    private void Update()
    {
        // ������ ��ġ�� �÷��̾� ��ġ�� ����ٴϰ�
        transform.position = playerPos.transform.position;
        
    }


    // ���� Ȱ��ȭ
    public void ShieldOn(InputAction.CallbackContext obj)
    {
        gameObject.SetActive(true);
        isSheild = true;
    }

    // ���� ��Ȱ��ȭ
    public void ShieldOff(InputAction.CallbackContext obj)
    {
        gameObject.SetActive(false);
        isSheild = false;
    }

    public void BreakedShield()
    {
        gameObject.SetActive(false);
        isBreaked = true;
    }

    
    IEnumerator ShieldCoolDown()
    {
        // 2�ʰ� ���� ��� �Ұ�
        yield return new WaitForSeconds(2.0f);

        // 2�� �� RecoveryShield �ڷ�ƾ ����
        Coroutine recovery = StartCoroutine("RecoveryShield");
    }

    IEnumerator RecoveryShield()
    {
        // ���� ��� �Ұ� + 3�� �� ���� ���� �Ϸ�
        yield return new WaitForSeconds(3.0f);
        isBreaked = false;
    }
    
}

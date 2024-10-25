using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class LJH_Shield : MonoBehaviour
{
    [SerializeField] GameObject ObjShieldPrefab;
    [SerializeField] GameObject playerPos;
    [SerializeField] InputActionReference shieldOn;

    bool isBreaked;
    float durability = 5;

    private void Start()
    {
        ObjShieldPrefab = gameObject;
        //shieldOn.action.performed += ShieldOn;

        isBreaked = false;
        durability = 5;
    }
    
    private void Update()
    {
        transform.position = playerPos.transform.position; // ������ ��ġ�� �÷��̾� ��ġ�� ����ٴϰ�
        
    }

   

    public void ShieldOn()                                  // ���� Ȱ��ȭ
    {
        ObjShieldPrefab.SetActive(true);
    }

    public void ShieldOff()                                 // ���� ��Ȱ��ȭ
    {
        ObjShieldPrefab.SetActive(false);
    }

    public void BreakedShield()
    {
        ObjShieldPrefab.SetActive(false);
        isBreaked = true;
    }

    /*
    IEnumerator ShieldCoolDown()
    {
        yield return new WaitForSecond(2.0f);               // 2�ʰ� ���� ��� �Ұ�
        Coroutine recovery = StartCoroutine("RecoveryShield");
    }

    IEnumerator RecoveryShield()
    {
        yield return new WaitForSecond(3.0f);               // ���� ��� �Ұ� + 3�� �� ���� ���� �Ϸ�
        isBreaked = false;
    }
    */
}

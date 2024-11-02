using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LJH_invincibility : MonoBehaviour
{
    [Header("������Ʈ")]
    [Header("���� ������Ʈ")]
    [SerializeField] GameObject shield;
    [SerializeField] GameObject damageManager;

    [Header("����")]
    [Header("���� ���� ����")]
    public bool isInvincibility;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        if(!isInvincibility)
        isInvincibility = true;
        damageManager.GetComponent<LJH_DamageManager>().isInvincibility = isInvincibility;
        Invoke("ObjOff", 0.2f);
    }

    void OnDisable()
    {
        isInvincibility = false;
        if (damageManager == null)
        {
            return;
        }
        else
        {
            damageManager.GetComponent<LJH_DamageManager>().isInvincibility = isInvincibility;
        }
    }

    void ObjOff()
    {
        gameObject.SetActive(false);
    }
}

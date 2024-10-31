using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LJH_invincibility : MonoBehaviour
{
    [Header("������Ʈ")]
    [Header("���� ������Ʈ")]
    [SerializeField] GameObject shield;

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
        shield.GetComponent<LJH_Shield>().isInvincibility = isInvincibility;
        Invoke("ObjOff", 0.2f);
    }

    void OnDisable()
    {
        isInvincibility = false;
        shield.GetComponent<LJH_Shield>().isInvincibility = isInvincibility;
    }

    void ObjOff()
    {
        gameObject.SetActive(false);
    }
}

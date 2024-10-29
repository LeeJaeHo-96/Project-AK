using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class LJH_invincibility : MonoBehaviour
{
    [SerializeField] GameObject shield;
    public bool isInvincibility;

    

    void OnEnable()
    {
        if(!isInvincibility)
        Debug.Log("���� �ߵ�");
        isInvincibility = true;
        shield.GetComponent<LJH_Shield>().isInvincibility = isInvincibility;
        Destroy(gameObject, 2f);
    }

    void OnDisable()
    {
        Debug.Log("���� ����");
        isInvincibility = false;
    }
}

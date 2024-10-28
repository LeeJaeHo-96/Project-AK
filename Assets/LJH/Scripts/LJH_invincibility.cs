using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class LJH_invincibility : MonoBehaviour
{
    GameObject shield;

    private void Start()
    {
        shield = GameObject.Find("Shield");
    }

    void OnEnable()
    {
        Debug.Log("���� �ߵ�");
        shield.GetComponent<LJH_Shield>().isInvincibility = true;

        Destroy(gameObject, 0.2f);
    }

    void OnDisable()
    {
        Debug.Log("���� ����");
        shield.GetComponent<LJH_Shield>().isInvincibility = false;
    }
}

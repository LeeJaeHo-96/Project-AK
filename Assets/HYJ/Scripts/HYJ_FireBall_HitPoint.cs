using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HYJ_FireBall_HitPoint : MonoBehaviour
{
    [SerializeField] HYJ_FireBall fireBall;
    [SerializeField] bool weak;

    [Header("������ �ؽ�Ʈ ����")]
    [SerializeField] public GameObject canvas;
    [SerializeField] public TextMeshPro damageText;

    private void Awake()
    {
        fireBall = GetComponent<HYJ_FireBall>();
    }

    public bool TakeDamage(float damage)
    {
        Debug.Log("�ǰ�");
        if (fireBall.HitFlag == false)
        {

        }
    }
}

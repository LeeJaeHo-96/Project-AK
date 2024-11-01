using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HYJ_Boss_Stage1 : MonoBehaviour
{
    [Header("���� ����")]
    [SerializeField] GameObject monster;
    [SerializeField] float nowHp;
    [SerializeField] float SetHp;
    //[SerializeField] float 
    [SerializeField] public float monsterShieldAtkPower;
    [SerializeField] public float monsterHpAtkPower;
    [SerializeField] public float monsterMoveSpeed;

    [Header("���� ����")]
    [SerializeField] public bool hitFlag;
    public bool HitFlag { get { return hitFlag; } set { hitFlag = value; } }

    Coroutine hitFlagCoroutine;
    WaitForSeconds hitFlagWaitForSeconds = new WaitForSeconds(0.1f);
    private void Start()
    {
        gameObject.tag = "Boss";
        nowHp = SetHp;
    }

    private void Update()
    {
        
    }
}

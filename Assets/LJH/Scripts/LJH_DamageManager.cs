using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LJH_DamageManager : MonoBehaviour
{

    private float lsy_hp = 100;
    private Color curColor;
    private readonly Color initColor = Color.green;

    [SerializeField] float curHp;
    public Image hpBar;

    [SerializeField] float durability;
    [SerializeField] float shieldATK;
    [SerializeField] float HPATK;
    [SerializeField] bool isInvincibility;
    [SerializeField] float HP;

    [SerializeField] AudioSource damagedShield;
    [SerializeField] AudioSource damagedHP;


    [SerializeField] GameObject invincibility;
    private void Start()
    {
        curColor = initColor;
        hpBar.color = initColor;
    }

    // Update is called once per frame
    void Update()
    {
        durability = GetComponent<LJH_Shield>().durability;
        //shiledATK = GetComponent<���ͽ�ũ��Ʈ>().������ݷ� - �����Բ�
        //HPATK = GetComponent<���ͽ�ũ��Ʈ>().ü�°��ݷ� - �����Բ�
        isInvincibility = GetComponent<LJH_invincibility>().isInvincibility;
    }


    public void DamagedHp()
    {
        if (durability > 0)
        {
            Debug.Log("ĳ���� ��������");

            HP -= shieldATK;

            damagedHP.Play();
            Debug.Log(HP);
        }
    }


    public void DamagedShield()
    {
        if (durability > 0)
        {
            Debug.Log("���� ��������");

            if (isInvincibility)
            {
                shieldATK = 0;
                durability -= shieldATK;
            }
            else if (!isInvincibility)
            {
                durability -= shieldATK;
                Instantiate(invincibility);
            }

            damagedShield.Play();
            Debug.Log(durability);
        }
    }

    private void DisplayHpBar()
    {
        float hpPercentage = curHp / lsy_hp;
        if (hpPercentage > 0.5f)
        {
            curColor = Color.green;
        }
        else if (hpPercentage > 0.3f)
        {
            curColor = Color.yellow;
        }
        else
        {
            curColor = Color.red;
        }
        hpBar.color = curColor;
        hpBar.fillAmount = hpPercentage;
    }

   // public int TakeDamage(GameObject monster)
   // {
   //     if(���� Ȱ��ȭ)
   //     {
   //         return ���ݷ� = �μ��� ������ (�����)���ݷ�;
   //     }
   //
   //     else if(���� ��Ȱ��ȭ)
   //     {
   //         return ���ݷ� = �μ��� ������ (ü�¿�)���ݷ�;
   //     }
   //     return 0;
   // }
}

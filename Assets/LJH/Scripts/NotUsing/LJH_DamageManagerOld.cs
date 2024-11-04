using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LJH_DamageManagerOld : MonoBehaviour
{/*
    [Header("������Ʈ")]
    [Header("���� ���� ������Ʈ")]
    [SerializeField] GameObject ljh_invincibility;
    [Header("���� ������Ʈ")]
    [SerializeField] GameObject shield;
    [Header("���� ������Ʈ")]
    [SerializeField] GameObject monster;

    [Header("��ũ��Ʈ")]
    [Header("MonsterTest ��ũ��Ʈ")]
    //[SerializeField] LJH_monsterTest enemyScript;
    [Header("HYK_Enemy ��ũ��Ʈ")]
    [SerializeField] HYJ_Enemy enemyScript;
    [Header("Shield ��ũ��Ʈ")]
    [SerializeField] LJH_Shield shieldScript;
    [Header("UIManager ��ũ��Ʈ")]
    [SerializeField] LJH_UIManager uiManagerScript;

    [Header("����")]
    [Header("���� ü��")]
    float ljh_curHp = 100;
    [Header("���� ������")]
    [SerializeField] float ljh_durability;
    [Header("���� Ȱ��ȭ ����")]
    [SerializeField] bool ljh_isInvincibility;


    [Header("�̹���")]
    [Header("ü�� �ǰ� �̹���")]
    public Image ljh_bloodImage;
    [Header("���� �ǰ� �̹���")]
    public Image ljh_shieldImage;

    [Header("�ڷ�ƾ")]
    [Header("ü�� �ǰ� �ڷ�ƾ")]
    private Coroutine ljh_bloodCoroutine;
    [Header("���� �ǰ� �ڷ�ƾ")]
    private Coroutine ljh_shieldCoroutine;

    [Header("�����")]
    [Header("���� ���ؽ� ����")]
    [SerializeField] AudioSource ljh_damagedShield;
    [Header("ü�� ���ؽ� ����")]
    [SerializeField] AudioSource ljh_damagedHP;

    void Update()
    {
        ljh_durability = shield.GetComponent<LJH_Shield>().durability;
        ljh_isInvincibility = shield.GetComponent<LJH_Shield>().isInvincibility;


        if (shield.GetComponent<LJH_Shield>().isShield)
        {
            if (monster.GetComponent<HYJ_Enemy>().nowAttack)
            {
                float damage = TakeDamage(enemyScript);
                DamagedShield(damage);

                if (ljh_shieldCoroutine != null)
                {
                    StopCoroutine(ljh_shieldCoroutine);
                }
                ljh_shieldCoroutine = StartCoroutine(ShowShieldScreen());
            }
        }
        else if (!shield.GetComponent<LJH_Shield>().isShield)
        {
            if (monster.GetComponent<HYJ_Enemy>().nowAttack)
            {
                float damage = TakeDamage(enemyScript);
                DamagedHP(damage);

                if (ljh_bloodCoroutine != null)
                {
                    StopCoroutine(ljh_bloodCoroutine);
                }
                ljh_bloodCoroutine = StartCoroutine(ShowBloodScreen());
            }
        }
            uiManagerScript.DisplayHpBar();
        
    }

    public void DamagedHP(float HPDamage)
    {
        Debug.Log("ü�� ��������");
        ljh_curHp -= HPDamage;
        
        //damagedHP.Play();
        
    }

    public void DamagedShield(float shieldDamage)// �μ� ��������
    {
        if (ljh_durability > 0)
        {
            // ToDo : �ǰݽ� ���� �����ؾ���

            if (ljh_isInvincibility)
            {
                float zeroDamage = 0;

                Debug.Log("���� ���� ����");
                ljh_durability -= zeroDamage;
            }
            else if (!ljh_isInvincibility)
            {
                Debug.Log("���� ��������");
                ljh_durability -= shieldDamage;
                uiManagerScript.UpdateShieldUI(ljh_durability);
                ljh_invincibility.SetActive(true);
            }

            //damagedShield.Play();
            Debug.Log(ljh_durability);
        }
    }

    IEnumerator ShowBloodScreen()
    {
        ljh_bloodImage.color = new Color(1, 0, 0, UnityEngine.Random.Range(0.9f, 1f));
        float duration = 1.5f;
        float elapsedTime = 0f;
        Color initialColor = ljh_bloodImage.color;
        // Commet : 1.5�� ���� ���� �̹����� ������������ ����
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(initialColor.a, 0, elapsedTime / duration);
            ljh_bloodImage.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            yield return null;
        }
        ljh_bloodImage.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0);
    }

    IEnumerator ShowShieldScreen()
    {
        ljh_shieldImage.color = new Color(0, 0, 1, UnityEngine.Random.Range(0.9f, 1f));
        float duration = 1.5f;
        float elapsedTime = 0f;
        Color initialColor = ljh_shieldImage.color;
        // Commet : 1.5�� ���� ���� �̹����� ������������ ����
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(initialColor.a, 0, elapsedTime / duration);
            ljh_shieldImage.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            yield return null;
        }
        ljh_shieldImage.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0);
    }

    public float TakeDamage(HYJ_Enemy monsterScript)
    {
        if(shield.GetComponent<LJH_Shield>().isShield)
        {
            float damage;
            return damage = monsterScript.GetComponent<HYJ_Enemy>().monsterShieldAtkPower;
        }
    
        else if(!shield.GetComponent<LJH_Shield>().isShield)
        {
            float damage;
            return damage = monsterScript.GetComponent<HYJ_Enemy>().monsterHpAtkPower;
        }
        return 0;
    }*/
    
}


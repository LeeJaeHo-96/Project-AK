using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LJH_DamageManager : MonoBehaviour
{

    float ljh_curHp = 100;
    public RawImage ljh_bloodImage;
    private Coroutine ljh_bloodCoroutine;
    public Image ljh_shieldImage;
    private Coroutine ljh_shieldCoroutine;                          // �ÿ��Բ�

    [SerializeField] float ljh_durability;
    [SerializeField] bool ljh_isInvincibility;


    [SerializeField] AudioSource ljh_damagedShield;
    [SerializeField] AudioSource ljh_damagedHP;

    [Header("������Ʈ")]
    [SerializeField] GameObject ljh_invincibility;
    [SerializeField] GameObject shield;
    [SerializeField] GameObject monster;

    [Header("��ũ��Ʈ")]
    [SerializeField] HYJ_Enemy enemyScript;
    [SerializeField] LJH_Shield shieldScript;
    [SerializeField] LJH_UIManager uiManagerScript;
    

    // Update is called once per frame
    void Update()
    {
        ljh_durability = shield.GetComponent<LJH_Shield>().durability;
        ljh_isInvincibility = GetComponent<LJH_invincibility>().isInvincibility;

        //if(enemyScript.nowAttack)
        //{
            if (shield.GetComponent<LJH_Shield>().isShield)
            {
                float damage = TakeDamage(enemyScript);
                DamagedShield(damage);

                if (ljh_shieldCoroutine != null)
                {
                    StopCoroutine(ljh_shieldCoroutine);
                }
                ljh_shieldCoroutine = StartCoroutine(ShowShieldScreen());
            }
            else if (!shield.GetComponent<LJH_Shield>().isShield)
            {
                float damage = TakeDamage(enemyScript);
                DamagedHP(damage);

                if (ljh_bloodCoroutine != null)
                {
                    StopCoroutine(ljh_bloodCoroutine);
                }
                ljh_bloodCoroutine = StartCoroutine(ShowBloodScreen());
            }

            uiManagerScript.DisplayHpBar();
        //}

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Todo : �� �κ��� �� ���� �� �ǰ��� ������ ��� ����ǵ���
            // Comment : ���ο� �ǰ��� ���� ��� �����ϴ� �ڷ�ƾ�� ���߰� ����۵ǵ���
            if (ljh_bloodCoroutine != null)
            {
                StopCoroutine(ljh_bloodCoroutine);
            }
            ljh_bloodCoroutine = StartCoroutine(ShowBloodScreen());
            // Todo : �� �κ��� �� ������ �� �ǰ� ������ ����ǵ���
            // Comment : ���ο� �ǰ��� ���� ��� �����ϴ� �ڷ�ƾ�� ���߰� ����۵ǵ���
            if (ljh_shieldCoroutine != null)
            {
                StopCoroutine(ljh_shieldCoroutine);
            }
            ljh_shieldCoroutine = StartCoroutine(ShowShieldScreen());
        }
    }


    //ToDo: ��� �°� �������ؾ���


    public void DamagedHP(float HPDamage)
    {
        
        Debug.Log("ü�� ��������");
        ljh_curHp -= HPDamage;
        
        //damaged.Play();
        Debug.Log(ljh_durability);
        
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

            //damaged.Play();
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
            //Todo: ���� ���� ����(�ۺ� �̽�)
            //return damage = monsterScript.GetComponent<HYJ_Enemy>().monsterShieldAtkPower;
            return damage = 1;
        }
    
        else if(!shield.GetComponent<LJH_Shield>().isShield)
        {
            float damage;
            //Todo: ���� ���� ����(�ۺ� �̽�)
            //return damage = monsterScript.GetComponent<HYJ_Enemy>().monsterHpAtkPower;
            return damage = 1000;
        }
        return 0;
    }
    
}


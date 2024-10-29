/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LJH_DamageManager : MonoBehaviour
{

    private float ljh__HP = 100;
    private Color ljh_curColor;
    private readonly Color ljh_initColor = Color.green;
    float ljh_curHp = 100;
    public Image ljh_hpBar;
    public Image ljh_bloodImage;
    private Coroutine ljh_bloodCoroutine;
    public Image ljh_shieldImage;
    private Coroutine ljh_shieldCoroutine;                          // �ÿ��Բ�

    [SerializeField] float ljh_durability;
    [SerializeField] float ljh_shieldATK;
    [SerializeField] float ljh_HPATK;
    [SerializeField] bool ljh_isInvincibility;
    [SerializeField] float ljh_HP;

    [SerializeField] AudioSource ljh_damagedShield;
    [SerializeField] AudioSource ljh_damagedHP;


    [SerializeField] GameObject ljh_invincibility;
    private void Start()
    {
        ljh_curColor = ljh_initColor;
        ljh_hpBar.color = ljh_initColor;
    }

    // Update is called once per frame
    void Update()
    {
        ljh_durability = GetComponent<LJH_Shield>().durability;
        //shiledATK = GetComponent<���ͽ�ũ��Ʈ>().������ݷ� - �����Բ�
        //HPATK = GetComponent<���ͽ�ũ��Ʈ>().ü�°��ݷ� - �����Բ�
        ljh_isInvincibility = GetComponent<LJH_invincibility>().isInvincibility;
    }

   // private void OnTriggerEnter(Collider other)
   // {
   //     if (other.gameObject.CompareTag("Enemy"))
   //     {
   //         DisplayHpBar();
   //         // Todo : �� �κ��� �� ���� �� �ǰ��� ������ ��� ����ǵ���
   //         // Comment : ���ο� �ǰ��� ���� ��� �����ϴ� �ڷ�ƾ�� ���߰� ����۵ǵ���
   //         if (bloodCoroutine != null)
   //         {
   //             StopCoroutine(bloodCoroutine);
   //         }
   //         bloodCoroutine = StartCoroutine(ShowBloodScreen());
   //         // Todo : �� �κ��� �� ������ �� �ǰ� ������ ����ǵ���
   //         // Comment : ���ο� �ǰ��� ���� ��� �����ϴ� �ڷ�ƾ�� ���߰� ����۵ǵ���
   //         if (shieldCoroutine != null)
   //         {
   //             StopCoroutine(shieldCoroutine);
   //         }
   //         shieldCoroutine = StartCoroutine(ShowShieldScreen());
   //     }
   // }
   //   ToDo: ��� �°� �������ؾ���


    public void DamagedHp()
    {
        if (ljh_durability > 0)
        {
            Debug.Log("ĳ���� ��������");

            ljh_HP -= ljh_shieldATK;

            ljh_damagedHP.Play();
            Debug.Log(ljh_HP);
        }
    }


    public void DamagedShield()
    {
        if (ljh_durability > 0)
        {
            Debug.Log("���� ��������");

            if (ljh_isInvincibility)
            {
                ljh_shieldATK = 0;
                ljh_durability -= ljh_shieldATK;
            }
            else if (!ljh_isInvincibility)
            {
                ljh_durability -= ljh_shieldATK;
                Instantiate(ljh_invincibility);
            }

            ljh_damagedShield.Play();
            Debug.Log(ljh_durability);
        }
    }

    private void DisplayHpBar()
    {
        float hpPercentage = ljh_curHp / ljh_HP;
        if (hpPercentage > 0.5f)
        {
            ljh_curColor = Color.green;
        }
        else if (hpPercentage > 0.3f)
        {
            ljh_curColor = Color.yellow;
        }
        else
        {
            ljh_curColor = Color.red;
        }
        ljh_hpBar.color = ljh_curColor;
        ljh_hpBar.fillAmount = hpPercentage;
    }

    IEnumerator ShowBloodScreen()
    {
        ljh_bloodImage.color = new Color(1, 0, 0, UnityEngine.Random.Range(0.4f, 0.5f));
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
        ljh_shieldImage.color = new Color(0, 0, 1, UnityEngine.Random.Range(0.4f, 0.5f));
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
*/

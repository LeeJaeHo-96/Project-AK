using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HYJ_Boss2_Object_HitPoint : MonoBehaviour
{
    [SerializeField] HYJ_Boss_Stage2_Object magicCircle;
    [SerializeField] bool weak;

    [Header("������ �ؽ�Ʈ ����")]
    [SerializeField] public GameObject canvas;
    [SerializeField] public Text damageText;

    private void Awake()
    {
        magicCircle = GetComponentInParent<HYJ_Boss_Stage2_Object>();
    }

    public bool TakeDamage(float damage)
    {
        Debug.Log("�ǰ�");
        if (magicCircle.HitFlag == false)
        {
            if (weak)
            {
                Debug.Log("����");
                magicCircle.MonsterTakeDamageCalculation(damage);
            }
            else
            {
                Debug.Log("�Ϲ�");
                magicCircle.MonsterTakeDamageCalculation(damage);
            }

            DamageText(weak, damage);
            magicCircle.HitFlag = true;
            magicCircle.StartHitFlagCoroutine();

            return true;
        }
        else
        {
            return false;
        }
    }

    public bool GetHitFlag()
    {
        return magicCircle.HitFlag;
    }


    public void DamageText(bool isWeak, float damage)
    {
        // ���� ���� ������ ��Ʈ ����
        // ������ set damge�� ����
        // ������ color ���� (�����̸� ����/�ƴϸ� �Ͼ��)
        Debug.Log(isWeak);
        Debug.Log(damage);
        StartCoroutine(OnDamageText(isWeak, damage));
        damageText.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 2, 0));
    }

    public IEnumerator OnDamageText(bool isWeak, float damage)
    {
        if (isWeak)
        {
            damage = damage * 2f;
            damageText.fontSize = 70;
            //damageText ����
            damageText.text = "<b>" + damage.ToString() + "</b>";
        }
        else if (!isWeak)
        {
            damageText.fontSize = 60;
            //damageText ���� �ʰ�
            damageText.text = damage.ToString();
        }
        canvas.SetActive(true);
        float colorHpF = (magicCircle.magicCircleNowHp / magicCircle.magicCircleSetHp) * 255;
        byte colorHpB = (byte)colorHpF;

        damageText.color = new Color32(255, colorHpB, colorHpB, 255);

        for (int i = damageText.fontSize; i >= 30; i--)
        {
            damageText.fontSize = i;
            yield return new WaitForFixedUpdate(); // ���� FixedUpdte���� ��ٸ�
        }

        yield return new WaitForSeconds(0.2f);
        damageText.text = "";
    }
}

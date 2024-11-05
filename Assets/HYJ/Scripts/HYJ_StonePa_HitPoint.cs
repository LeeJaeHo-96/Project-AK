using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HYJ_StonePa_HitPoint : MonoBehaviour
{
    [SerializeField] HYJ_StonePa stonePa;
    [SerializeField] bool weak;

    [Header("������ �ؽ�Ʈ ����")]
    [SerializeField] public GameObject canvas;
    [SerializeField] public Text damageText;

    private void Awake()
    {
        stonePa = GetComponent<HYJ_StonePa>();
    }

    public bool TakeDamage(float damage)
    {
        Debug.Log("�ǰ�");
        if (stonePa.HitFlag == false)
        {

            if (weak)
            {
                Debug.Log("����");
                stonePa.MonsterTakeDamageCalculation(damage * 2f);
            }
            else
            {
                Debug.Log("�Ϲ�");
                stonePa.MonsterTakeDamageCalculation(damage);
            }
            DamageText(weak, damage);
            stonePa.HitFlag = true;
            stonePa.StartHitFlagCoroutine();

            return true;
        }
        else
        {
            return false;
        }
    }

    public bool GetHitFlag()
    {
        return stonePa.HitFlag;
    }

    public void DamageText(bool isWeak, float damage)
    {
        // ���� ���� ������ ��Ʈ ����
        // ������ set damge�� ����
        // ������ color ���� (�����̸� ����/�ƴϸ� �Ͼ��)
        Debug.Log(isWeak);
        Debug.Log(damage);
        //StartCoroutine(OnDamageText(isWeak, damage));
        //damageText.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 2, 0));
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
        float colorHpF = (stonePa.nowHp / stonePa.setHp) * 255;
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

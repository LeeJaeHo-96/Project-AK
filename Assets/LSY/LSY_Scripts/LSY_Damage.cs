using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LSY_Damage : MonoBehaviour
{
    private float lsy_HP = 100;
    private Color curColor;
    private readonly Color initColor = Color.green;

    public float curHp = 100;

    public TextMeshProUGUI curHPUI;

    public Image hpBar;

    public Image bloodImage;
    private Coroutine bloodCoroutine;

    public Image shieldImage;
    private Coroutine shieldCoroutine;

    private void Start()
    {
        curColor = initColor;
        hpBar.color = initColor;
        curHPUI.text = $"{curHp}";
    }

    // Todo : ���Ƿ� ���� �浹���� �� UIȰ��ȭ �ǰ� �س���
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            DisplayHpBar();
            curHPUI.text = $"{curHp}";

            // Todo : �� �κ��� �� ���� �� �ǰ��� ������ ��� ����ǵ���
            // Comment : ���ο� �ǰ��� ���� ��� �����ϴ� �ڷ�ƾ�� ���߰� ����۵ǵ���
            if (bloodCoroutine != null)
            {
                StopCoroutine(bloodCoroutine);
            }
            bloodCoroutine = StartCoroutine(ShowBloodScreen());

            // Todo : �� �κ��� �� ������ �� �ǰ� ������ ����ǵ���
            // Comment : ���ο� �ǰ��� ���� ��� �����ϴ� �ڷ�ƾ�� ���߰� ����۵ǵ���
            if (shieldCoroutine != null)
            {
                StopCoroutine (shieldCoroutine);
            }
            shieldCoroutine = StartCoroutine(ShowShieldScreen());
        }
    }

    // Comment : hp�� ���� ü�¹�UI �̹��� ����
    private void DisplayHpBar()
    {
        float hpPercentage = curHp / lsy_HP;

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

    // Comment : �÷��̾ �ǰ� ���� �� �������� �ǰ�ȿ��
    IEnumerator ShowBloodScreen()
    {
        bloodImage.color = new Color(1, 0, 0, UnityEngine.Random.Range(0.9f, 1f));

        float duration = 1.5f;
        float elapsedTime = 0f;
        Color initialColor = bloodImage.color;

        // Commet : 1.5�� ���� ���� �̹����� ������������ ����
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(initialColor.a, 0, elapsedTime / duration);
            bloodImage.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            yield return null; 
        }

        bloodImage.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0);
    }

    IEnumerator ShowShieldScreen()
    {
        shieldImage.color = new Color(0, 0, 1, UnityEngine.Random.Range(0.9f, 1f));

        float duration = 1.5f;
        float elapsedTime = 0f;
        Color initialColor = shieldImage.color;

        // Commet : 1.5�� ���� ���� �̹����� ������������ ����
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(initialColor.a, 0, elapsedTime / duration);
            shieldImage.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            yield return null;
        }

        shieldImage.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0);
    }

}

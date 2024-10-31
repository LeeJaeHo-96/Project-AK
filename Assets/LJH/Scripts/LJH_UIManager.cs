using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class LJH_UIManager : MonoBehaviour
{
    [Header("���� ������ UI")]
    [SerializeField] GameObject[] ljh_shieldImages;     // ������ UI��

    [Header("�ִ� ü��")]
    private float ljh_MaxHP = 100;
    
    [Header("ü�¹� ��")]
    private Color ljh_curColor;
    private readonly Color ljh_initColor = Color.green;
    
    [Header("���� ü��")]
    [Range (0,100)]
    [SerializeField] float ljh_curHp = 100;

    [Header("ü�¹� �̹���")]
    [SerializeField] public Image ljh_hpBar;

    private void Start()
    {
        ljh_curColor = ljh_initColor;
        ljh_hpBar.color = ljh_initColor;
    }

    private void Update()
    {
        DisplayHpBar();
    }

    public void UpdateShieldUI(float durability)
    {
        durability = Mathf.Clamp(durability, 0, ljh_shieldImages.Length);
        for (int i = 0; i < ljh_shieldImages.Length; i++)
        {
            ljh_shieldImages[i].gameObject.SetActive(i < durability);
        }
    }

    public void DisplayHpBar()
    {
        float hpPercentage = ljh_curHp / ljh_MaxHP;
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
}
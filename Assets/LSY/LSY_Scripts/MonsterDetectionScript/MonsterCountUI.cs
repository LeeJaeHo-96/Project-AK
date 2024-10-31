using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class MonsterCountUI : MonoBehaviour
{
    // Comment : ���� �浹ü�� �����Ǵ� ���Ϳ� ������ �浹ü�� �����Ǵ� ������ ���ڸ� ����
    public int[] counters = new int[2];

    public Dictionary<HYJ_Enemy, ColliderType> Enemies = new();

    public bool isEntered;

    public Dictionary<HYJ_Enemy, bool> isEnter = new Dictionary<HYJ_Enemy, bool>();

    [Header("���� ī��Ʈ UI")]
    public TextMeshProUGUI rightCount;
    public TextMeshProUGUI leftCount;


    private void Update()
    {
        UpdateScoreText();
    }
    private void UpdateScoreText()
    {
        // Comment : �浹ü���� ���� ���ڸ� ��� ������Ʈ�ؼ� UI���� ������
        rightCount.text = counters[0].ToString();
        leftCount.text = counters[1].ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonsterCount : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshPro;
    int score = 0;

    private void Start()
    {
        textMeshPro.text = score.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            score++;
            textMeshPro.text = score.ToString();
            if (other.GetComponent<UnitToScreenBoundary>() != null)
            other.GetComponent<UnitToScreenBoundary>().isActiveUI = true;
        }

        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            score--;
            textMeshPro.text = score.ToString();
            // ���Ͱ� �ױ��������� ��� ȭ��� ������ UI �̹��� Ȱ��ȭ? 
            //other.GetComponent<UnitToScreenBoundary>().isActiveUI = false;
        }

    }
}

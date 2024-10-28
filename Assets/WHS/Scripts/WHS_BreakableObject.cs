using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WHS_BreakableObject : MonoBehaviour
{
    // �ı��� ������Ʈ�� Fracture ������Ʈ�� �Բ� �߰�

    [SerializeField] GameObject itemPrefab;

    void Start()
    {
        // FractureManager�� ��ųʸ��� ����ؼ�, ����ȭ �� ���ŵǰ� ����
        FractureManager.Instance.GetFractureObject(gameObject);
    }

    // �ı� �� ��Ȱ��ȭ �� ������Ʈ�� �ڸ��� itemPrefab ����
    private void OnDisable()
    {
        if (itemPrefab != null)
        {
            Instantiate(itemPrefab, transform.position, Quaternion.identity);
        }
    }

}
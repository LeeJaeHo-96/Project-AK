using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WHS_BreakableObject : MonoBehaviour
{
    // �ı��� ������Ʈ�� Fracture ������Ʈ�� �Բ� �߰��ؼ� ���

    [Header("������ ������ ���")]
    [Tooltip("�ı� �� ������ ������ ��� (����θ� ���� X)")]
    [SerializeField] GameObject itemPrefab; // ������ ������ ���

    private void Start()
    {
        // FractureManager�� ��ųʸ��� ����� ������Ʈ�� �ı��ϰ� ������ ����
        WHS_FractureManager.Instance.GetFractureObject(gameObject, itemPrefab);
    }

    // OnDestroy()�� �������� �����ϴ� Fracture �� n�ʵ� �����Ǽ� ������ ������ �ʰ� �Ǵ� ����
    // OnDisable()�� �����ص� ���ۿ� ������ ���ܼ� FractureManager���� �����ϰ���
}
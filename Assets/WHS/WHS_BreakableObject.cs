using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WHS_BreakableObject : MonoBehaviour
{
    // �ı��� ������Ʈ�� Fracture ������Ʈ�� �Բ� �߰�

    void Start()
    {
        // FractureManager�� ��ųʸ��� ����ؼ�, ����ȭ �� ���ŵǰ� ����
        FractureManager.Instance.GetFractureObject(gameObject);
    }

}
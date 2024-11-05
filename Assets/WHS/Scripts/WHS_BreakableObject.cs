using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Fracture))]
public class WHS_BreakableObject : MonoBehaviour
{
    // �ı��� ������Ʈ�� Fracture ������Ʈ�� �Բ� �߰��ؼ� ���
    private void Start()
    {
        // FractureManager�� ��ųʸ��� ����� ������Ʈ�� �ı��ϰ� ������ ����
        WHS_FractureManager.Instance.GetFractureObject(gameObject);
    }

    // OnDestroy()�� �������� �����ϴ� Fracture �� n�ʵ� �����Ǽ� ������ ������ �ʰ� �Ǵ� ����
    // OnDisable()�� �����ص� ���ۿ� ������ ���ܼ� FractureManager���� �����ϰ���
}
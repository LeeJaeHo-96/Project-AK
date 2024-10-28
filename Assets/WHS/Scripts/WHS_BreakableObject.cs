using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WHS_BreakableObject : MonoBehaviour
{
    // �ı��� ������Ʈ�� Fracture ������Ʈ�� �Բ� �߰��ؼ� ���

    [SerializeField] GameObject itemPrefab; // ������ ������ ���

    private void Start()
    {
        // FractureManager�� ��ųʸ��� ����� ������Ʈ�� �ı��ϰ� ������ ����
        WHS_FractureManager.Instance.GetFractureObject(gameObject, itemPrefab);
    }

    // OnDestroy()�� �������� �����ϴ� Fracture �� n�ʵ� �����Ǽ� ������ ������ �ʰ� �Ǵ� ����
    // OnDisable()�� �����ص� ���ۿ� ������ ���ܼ� FractureManager���� �����ϰ���

    /*
    private void OnDestroy()
    {
        Vector3 dropPos = obj.transform.position + new Vector3(0, 1f, 0);
        Instantiate(itemPrefab, dropPos, Quaternion.identity); // ������Ʈ�� �ı��� �ڸ��� 1m���̿� ������ ����
    }
    */
}
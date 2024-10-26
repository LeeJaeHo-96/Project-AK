using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WHS_BreakableObject : MonoBehaviour
{
    // �ı� ������ ���� - �ٸ� ������� ���е� �׵θ��� �β��� ��¦�̰� �� Ȯ�� ����

    // �ı��ϸ� ���������� ������ų� ��� �����

    // �߻�� ����(�Ѿ� ��)�� ������ ������Ʈ �μ���

    private Fracture fracture;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                fracture = hit.transform.GetComponent<Fracture>();
                if (fracture != null)
                {
                    Debug.Log("�μ���");
                    fracture.CauseFracture(); // ������Ʈ �μ���
                }
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet")) // �Ѿ� �±׿� �浹�ϸ�
        {
            Debug.Log("�Ѿ˰� �浹");
            if (fracture != null) // fracture�� ������
            {
                fracture.CauseFracture(); // ������Ʈ �μ���
            }
            Destroy(collision.gameObject);
        }
    }
}
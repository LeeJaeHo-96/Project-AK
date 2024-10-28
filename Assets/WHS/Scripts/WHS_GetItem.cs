using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WHS_GetItem : MonoBehaviour
{
    [SerializeField] float hoverRange; // ���ִ� ����
    [SerializeField] float moveSpeed; // �����̴� �ӵ�

    [SerializeField] int bulletAmount; // ȹ���� �Ѿ� ����

    // �������� ������Ʈ(����)�� �ı��� �ڸ��� ����
    // Ư�� �������� ������ ȹ�� ( VR��Ʈ�ѷ��� Ʈ����? ���ʵ� ȸ��? )
    // -> 1�� �� ȸ��

    private Vector3 startPos; // ������ ���� ��ġ

    private void Start()
    {
        startPos = transform.position; // ������ ��ġ�� ����
    }

    private void Update()
    {
        // hoverRange�� ���Ʒ��� �����̴� ����, moveSpeed�� �̵� �ӵ� ����
        float newY = startPos.y + Mathf.Sin(Time.time * moveSpeed) * hoverRange; // sin�Լ��� ���Ʒ��� �̵��ϴ� ȿ��
        transform.position = new Vector3(transform.position.x, newY, transform.position.z); // y ��ġ�� �̵���Ŵ
    }

}

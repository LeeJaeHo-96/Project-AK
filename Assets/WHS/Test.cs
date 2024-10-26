using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject bulletPrefab; // �Ѿ� ������
    public Transform shootPos;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���콺 Ŭ�� ��
        {
            Shoot();
        }

        float move = Input.GetAxis("Horizontal"); // A, D Ű �Է� ����
        Vector3 movement = new Vector3(move, 0f, 0f) * 5 * Time.deltaTime;
        transform.position += movement; // ������Ʈ �̵�
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, shootPos.position, shootPos.rotation); // ���濡 �Ѿ� ����
    }
}

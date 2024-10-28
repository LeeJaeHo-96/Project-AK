using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject bulletPrefab; // �Ѿ� ������
    public Transform shootPos;

    // WASD�� �̵��ؼ� Ŭ������ �Ѿ� �߻�, ������Ʈ �ı� �� ������ �׽�Ʈ

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(x, y, 0f) * 5 * Time.deltaTime;
        transform.position += movement; 
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, shootPos.position, shootPos.rotation); // ���濡 �Ѿ� ����
    }
}

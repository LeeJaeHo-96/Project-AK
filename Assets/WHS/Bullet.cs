using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;

    private Rigidbody rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody>(); // Rigidbody ������Ʈ�� ������
        rigid.AddForce(transform.forward * speed, ForceMode.Impulse);
    }
    
}
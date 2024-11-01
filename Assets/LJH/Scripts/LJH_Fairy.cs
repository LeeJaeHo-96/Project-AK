using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LJH_Fairy : MonoBehaviour
{
    [Header("������Ʈ")]
    [Header("ĳ���� ������Ʈ")]
    [SerializeField] GameObject character;

    [Header("����")]
    [Header("�� ��ġ �̵� �� ����")]
    [SerializeField] bool fairyWithCharacter;  // ĳ���Ϳ� �ش� ���� �־������, ���� ���� �̺�Ʈ ������ Ʈ�� ���� �������� �Ƚ�
    [Header("�� ����Ÿ� �� ����")]
    [SerializeField] bool fairyfixed;  // ĳ���Ϳ� �ش� ���� �־������, ���� ���� �̺�Ʈ ������ Ʈ�� ���� �������� �Ƚ�


    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        fairyfixed = false;
    }
    void Update()
    {
        fairyWithCharacter = character.GetComponent<FairyTest>().fairyWithCharacter;

        if (character.transform.position.z - transform.position.z == 3)
        {
            character.GetComponent<FairyTest>().fairyWithCharacter = true;
        }




        if (fairyWithCharacter)
        {
            this.gameObject.transform.position = character.transform.position + new Vector3(1, 1, 1);
            character.GetComponent<FairyTest>().fairyWithCharacter = false;
            fairyfixed = true;
        }

        if (fairyfixed)
        {
            if (this.transform.position.x - character.transform.position.x > 0)
            {
                rb.AddForce(new Vector3(-2, 0, 0));
            }
            else if (this.transform.position.x - character.transform.position.x < 0)
            {
                rb.AddForce(new Vector3(2, 0, 0));
            }
        }
    }
}

using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStop : MonoBehaviour
{
    [SerializeField] CinemachineDollyCart cinemachineDollyCart;
    [SerializeField] GameObject[] monsters = new GameObject[6];
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("StopPoint"))
        {
            cinemachineDollyCart.m_Speed = 0;
            Debug.Log("���ǵ� 0");
        }
    }

    private void Update()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        //TODO : �� �ʰ� ���� �����ؼ� �ؾ� �ҵ�? ���� �� óġ�� ���ǵ� �ö󰡵���
        //if (monsters.Length == 0)
        //{
        //    cinemachineDollyCart.m_Speed = 2;
        //}
    }
}

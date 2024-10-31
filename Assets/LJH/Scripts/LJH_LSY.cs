using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LJH_LSY : MonoBehaviour
{
    [SerializeField] CinemachineDollyCart cinemachineDollyCart;
    public float radius = 0f;
    public LayerMask layer; // TODO : Enemy, EliteEnemy ���̾�� �����ؾ� �� EliteEnemy Layer �߰��ؾ� ��
    public Collider[] colliders;
    int i = 0;
    void Update()
    {
        // Comment : �÷��̾� ���� ���� Enemy���̾ ���� ������Ʈ�� ã�� �Լ� ����, ���Ͱ� �������� �ʴ´ٸ� ���� ���·� �ʱ�ȭ
        colliders = Physics.OverlapSphere(transform.position, radius, layer);
        PlayerMove();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("StopPoint"))
        {
            cinemachineDollyCart.m_Speed = 0;
            Debug.Log("���ǵ� 0");
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    // Comment : �÷��̾� �ֺ� OverlapSphere �� �����Ǵ� Enemy, EliteEnemy���̾ ���ٸ� �ٽ� ����ϵ��� ��
    private void PlayerMove()
    {
        if (colliders.Length == 0)
        {
            cinemachineDollyCart.m_Speed = 2;
        }
    }
}

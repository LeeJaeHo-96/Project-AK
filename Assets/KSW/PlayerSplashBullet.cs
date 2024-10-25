using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSplashBullet : PlayerBullet
{
    [SerializeField] float splashRadius;

    protected override void HitBullet()
    {

        //TODO : ���̾� ����ũ �߰�

        Collider[] colliders = Physics.OverlapSphere(transform.position, splashRadius);

        foreach (Collider collider in colliders)
        {
           // TODO : ������ ����
        }

        ReturnBullet();

        
    }

    // Comment : ���� Ȯ��
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(this.transform.position, splashRadius);
    }

}

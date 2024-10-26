using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum BulletType
{
    NORMAL = 1 << 0,
    PIERCE = 1 << 1,
    SPLASH = 1 << 2
}

public class PlayerBullet : MonoBehaviour
{



    private Rigidbody rigidBody;
    [SerializeField] BulletType type;
    [SerializeField] float returnDelay;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private int defaultPierceCount;
    [SerializeField] private int pierceCount;

 
    [SerializeField] private float splashRadius;

    private PlayerGun playerGun;

    private WaitForSeconds returnWaitForSeconds;
    private Coroutine returnCoroutine;



    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        returnWaitForSeconds = new WaitForSeconds(returnDelay);
    }

    public void MoveBullet()
    {
        pierceCount = defaultPierceCount;
        returnCoroutine = StartCoroutine(ReturnTime());
        rigidBody.velocity = transform.forward * bulletSpeed;
    
    }

    public void SetPlayerGun(PlayerGun _playerGun)
    {
        playerGun = _playerGun;
    }

    // Comment : ������Ʈ Ǯ ȸ��
    public void ReturnBullet()
    {
        if (returnCoroutine != null)
        {
            StopCoroutine(returnCoroutine);
        }

        gameObject.SetActive(false);
        rigidBody.velocity = Vector3.zero;
        playerGun.EnqueueBullet(this);
    }

    private void HitBullet()
    {
        if (type.HasFlag(BulletType.PIERCE))
        {
            pierceCount--;
        }
        if (type.HasFlag(BulletType.SPLASH))
        {
            Splash();
        }
        if (pierceCount <= 0)
        {
            ReturnBullet();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // TODO : ������ ����

        HitBullet();
    }

    IEnumerator ReturnTime()
    {
        yield return returnWaitForSeconds;
        ReturnBullet();
    }


    private void Splash()
    {

        //TODO : ���̾� ����ũ �߰�

        Collider[] colliders = Physics.OverlapSphere(transform.position, splashRadius);

        foreach (Collider collider in colliders)
        {
            // TODO : ������ ����
        }

    }

    /*
    // Comment : ���÷��� ���� Ȯ��
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(this.transform.position, splashRadius);
    }
    */
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private Transform muzzle;

    [SerializeField] private GameObject bulletPrefab;


    [SerializeField] private float firingDelay;
    [SerializeField] private float firingCoolDown;

    [SerializeField] private int bulletPoolSize;

    [SerializeField] private GameObject aim;

    private Queue<PlayerBullet> playerBullets;

   
    private Coroutine firingCoroutine;

    [SerializeField] private LayerMask mask;

    private void Update()
    {
        MoveAim();
    }

    private void Awake()
    {
        aim = GameObject.Find("Aim");
        playerBullets = new Queue<PlayerBullet>();
       
    }

    private void Start()
    {
        SetBullet();
    }

    public void OnFireCoroutine()
    {
        CoroutineCheck();
        firingCoroutine = StartCoroutine(Firing());
    }
    public void OffFireCoroutine()
    {
        CoroutineCheck();

        firingCoroutine = StartCoroutine(BackgroundFiringCooldown());
    }

    private void CoroutineCheck()
    {
        if (firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);

        }
    }

    IEnumerator Firing()
    {
        while (true)
        {
            firingCoolDown -= Time.deltaTime;

            if (firingCoolDown <= 0)
            {
                Fire();
                firingCoolDown = firingDelay;
            }
            yield return null;
            
           
        }
    }

    // Comment : �߻� ���� �ƴҶ� �߻� ��ٿ� ���� 
    IEnumerator BackgroundFiringCooldown()
    {
        while (firingCoolDown > 0)
        {
            firingCoolDown -= Time.deltaTime;
            yield return null;


        }
    }

    // Comment : �Ѿ� ������Ʈ Ǯ��
    void SetBullet()
    {
        for (int i = 0; i < bulletPoolSize; i++)
        {
           GameObject bullet = Instantiate(bulletPrefab);
           PlayerBullet playerBullet = bullet.GetComponent<PlayerBullet>();
           playerBullets.Enqueue(playerBullet);
           playerBullet.SetPlayerGun(this);
           playerBullet.gameObject.SetActive(false);
       
        }
    }

    void Fire()
    {
   
        if (playerBullets.Count <= 0)
            return;

        PlayerBullet playerBullet = playerBullets.Dequeue();
        playerBullet.transform.position = muzzle.position;
        playerBullet.transform.rotation = muzzle.rotation;
        playerBullet.gameObject.SetActive(true);
        playerBullet.MoveBullet();
    }

    // Comment : ȸ�� �� �Ѿ� pool�� ����
    public void EnqueueBullet(PlayerBullet playerBullet)
    {
        playerBullets.Enqueue(playerBullet);
    }

    // Comment : ������ �̵�
    // TODO : �Ͻ������� Bullet�� UI ���̾� �ο�, ���� ���̾� ���� �� ����ũ ���̾� ���� �ʿ� 
    // ����ũ ���̾ �ٸ����� �����ؼ� �ϳ��� ����ϴ°͵� �ʿ�

    public void MoveAim()
    {

        RaycastHit hit;

        if (Physics.Raycast(muzzle.position, muzzle.forward, out hit, 100f, mask))
        {
           
            aim.transform.position = hit.point;

        }
        else
        {
            aim.transform.position = Vector3.zero;
        }
      
    }

}

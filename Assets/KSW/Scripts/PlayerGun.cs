using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerGun : MonoBehaviour
{
    private PlayerGunStatus playerGunStatus;

    private PlayerBulletCustom customBullet;
    public PlayerBulletCustom CustomBullet { get { return customBullet; } }

    [SerializeField] private Transform muzzle;

    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private float firingCoolDown;

    [SerializeField] private int bulletPoolSize;

    [SerializeField] private float bulletReturnDelay;

    public float BulletReturnDelay { get { return bulletReturnDelay; } }

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
        customBullet = GetComponent<PlayerBulletCustom>();
        playerGunStatus = GetComponent<PlayerGunStatus>();
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
                firingCoolDown = playerGunStatus.FiringDelay;
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

    public void Fire()
    {
        if (playerGunStatus.Magazine <= 0)
            return;
        if (playerBullets.Count <= 0)
            return;

        PlayerBullet playerBullet = playerBullets.Dequeue();
        playerBullet.transform.position = muzzle.position;
        playerBullet.transform.rotation = muzzle.rotation;
        playerBullet.gameObject.SetActive(true);
        playerBullet.MoveBullet();
        playerGunStatus.Magazine--;
    }

    public void Reload()
    {
        if(playerGunStatus.MaxMagazine <= playerGunStatus.Magazine)
        return;

        playerGunStatus.Magazine = playerGunStatus.MaxMagazine;
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

    private void OnDisable()
    {
        StopAllCoroutines();
    }

}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
  
    // Comment : �߻� ����Ʈ
    [Header("- �߻� ����Ʈ")]
    [SerializeField] private GameObject fireEffect;

    // Comment : ������Ʈ
    private PlayerGunStatus playerGunStatus;

    private PlayerBullet playerBullet;

    private PlayerBulletCustom customBullet;

    private LineRenderer aimLineRenderer;

    private Animator animator;


    // Comment : �ѱ� ��ġ
    [Header("- �ѱ� ��ġ")]
    [SerializeField] private Transform muzzle;

    // Comment : UI
    [Header("- UI")]
    [SerializeField] private TextMeshProUGUI magazineUI;
    [SerializeField] private TextMeshProUGUI toggleMagazineUI;
    [SerializeField] private LayerMask aimMask;
    private GameObject aim;

    // Comment : ����ĳ��Ʈ ����Ʈ
  
    RaycastHit aimHit;

    // Comment : �⺻ ���� Ȯ��
    [Header("- �⺻ ���� Ȯ��")]
    [SerializeField] bool isDefaultWeapon;



    // Comment : �߻� ��ٿ�
    [SerializeField] private float firingCoolDown;

    private Coroutine firingCoroutine;
    private Coroutine firingAccelerationCoroutine;
    private WaitForSeconds firingAccelerationWaitForSeconds;

    StringBuilder stringBuilder;

    private void Update()
    {
        MoveAim();
    }


    // Commnet : �ʱ�ȭ
    public void InitGun()
    {
        firingAccelerationWaitForSeconds = new WaitForSeconds(0.1f);

        stringBuilder = new StringBuilder();

        animator = GetComponent<Animator>();
        customBullet = GetComponent<PlayerBulletCustom>();
        playerGunStatus = GetComponent<PlayerGunStatus>();
        playerBullet = GetComponent<PlayerBullet>();
        aimLineRenderer = GetComponent<LineRenderer>();
        aim = GameObject.Find("AimTarget");

    }

    #region �߻�
    public void OnFireCoroutine()
    {
        CoroutineCheck();
        if (customBullet.GunType.HasFlag(GunType.REPEATER))
        {
           
            firingCoroutine = StartCoroutine(Firing());
            firingAccelerationCoroutine = StartCoroutine(FiringAcceleration());
        }
        else
        {
            FiringOnce();
        }
    }
    public void OffFireCoroutine()
    {
      
        if (customBullet.GunType.HasFlag(GunType.REPEATER))
        {
            CoroutineCheck();
            playerGunStatus.FiringDelay = playerGunStatus.DefaultFiringDelay;
            firingCoroutine = StartCoroutine(BackgroundFiringCooldown());
        }
    }

    private void CoroutineCheck()
    {
        if (firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);

        }
        if (firingAccelerationCoroutine != null)
        {
            StopCoroutine(firingAccelerationCoroutine);

        }
    }

    public void Fire()
    {
        if (playerGunStatus.Magazine <= 0)
            return;
       


        // Comment : ���־��� �κ�
        fireEffect.SetActive(false);
        animator.SetTrigger("Shot");
        fireEffect.SetActive(true);

        if (customBullet.GunType.HasFlag(GunType.PIERCE))
        {
            RaycastHit[] hit = Physics.RaycastAll(muzzle.position, muzzle.forward, playerGunStatus.Range, aimMask).OrderBy(hit=>hit.distance).ToArray();
            

            playerBullet.HitRay(hit);

        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(muzzle.position, muzzle.forward, out hit, playerGunStatus.Range, aimMask))
            {
                playerBullet.HitRay(hit);

            }
        }

        playerGunStatus.Magazine--;
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
    void FiringOnce()
    {

        if (firingCoolDown <= 0)
        {
            Fire();
            firingCoolDown = playerGunStatus.FiringDelay;
        }
        firingCoroutine = StartCoroutine(BackgroundFiringCooldown());
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
 
    // Commnet : ���� Ư�� ���� 
    IEnumerator FiringAcceleration()
    {
        // ���Ӱ�
        float accle = (0.1f + playerGunStatus.Tier * 0.2f)/ (playerGunStatus.AccelerationTime*10); 

        // 0.1�� x * 10ȸ �ݺ�
        for (int i = 1; i <= playerGunStatus.AccelerationTime*10; i++)
        {
            // 0.1��
            yield return firingAccelerationWaitForSeconds;

            playerGunStatus.FiringDelay = playerGunStatus.DefaultFiringDelay / (1 + (accle*i));

        }

    }
    #endregion



    #region ������
    public void Reload()
    {
        if (MagazineRemainingCheck())
            return;

        playerGunStatus.Magazine = playerGunStatus.MaxMagazine;

    }

    // Comment : �Ѿ� �ִ� �� ���������� üũ
    public bool MagazineRemainingCheck()
    {
        if (playerGunStatus.MaxMagazine <= playerGunStatus.Magazine)
            return true;

        return false;
    }
    #endregion

    #region UI
    // Comment : �Ѿ� ui ������Ʈ
    public void UpdateMagazineUI(int magazine)
    {

        magazineUI.text = magazine.ToString();

    }
    public void UpdateChangeToggleUI(int magazine)
    {
        stringBuilder.Clear();
        stringBuilder.Append(magazine.ToString());
        stringBuilder.Append("/");


        if (isDefaultWeapon)
        {
            stringBuilder.Append("��");


        }
        else
        {
            stringBuilder.Append(playerGunStatus.MaxMagazine);

        }

        toggleMagazineUI.text = stringBuilder.ToString();

    }
    public void UpdateChangeToggleUI()
    {

        UpdateChangeToggleUI(playerGunStatus.Magazine);
    }



    // Comment : ������ �̵�
    // TODO : �Ͻ������� Bullet�� UI ���̾� �ο�, ���� ���̾� ���� �� ����ũ ���̾� ���� �ʿ� 
    // ����ũ ���̾ �ٸ����� �����ؼ� �ϳ��� ����ϴ°͵� �ʿ�

    public void MoveAim()
    {
        
        if (Physics.Raycast(muzzle.position, muzzle.forward, out aimHit, 100f, aimMask))
        {
            aimLineRenderer.enabled = true;
            aimLineRenderer.SetPosition(0, muzzle.position);
            aimLineRenderer.SetPosition(1, aimHit.point);
            aim.transform.position = aimHit.point;

        }
        else
        {
            aimLineRenderer.enabled = false;
            aim.transform.position = Vector3.zero;
        }

    }
    #endregion


    // Comment : UI �̺�Ʈ �߰�, ����
    private void OnEnable()
    {
        playerGunStatus.OnMagazineChanged += UpdateMagazineUI;
        playerGunStatus.OnMagazineChanged += UpdateChangeToggleUI;
        UpdateMagazineUI(playerGunStatus.Magazine);
    }

    private void OnDisable()
    {
        playerGunStatus.OnMagazineChanged -= UpdateMagazineUI;
        playerGunStatus.OnMagazineChanged -= UpdateChangeToggleUI;
        fireEffect.SetActive(false);
        StopAllCoroutines();
    }

}

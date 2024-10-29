using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{

    // Comment : �߻� ����Ʈ
    [Header("- �߻� ����Ʈ")]
    [SerializeField] private GameObject fireEffect;

    // Comment : ������Ʈ
    private PlayerGunStatus playerGunStatus;

    private PlayerBullet playerBullet;

    private LineRenderer aimLineRenderer;

    private Animator animator;


    // Comment : �ѱ� ��ġ
    [Header("- �ѱ� ��ġ")]
    [SerializeField] private Transform muzzle;

    // Comment : UI
    [Header("- UI ����")]
    [SerializeField] private PlayerWeaponUI weaponUI;
    [SerializeField] private LayerMask aimMask;
    private GameObject aim;

    // Comment : ����ĳ��Ʈ ����Ʈ

    RaycastHit aimHit;


    // Comment : �߻� ��ٿ�
    [SerializeField] private float firingCoolDown;

    private Coroutine firingCoroutine;
    private Coroutine firingAccelerationCoroutine;
    private WaitForSeconds firingAccelerationWaitForSeconds;

    StringBuilder stringBuilder;


    private void Start()
    {
        UpdateMagazine(playerGunStatus.Magazine);
    }

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

        playerGunStatus = GetComponent<PlayerGunStatus>();
        playerBullet = GetComponent<PlayerBullet>();
        aimLineRenderer = GetComponent<LineRenderer>();
        aim = GameObject.Find("AimTarget");

    }

    #region �߻�
    public void OnFireCoroutine()
    {
        CoroutineCheck();
       
        if (playerGunStatus.GunType.HasFlag(GunType.REPEATER))
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

        if (playerGunStatus.GunType.HasFlag(GunType.REPEATER))
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
    


        // Comment : ���־��� �κ�
        fireEffect.SetActive(false);
        animator.SetTrigger("Shot");
        fireEffect.SetActive(true);


        // ����
        if (playerGunStatus.GunType.HasFlag(GunType.PIERCE))
        {
            RaycastHit[] hit = Physics.RaycastAll(muzzle.position, muzzle.forward, playerGunStatus.Range, aimMask).OrderBy(hit => hit.distance).ToArray();


            playerBullet.HitRay(hit, muzzle);

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

    // Commnet : �����
    IEnumerator Firing()
    {
        while (true)
        {
            if (playerGunStatus.Magazine <= 0)
            {
                CooldownCheck();
                break;
            }

            firingCoolDown -= Time.deltaTime;

            weaponUI.UpdateFiringCooltimeUI(firingCoolDown);
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
        if (playerGunStatus.Magazine <= 0)
        {
            CooldownCheck();
            return;
        }
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
            weaponUI.UpdateFiringCooltimeUI(firingCoolDown);
            yield return null;


        }
    }

    // Comment : Ʈ���� �Է� �������� ��ٿ� üũ

    void CooldownCheck()
    {
        CoroutineCheck();
        if (firingCoolDown > 0)
        {
            firingCoroutine = StartCoroutine(BackgroundFiringCooldown());
        }
    }


    // Commnet : ���� Ư�� ���� 
    IEnumerator FiringAcceleration()
    {
        // ���Ӱ�
        float accle = playerGunStatus.AccelerationRate / (playerGunStatus.AccelerationTime * 10);

        // 0.1�� x * 10ȸ �ݺ�
        for (int i = 1; i <= playerGunStatus.AccelerationTime * 10; i++)
        {
            // 0.1��
            yield return firingAccelerationWaitForSeconds;

            playerGunStatus.FiringDelay = playerGunStatus.DefaultFiringDelay / (1 + (accle * i));

        }

    }
    #endregion



    #region ������
    public void Reload(int index)
    {
        if (MagazineRemainingCheck())
            return;

        if(index == -1)
        {
            playerGunStatus.Magazine = playerGunStatus.MaxMagazine;
            return;
        }
     

        int amount = PlayerSpecialBullet.Instance.SpecialBullet[index];

        if(amount + playerGunStatus.Magazine < playerGunStatus.MaxMagazine)
        {
            amount = amount + playerGunStatus.Magazine;

            PlayerSpecialBullet.Instance.SpecialBullet[index] = 0;
        }
        else if (amount + playerGunStatus.Magazine >= playerGunStatus.MaxMagazine)
        {
            amount = playerGunStatus.MaxMagazine;
            PlayerSpecialBullet.Instance.SpecialBullet[index] -= (playerGunStatus.MaxMagazine - playerGunStatus.Magazine);
        }

        playerGunStatus.Magazine = amount;

    }

    // Comment : �Ѿ� �ִ� �� ���������� üũ
    public bool MagazineRemainingCheck()
    {
        if (playerGunStatus.MaxMagazine <= playerGunStatus.Magazine)
            return true;

        return false;
    }

    public float GetReloadSpeed()
    {

        return playerGunStatus.ReloadSpeed;
    }


    #endregion

    #region UI ����
    // Comment : �Ѿ� ui ������Ʈ

    public int GetMagazine()
    {
        return playerGunStatus.Magazine;
    }
    public int GetMaxMagazine()
    {
        return playerGunStatus.MaxMagazine;
    }

    public void UpdateMagazine(int magazine)
    {
        weaponUI.UpdateMagazineUI(magazine, playerGunStatus.MaxMagazine);

    }
    public void UpdateMagazine()
    {
        weaponUI.UpdateMagazineUI(playerGunStatus.Magazine, playerGunStatus.MaxMagazine);
        weaponUI.UpdateFiringCooltimeUI(firingCoolDown);

    }

    // Comment : ������ �̵�
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
        playerGunStatus.OnMagazineChanged += UpdateMagazine;
        CooldownCheck();
    }

    private void OnDisable()
    {
        playerGunStatus.OnMagazineChanged -= UpdateMagazine;
        fireEffect.SetActive(false);
        StopAllCoroutines();
    }

}

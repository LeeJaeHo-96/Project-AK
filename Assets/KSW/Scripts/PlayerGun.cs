using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{

    // Comment : 발사 이펙트
    [Header("- 발사 이펙트")]
    [SerializeField] private GameObject fireEffect;

    // Comment : 컴포넌트
    private PlayerGunStatus playerGunStatus;

    private PlayerBullet playerBullet;

    private LineRenderer aimLineRenderer;

    private Animator animator;


    // Comment : 총구 위치
    [Header("- 총구 위치")]
    [SerializeField] private Transform muzzle;

    // Comment : UI
    [Header("- UI 관리")]
    [SerializeField] private PlayerWeaponUI weaponUI;
    [SerializeField] private LayerMask aimMask;
    private GameObject aim;

    // Comment : 레이캐스트 포인트

    RaycastHit aimHit;

    // Comment : 기본 무기 확인
    [Header("- 기본 무기 확인")]
    [SerializeField] bool isDefaultWeapon;



    // Comment : 발사 쿨다운
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


    // Commnet : 초기화
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

    #region 발사
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
        if (playerGunStatus.Magazine <= 0)
            return;



        // Comment : 비주얼적 부분
        fireEffect.SetActive(false);
        animator.SetTrigger("Shot");
        fireEffect.SetActive(true);

        if (playerGunStatus.GunType.HasFlag(GunType.PIERCE))
        {
            RaycastHit[] hit = Physics.RaycastAll(muzzle.position, muzzle.forward, playerGunStatus.Range, aimMask).OrderBy(hit => hit.distance).ToArray();


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



    // Comment : 발사 중이 아닐때 발사 쿨다운 감소 
    IEnumerator BackgroundFiringCooldown()
    {
        while (firingCoolDown > 0)
        {
            firingCoolDown -= Time.deltaTime;
            yield return null;


        }
    }

    // Commnet : 연사 특성 가속 
    IEnumerator FiringAcceleration()
    {
        // 가속값
        float accle = playerGunStatus.AccelerationRate / (playerGunStatus.AccelerationTime * 10);

        // 0.1초 x * 10회 반복
        for (int i = 1; i <= playerGunStatus.AccelerationTime * 10; i++)
        {
            // 0.1초
            yield return firingAccelerationWaitForSeconds;

            playerGunStatus.FiringDelay = playerGunStatus.DefaultFiringDelay / (1 + (accle * i));

        }

    }
    #endregion



    #region 재장전
    public void Reload()
    {
        if (MagazineRemainingCheck())
            return;

        playerGunStatus.Magazine = playerGunStatus.MaxMagazine;

    }

    // Comment : 총알 최대 수 보유중인지 체크
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

    #region UI 연동
    // Comment : 총알 ui 업데이트

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


    // Comment : 조준점 이동
    // TODO : 일시적으로 Bullet에 UI 레이어 부여, 추후 레이어 합의 후 마스크 레이어 관리 필요 
    // 마스크 레이어를 다른곳에 정의해서 하나만 사용하는것도 필요

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


    // Comment : UI 이벤트 추가, 제거
    private void OnEnable()
    {
        playerGunStatus.OnMagazineChanged += UpdateMagazine;

    }

    private void OnDisable()
    {
        playerGunStatus.OnMagazineChanged -= UpdateMagazine;
        fireEffect.SetActive(false);
        StopAllCoroutines();
    }

}

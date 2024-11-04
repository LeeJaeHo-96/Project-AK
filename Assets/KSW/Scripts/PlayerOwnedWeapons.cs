using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOwnedWeapons : MonoBehaviour
{
    [SerializeField] private int index;

    public int Index { get { return index; } set { index = value; } }

    // Comment : ����
    [Header("- ������ �Ұ� ����")]
    [SerializeField] private AudioClip reloadDenySound;

    [Header("- UI ����")]
    [SerializeField] private PlayerWeaponUI weaponUI;

    [Header("- �������� ����")]
    [SerializeField] List<PlayerGun> ownedWeapons;
    [Header("- ������� ����")]
    [SerializeField] PlayerGun currentWeapon;
    [Header("- źâ ������Ʈ")]
    [SerializeField] PlayerMagazine magazine;



    [Header("- ���� �� Ȯ��")]
    [SerializeField] private bool ontGrip;


    public bool OntGrip { get { return ontGrip; } set { ontGrip = value; } }

    [Header("- ���ο�")]
    // Commnet : 1 ������ ��� 50% ������ ����
    [SerializeField] private float additionalCoolDown;
    public float AdditionalCoolDown { get { return additionalCoolDown; } set { additionalCoolDown = value; } }


    Coroutine slowCoroutine;
    WaitForSeconds slowWaitForSeconds = new WaitForSeconds(3.0f);
    bool disableFlag;

    private void Awake()
    {
        SetWeapons();
        weaponUI.SetUIPos();
    }

    // Comment : ���� �ʱ�ȭ �Լ� ȣ��
    public void SetWeapons()
    {
        foreach (PlayerGun weapon in ownedWeapons)
        {

            weapon.InitGun();
        }

    }

    // Comment : ������� ���� ��ȯ
    public PlayerGun GetCurrentWeapon()
    {
        return currentWeapon;
    }



    // Comment : �������� ���� ��ȯ
    public PlayerGun GetOwnedWeapons(int _index)
    {
        return ownedWeapons[_index];
    }


    // Comment : �������� ���� �� ��ȯ
    public int GetOwnedWeaponsCount()
    {
        return ownedWeapons.Count - 1;
    }

    // Comment : ���� ��ü
    public void SetCurrentWeapon()
    {

        currentWeapon.gameObject.SetActive(false);
        currentWeapon = ownedWeapons[index];
        currentWeapon.gameObject.SetActive(true);
        weaponUI.SetUIPos();
    }

    // Comment : Ư�� źȯ ��� ������ ȣ���� �⺻ ���� ��ü �Լ�
    public void SetDefaultWeapon()
    {

        currentWeapon.gameObject.SetActive(false);

        index = 0;
        currentWeapon = ownedWeapons[index];
        currentWeapon.gameObject.SetActive(true);
        currentWeapon.UpdateMagazine();

    }

    // Comment : ������
    public void ReloadMagazine()
    {
        currentWeapon.Reload(index - 1);
    }

    public void ReloadGripOnMagazine()
    {
        if (currentWeapon.MagazineRemainingCheck() ||
            index != 0 && PlayerSpecialBullet.Instance.SpecialBullet[index - 1] <= 0 ||
            weaponUI.GetChangeUIActiveSelf())
        {
            AudioManager.Instance.PlaySE(reloadDenySound);
            return;
        }

        magazine.gameObject.SetActive(true);
        ontGrip = true;
    }

    public void ReloadGripOffMagazine()
    {
        magazine.gameObject.SetActive(false);
        ontGrip = false;
    }

    public void SetReloadText()
    {
        magazine.SetTextMagazine();
    }

    // Comment : ���ο� ����
    public void StartSlow()
    {
        if (disableFlag)
            return;

        if (slowCoroutine != null)
            StopCoroutine(slowCoroutine);
        slowCoroutine = StartCoroutine(SlowEnd());
        additionalCoolDown = 1;
    }

    IEnumerator SlowEnd()
    {
        yield return slowWaitForSeconds;
        additionalCoolDown = 0;
    }

    // ���� ó��
    private void OnEnable()
    {
        disableFlag = false;
    }
    private void OnDisable()
    {
        disableFlag = true;
    }

    private void OnDestroy()
    {
        disableFlag = true;
    }
}

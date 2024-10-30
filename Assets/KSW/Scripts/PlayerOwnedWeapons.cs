using System;
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
        return ownedWeapons.Count-1;
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
    }

    public void ReloadGripOffMagazine()
    {
        magazine.gameObject.SetActive(false);
    }

   

}

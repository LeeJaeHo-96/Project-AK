using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOwnedWeapons : MonoBehaviour
{
    [SerializeField] private int index;

    public int Index { get { return index; } set { index = value; } }

    [Header("- �������� ����")]
    [SerializeField] List<PlayerGun> ownedWeapons;
    [Header("- ������� ����")]
    [SerializeField] PlayerGun currentWeapon;
    [Header("- źâ ������Ʈ")]
    [SerializeField] PlayerMagazine magazine;

    private void Awake()
    {
        SetWeapons();
    }

    // Comment : ���� �ʱ�ȭ �Լ� ȣ��
    public void SetWeapons()
    {
        foreach (PlayerGun weapon in ownedWeapons)
        {

            weapon.InitGun();
        }
        ChangeUIUpdate();
    }

    // Comment : ������� ���� ��ȯ
    public PlayerGun GetCurrentWeapon()
    {
        return currentWeapon;
    }

    // Comment : ���� ��ü
    public void SetCurrentWeapon()
    {
       
        currentWeapon.gameObject.SetActive(false);
        currentWeapon = ownedWeapons[index];
        currentWeapon.gameObject.SetActive(true);
    }

    // Comment : �������� ���� �� ��ȯ
    public int GetOwnedWeaponsCount()
    {
        return ownedWeapons.Count-1;
    }

    // Comment : ������
    public void ReloadMagazine()
    {
        currentWeapon.Reload();
    }

    public void ReloadGripOnMagazine()
    {
        if (currentWeapon.MagazineRemainingCheck())
            return;
        magazine.gameObject.SetActive(true);
    }

    public void ReloadGripOffMagazine()
    {
        magazine.gameObject.SetActive(false);
    }

    // Comment : ��ü UI ������Ʈ
    public void ChangeUIUpdate()
    {
        foreach (PlayerGun weapon in ownedWeapons)
        {
         
            weapon.UpdateChangeToggleUI();
        }
    }
}

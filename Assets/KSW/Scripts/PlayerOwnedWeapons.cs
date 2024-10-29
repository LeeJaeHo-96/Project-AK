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
    }
    // Comment : ������
    public void ReloadMagazine()
    {
        currentWeapon.Reload(index - 1);
    }

    public void ReloadGripOnMagazine()
    {
        if (currentWeapon.MagazineRemainingCheck())
            return;
        if (index != 0 && PlayerSpecialBullet.Instance.SpecialBullet[index-1] <= 0)
        {
            return;
        }

        magazine.gameObject.SetActive(true);
    }

    public void ReloadGripOffMagazine()
    {
        magazine.gameObject.SetActive(false);
    }

   

}

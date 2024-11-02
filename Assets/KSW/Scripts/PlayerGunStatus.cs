using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[Flags]
public enum GunType
{
    NORMAL = 1 << 0,
    REPEATER = 1 << 1,
    PIERCE = 1 << 2,
    SPLASH = 1 << 3,

}


public enum Tier
{
    Tier1, Tier2, Tier3

}

public struct ExplainStatus
{


    public string weaponName;
    public string gunType;
    public string magazine;
    public string atk;

    public ExplainStatus(string _weaponName, string _gunType, string _magazine, string _atk)
    {
        weaponName = _weaponName;
        gunType = _gunType;
        magazine = _magazine;
        atk = _atk;
    }
}

public class PlayerGunStatus : MonoBehaviour
{
    // Comment : ���� UI�� ����ü
    ExplainStatus status;

    [Header("- ���� ����")]
    [SerializeField] private WeaponData weaponData;
    [Header("- ���� �߻� ����")]
    [SerializeField] private float firingDelay;
    [Header("- ���� ��ź��")]
    [SerializeField] private int magazine;
    [Header("- ���� Ƚ��")]
    [SerializeField] private int defaultPierceCount;
    [Header("- ���÷��� ����(����)")]
    [SerializeField, Range(0, 3)] private float splashRadius;
    [Header("- ���÷��� ���ݷ�")]
    [SerializeField] private float splashDamage;
    [Header("- ���ӷ�")]
    [SerializeField] private float accelerationRate;


    private string ablityTextString;

    public ExplainStatus Status { get { return status; } }

    public string WeaponName { get { return weaponData.weaponName; } }
    public GunType GunType { get { return weaponData.gunType; } }
    public float BulletAttack { get { return weaponData.bulletAttack; } }

    public float DefaultFiringDelay { get { return weaponData.defaultFiringDelay; } }
    public float FiringDelay { get { return firingDelay; } set { firingDelay = value; } }
    public float AccelerationTime { get { return weaponData.accelerationTime; }  }
    public int MaxMagazine { get { return weaponData.maxMagazine; } }
    public int Magazine { get { return magazine; } set { magazine = value; OnMagazineChanged?.Invoke(magazine); } }

    public UnityAction<int> OnMagazineChanged;

    public float ReloadSpeed { get { return weaponData.reloadSpeed; } }

    public Tier Tier { get { return weaponData.tier; } }

    public float Range { get { return weaponData.range; } }

    public int DefaultPierceCount { get { return defaultPierceCount; } }
    public float SplashRadius { get { return splashRadius; } }
    public float SplashDamage { get { return splashDamage; } }
    public float AccelerationRate { get { return accelerationRate; } }

    public string AblityTextString { get { return ablityTextString; } }

    public void Init()
    {
        ablityTextString = null;
        FiringDelay = DefaultFiringDelay;
        switch (weaponData.tier)
        {
            case Tier.Tier1:
                defaultPierceCount = 2;
                splashRadius = 0.3f;
                accelerationRate = 0.3f;
                splashDamage = BulletAttack * 0.3f;

                break;
            case Tier.Tier2:
                defaultPierceCount = 3;
                splashRadius = 0.5f;
                accelerationRate = 0.5f;
                splashDamage = BulletAttack * 0.5f;
                break;
            case Tier.Tier3:
                defaultPierceCount = 4;
                splashRadius = 1f;
                accelerationRate = 0.7f;
                splashDamage = BulletAttack;
                break;
        }


        if (GunType.HasFlag(GunType.PIERCE))
        {
            ablityTextString += "<sprite name=gantong";
            ablityTextString += ((int)weaponData.tier + 1).ToString() + ">";
        }
        if (GunType.HasFlag(GunType.SPLASH))
        {
            ablityTextString += "<sprite name=pockbal";
            ablityTextString += ((int)weaponData.tier + 1).ToString() + ">";
        }
        if (GunType.HasFlag(GunType.REPEATER))
        {
            ablityTextString += "<sprite name=yeonsa";
            ablityTextString += ((int)weaponData.tier + 1).ToString() + ">";
        }

        status = new ExplainStatus(WeaponName, "Ư�� : " + AblityTextString, "�ִ� źâ : " + MaxMagazine.ToString(), "���ݷ� : " + BulletAttack.ToString());
    }
}

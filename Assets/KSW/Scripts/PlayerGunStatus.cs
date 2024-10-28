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
public class PlayerGunStatus : MonoBehaviour
{
    [Header("- �ѱ� Ư��")]
    [SerializeField] private GunType gunType;
    [Header("- ���� Ƽ��")]
    [SerializeField] private int tier;
    [Header("- ���ݷ�")]
    [SerializeField] private float bulletAttack;
    [Header("- �⺻ �߻� ����")]
    [SerializeField] private float defaultFiringDelay;
    [Header("- ���� �߻� ����")]
    [SerializeField] private float firingDelay;
    [Header("- ���� ���� �ð�")]
    [SerializeField] private float accelerationTime;
    [Header("- �ִ� ��ź��")]
    [SerializeField] private int maxMagazine;
    [Header("- ���� ��ź��")]
    [SerializeField] private int magazine;
    [Header("- ������ �ӵ�")]
    [SerializeField] private float reloadSpeed;
    [Header("- �����Ÿ�")]
    [SerializeField] private float range;
    [Header("- ���� Ƚ��")]
    [SerializeField] private int defaultPierceCount;
    [Header("- ���÷��� ����(����)")]
    [SerializeField, Range(0, 3)] private float splashRadius;
    [Header("- ���÷��� ���ݷ�")]
    [SerializeField] private float splashDamage;
    [Header("- ���ӷ�")]
    [SerializeField] private float accelerationRate;


    public GunType GunType { get { return gunType; } }
    public float BulletAttack { get { return bulletAttack; } }

    public float DefaultFiringDelay { get { return defaultFiringDelay; } }
    public float FiringDelay { get { return firingDelay; } set { firingDelay = value; } }
    public float AccelerationTime { get { return accelerationTime; }  }
    public int MaxMagazine { get { return maxMagazine; } }
    public int Magazine { get { return magazine; } set { magazine = value; OnMagazineChanged?.Invoke(magazine); } }

    public UnityAction<int> OnMagazineChanged;

    public float ReloadSpeed { get { return reloadSpeed; } }

    public int Tier { get { return tier; } }

    public float Range { get { return range; } }

    public int DefaultPierceCount { get { return defaultPierceCount; } }
    public float SplashRadius { get { return splashRadius; } }
    public float SplashDamage { get { return splashDamage; } }
    public float AccelerationRate { get { return accelerationRate; } }


    void Awake()
    {
        switch (tier)
        {
            case 1:
                defaultPierceCount = 2;
                splashRadius = 0.3f;
                accelerationRate = 0.3f;
                splashDamage = bulletAttack * 0.3f;
                break;
            case 2:
                defaultPierceCount = 3;
                splashRadius = 0.5f;
                accelerationRate = 0.5f;
                splashDamage = bulletAttack * 0.5f;
                break;
            case 3:
                defaultPierceCount = 4;
                splashRadius = 1f;
                accelerationRate = 0.7f;
                splashDamage = bulletAttack;
                break;
        }


    }
}

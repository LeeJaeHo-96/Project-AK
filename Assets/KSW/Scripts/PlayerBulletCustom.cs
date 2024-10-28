using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Flags]
public enum GunType
{
    NORMAL = 1 << 0,
    REPEATER = 1 << 1,
    PIERCE = 1 << 2,
    SPLASH = 1 << 3,
   
}
public class PlayerBulletCustom : MonoBehaviour
{
    [Header("- �ѱ� Ư��")]
    [SerializeField] private GunType gunType;
    public GunType GunType { get { return gunType; } }

    [Header("- ���ݷ�")]
    [SerializeField] private float bulletAttack;

    public float BulletAttack { get { return bulletAttack; } }

    [Header("- ���� Ƚ��")]
    [SerializeField] private int defaultPierceCount;

    public int DefaultPierceCount { get { return defaultPierceCount; } }


    [Header("- ���÷��� ����(����)")]
    [SerializeField, Range(0, 3)] private float splashRadius;

    public float SplashRadius { get { return splashRadius; } }

}

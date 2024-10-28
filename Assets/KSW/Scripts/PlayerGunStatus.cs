using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerGunStatus : MonoBehaviour
{
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
    [Header("- ���� Ƽ��")]
    [SerializeField] private int tier;

    public float DefaultFiringDelay { get { return defaultFiringDelay; } }
    public float FiringDelay { get { return firingDelay; } set { firingDelay = value; } }
    public float AccelerationTime { get { return accelerationTime; }  }
    public int MaxMagazine { get { return maxMagazine; } }
    public int Magazine { get { return magazine; } set { magazine = value; OnMagazineChanged?.Invoke(magazine); } }

    public UnityAction<int> OnMagazineChanged;

    public float ReloadSpeed { get { return reloadSpeed; } }

    public int Tier { get { return tier; } }


}

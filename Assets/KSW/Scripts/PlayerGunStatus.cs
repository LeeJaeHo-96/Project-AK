using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerGunStatus : MonoBehaviour
{
    [Header("- �߻� ����")]
    [SerializeField] private float firingDelay;
    [Header("- �ִ� ��ź��")]
    [SerializeField] private int maxMagazine;
    [Header("- ���� ��ź��")]
    [SerializeField] private int magazine;
    [Header("- ������ �ӵ�")]
    [SerializeField] private float reloadSpeed;
    public float FiringDelay { get { return firingDelay; }  }
    public int MaxMagazine { get { return maxMagazine; } }
    public int Magazine { get { return magazine; } set { magazine = value; OnMagazineChanged?.Invoke(magazine); } }

    public UnityAction<int> OnMagazineChanged;

    public float ReloadSpeed { get { return reloadSpeed; } }
}

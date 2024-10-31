using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData", order = int.MaxValue)]
public class WeaponData : ScriptableObject
{
    [Header("- �̸�")]
    [SerializeField] public string weaponName;
    [Header("- �ѱ� Ư��")]
    [SerializeField] public GunType gunType;
    [Header("- ���� Ƽ��")]
    [SerializeField] public Tier tier;
    [Header("- ���ݷ�")]
    [SerializeField] public float bulletAttack;
    [Header("- �⺻ �߻� ����")]
    [SerializeField] public float defaultFiringDelay;

    [Header("- ���� ���� �ð�")]
    [SerializeField] public float accelerationTime;
    [Header("- �ִ� ��ź��")]
    [SerializeField] public int maxMagazine;

    [Header("- ������ �ӵ�")]
    [SerializeField] public float reloadSpeed;
    [Header("- �����Ÿ�")]
    [SerializeField] public float range;
 
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBullet : MonoBehaviour
{
    [Header("- ����ũ ����Ʈ ������")]
    [SerializeField] private GameObject sparkEffectPrefab;
    private List<GameObject> spark;
  
    private int pierceCount;
    private PlayerBulletCustom customBullet;


    [Header("- ���÷��� ���̾� ����ũ")]
    [SerializeField] LayerMask mask;

    private void Awake()
    {
        customBullet = GetComponent<PlayerBulletCustom>();
        SetEffect();
      
    }

    private void SetEffect()
    {
        spark = new List<GameObject>();
        if (customBullet.GunType.HasFlag(GunType.PIERCE))
        {
            for (int i = 0; i < customBullet.DefaultPierceCount; i++)
            {
                spark.Add(Instantiate(sparkEffectPrefab));
                spark[i].SetActive(false);
            }
        }
        else
        {
            spark.Add(Instantiate(sparkEffectPrefab));
            spark[0].SetActive(false);
        }
    }

    public void HitRay(RaycastHit hit)
    {
        HitBullet(hit.point, 0);
    }

    public void HitRay(RaycastHit[] hit)
    {
        pierceCount = customBullet.DefaultPierceCount;

        int loop = pierceCount;
        if (hit.Length < pierceCount)
        {
            loop = hit.Length;
        }



        for (int i = 0; i < loop; i++)
        {
            HitBullet(hit[i].point, i);
        }
    }


    private void HitBullet(Vector3 point, int sparkIndex)
    {
       

        if (customBullet.GunType.HasFlag(GunType.PIERCE))
        {
            pierceCount--;
         
        }
        else
        {
            pierceCount = 0;
        }

        if (customBullet.GunType.HasFlag(GunType.SPLASH))
        {
            Splash(point);
        }
        
        OnEffect(point, sparkIndex);
    }

    private void OnEffect(Vector3 vec, int cnt)
    {

        spark[cnt].SetActive(false);

        spark[cnt].transform.position = vec;
        spark[cnt].transform.LookAt(transform.position);
        spark[cnt].SetActive(true);
    }


    private void Splash(Vector3 vec)
    {

        //TODO : ���̾� ����ũ �߰�

        Collider[] colliders = Physics.OverlapSphere(vec, customBullet.SplashRadius, mask);

        foreach (Collider collider in colliders)
        {
            //Debug.Log(collider.name);
            // TODO : ������ ����
        }

    }

    
 
    
}

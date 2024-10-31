using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class WHS_ItemManager : MonoBehaviour
{

    private static WHS_ItemManager instance;

    [System.Serializable]
    public class ItemInfo
    {
        [Header("������ ������")]
        public GameObject itemPrefab;
        [Header("������ �����")]
        public float dropRate;
    }    
    [SerializeField] List<ItemInfo> itemInfos;
    [Header("������ ���� ����")]
    [SerializeField] float itemHeight = 1f;
    [Header("�������� �� �� Ȯ��")]
    [Range(0, 100)]
    [SerializeField] float noneDropRate = 40;

    public static WHS_ItemManager Instance
    { 
        get
        { 
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // �� �� Ȯ���� �ִ� ���� ������ ���
    private GameObject GetRandomItemWithProbability()
    {
        float randomValue = Random.Range(0, 100f);
        if(randomValue < noneDropRate) // noneDropRate���� ������ �������� �������� ����
        {
            return null;
        }
        return GetRandomItem();
    }

    // �������� ������ ���
    private GameObject GetRandomItem()
    {
        if(itemInfos.Count == 0)
        {
            Debug.Log("��ϵ� �������� ����");
            return null;
        }

        float totalRate = 0;
        foreach(var item in itemInfos)
        {
            totalRate += item.dropRate;
        }

        float randomValue = Random.Range(0, totalRate);
        float curRate = 0;

        foreach(var item in itemInfos)
        {
            curRate += item.dropRate;
            if(randomValue <= curRate)
            {
                return item.itemPrefab;
            }
        }

        return null;
    }

    // ������������ ������ ���� - ����Ʈ����, Ư������
    public void SpawnItem(Vector3 pos)
    {       
        GameObject spawnedItem = GetRandomItem();
        if (spawnedItem != null)
        {
            Vector3 dropPos = pos + new Vector3(0, itemHeight, 0);
            Instantiate(spawnedItem, dropPos, Quaternion.identity);
        }
        else
        {
            Debug.Log("������ ��ϵ��� ����");
        }
    }

    // ������������ Ȯ���� ���� - �Ϲݸ��� ��  
    public void SpawnItemWithProbability(Vector3 pos)
    {
        GameObject spawnedItem = GetRandomItemWithProbability();
        if (spawnedItem != null)
        {
            Vector3 dropPos = pos + new Vector3(0, itemHeight, 0);
            Instantiate(spawnedItem, dropPos, Quaternion.identity);            
        }
        else
        {
            Debug.Log("������ �������� ����");
        }
    }

}

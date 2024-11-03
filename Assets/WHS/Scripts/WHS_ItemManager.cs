using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class WHS_ItemManager : MonoBehaviour
{

    [SerializeField] GameObject itemPrefab;

    [System.Serializable]    
    public class ItemInfo
    {
        [Header("�Ѿ� �ε���")]
        public int bulletIndex;
        [Header("�Ѿ� ����")]
        public int bulletAmount;
        [Header("������ �����")]
        public float dropRate;
    }

    [System.Serializable]
    public class DropInfo
    {
        [Header("���� ����")]
        public string monsterType;
        [Header("������ ���̺�")]
        public List<ItemInfo> items;
    }

    [Header("���� �����")]
    [SerializeField] List<DropInfo> dropInfo;

    [Header("������Ʈ �����")]
    [SerializeField] DropInfo objectDrops;

    private float itemHeight = 1f;

    private static WHS_ItemManager instance;

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
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ������ ������ ���
    public void SpawnItem(Vector3 pos, string monsterType)
    {
        // ������ ���� �޾ƿ���
        DropInfo itemDrops = dropInfo.Find(a => a.monsterType == monsterType);

        if (itemDrops != null)
        {
            ItemInfo selectedItem = GetRandomItem(itemDrops.items);
            if (selectedItem != null)
            {
                // �ε��� -1�϶� ������ �������� ���� (��)
                if (selectedItem.bulletIndex == -1)
                {
                    Debug.Log("������ ȹ������ ����");
                }
                // �ش� �ε����� ������ ȹ��
                else
                {
                    SpawnSelectedItem(pos, selectedItem);
                    Debug.Log($"{selectedItem.bulletIndex + 1}�� �Ѿ� {selectedItem.bulletAmount}�� ����");
                }
            }
        }

        else
        {
            Debug.Log($"Ÿ�� ��ġ���� ���� {monsterType}");
        }
    }

    // ������Ʈ�� ������ ���
    public void SpawnItem(Vector3 pos)
    {
        ItemInfo selectedItem = GetRandomItem(objectDrops.items);

        if (selectedItem != null)
        {
            // -1���̸� �ٽ� ������?
            if (selectedItem.bulletIndex == -1)
            {
                SpawnItem(pos);
            }
            else
            {
                SpawnSelectedItem(pos, selectedItem);
            }
        }
    }


    // ��� ���̺��� ���� ������ ȹ��
    private ItemInfo GetRandomItem(List<ItemInfo> items)
    {
        float totalRate = 0;
        foreach (ItemInfo item in items)
        {
            totalRate += item.dropRate;
        }

        float randomValue = Random.Range(0, totalRate);
        float curRate = 0;

        foreach (ItemInfo item in items)
        {
            curRate += item.dropRate;
            if (randomValue <= curRate)
            {
                return item;
            }
        }

        return null;
    }

    // ������ ����
    private void SpawnSelectedItem(Vector3 Pos, ItemInfo itemInfo)
    {
        Vector3 dropPos = Pos + new Vector3(0, itemHeight, 0);
        GameObject spawnedItem = Instantiate(itemPrefab, dropPos, Quaternion.identity);

        // WHS_Item�� bulletIndex, bulletAmount ����
        WHS_Item item = spawnedItem.GetComponent<WHS_Item>();
        item.SetItemInfo(itemInfo.bulletIndex, itemInfo.bulletAmount);
    }
}

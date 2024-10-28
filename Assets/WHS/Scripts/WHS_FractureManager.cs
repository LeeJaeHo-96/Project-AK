using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WHS_FractureManager : MonoBehaviour
{
    [SerializeField] float removeDelay = 1.5f;
    private static WHS_FractureManager instance; // �ı��� Fracture ������Ʈ���� �ν��Ͻ�
    private Dictionary<GameObject, Fracture> fractureObjects = new Dictionary<GameObject, Fracture>(); // �ı��� ������Ʈ�� Fracture ������Ʈ�� ����
    private Dictionary<GameObject, GameObject> itemPrefabs = new Dictionary<GameObject, GameObject>(); // �ı��� ������Ʈ�� ������ �������� ����

    public static WHS_FractureManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<WHS_FractureManager>(); // �ν��Ͻ��� ������ ������ FractureManager�� ã��
                if (instance == null)
                {
                    instance = new GameObject("WHS_FractureManager").AddComponent<WHS_FractureManager>(); // ���ٸ� FractureManager�� ����
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // ���� �ִ� FractureManager�� �ν��Ͻ��� ����
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Fracture�� ������Ʈ�� ��ųʸ��� ���
    public void GetFractureObject(GameObject obj, GameObject itemPrefab)
    {
        Fracture fracture = obj.GetComponent<Fracture>(); // ������Ʈ�� Fracture ������Ʈ ��������

        if (fracture != null && !fractureObjects.ContainsKey(obj)) // Fracture ������Ʈ�� �ְ�, ���� ��ϵ��� �ʾ�����
        {
            fractureObjects.Add(obj, fracture); // ��ųʸ��� �ı��� ������Ʈ �߰�
            itemPrefabs.Add(obj, itemPrefab); // ��ųʸ��� ������������ �߰�

            fracture.callbackOptions.onCompleted.AddListener(() => FractureOnCompleted(obj)); // Fracture�� �Ϸ�Ǹ� FractureOnCompleted ȣ��
        }
    }

    // Fracture�� �Ϸ�(onCompleted) �Ǹ� ȣ��
    private void FractureOnCompleted(GameObject obj)
    {
        StartCoroutine(RemoveFragments(obj)); // Fracture�� �Ϸ�Ǹ� �ڷ�ƾ ����
    }

    // Fracture �Ϸ� �� ������ ���� �� ���� ����
    private IEnumerator RemoveFragments(GameObject obj)
    {
        // ������ ����
        GameObject itemPrefab = itemPrefabs[obj]; // ������������ �޾ƿ���
        Vector3 dropPos = obj.transform.position + new Vector3(0, 1.5f, 0);
        Instantiate(itemPrefab, dropPos, Quaternion.identity); // ������Ʈ�� �ı��� �ڸ��� 1.5���̿� ������ ����

        // removeDelay�� �� ���� ����
        yield return new WaitForSeconds(removeDelay);

        // ���� ����
        GameObject fragmentRoot = GameObject.Find($"{obj.name}Fragments"); // ������Ʈ�� �̸�+Fragments �̸��� ������ ���� ������Ʈ ã��

        Destroy(fragmentRoot); // ���� ������Ʈ ����
        Destroy(obj); // ���� ������Ʈ ����

        fractureObjects.Remove(obj); // ��ųʸ����� ����
        itemPrefabs.Remove(obj);
    }
}
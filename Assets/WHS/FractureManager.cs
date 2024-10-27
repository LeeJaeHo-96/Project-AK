using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FractureManager : MonoBehaviour
{
    [SerializeField] float removeDelay = 2f;
    private static FractureManager instance; // �ı��� Fracture ������Ʈ���� �ν��Ͻ�
    private Dictionary<GameObject, Fracture> fractureObjects = new Dictionary<GameObject, Fracture>(); // �ı��� ������Ʈ���� ����, ���ӿ�����Ʈ�� Ű, fracture�� ������

    public static FractureManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<FractureManager>(); // ������ FractureManager�� ã��
                if (instance == null)
                {
                    instance = new GameObject("FractureManager").AddComponent<FractureManager>(); // ���ٸ� FractureManager�� ����
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
    public void GetFractureObject(GameObject obj)
    {
        Fracture fracture = obj.GetComponent<Fracture>(); // Fracture ������Ʈ�� ���� ������Ʈ
        if (fracture != null && !fractureObjects.ContainsKey(obj))
        {
            fractureObjects.Add(obj, fracture); // ��ųʸ��� ������Ʈ �߰�
            fracture.callbackOptions.onCompleted.AddListener(() => FractureOnCompleted(obj));
        }
    }

    // Fracture�� �Ϸ�(onCompleted) �Ǹ� ȣ��
    private void FractureOnCompleted(GameObject obj)
    {
        StartCoroutine(RemoveFragments(obj));
    }

    // Fracture�� ������ ������� ������
    private IEnumerator RemoveFragments(GameObject obj)
    {
        yield return new WaitForSeconds(removeDelay); // removeDelay�� �� ������Ʈ ����

        GameObject fragmentRoot = GameObject.Find($"{obj.name}Fragments"); // ������Ʈ�̸�+Fragments �̸��� ������ ���� ������Ʈ ã��
        if (fragmentRoot != null)
        {
            Destroy(fragmentRoot); // ���� ������Ʈ ����
        }

        Destroy(obj); // ���� ������Ʈ ����
        fractureObjects.Remove(obj); // ��ųʸ����� ����
    }
}
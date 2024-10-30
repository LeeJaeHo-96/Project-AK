using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class WHS_FractureManager : MonoBehaviour
{
    // FractureManager���� Fracture�� ������Ʈ�� ������ ����, �������� ������ ������
    // �μ� ������Ʈ�� Fracture ������Ʈ, BreakableObject ��ũ��Ʈ �߰�

    // Fracture Options -> FragmentCount ���� �ı��� �������� ������ ���� ����(10�� ���� ����)
    //                  -> Inside Metarial���� ������ ���� ���͸���(������ ����� �������� ����)

    [SerializeField] float removeDelay = 1.5f; // delay�� �� ���� ����

    private static WHS_FractureManager instance; // �ı��� Fracture ������Ʈ���� �ν��Ͻ�
    private Dictionary<GameObject, Fracture> fractureObjects = new Dictionary<GameObject, Fracture>(); // �ı��� ������Ʈ�� Fracture ������Ʈ�� ����
    
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
    public void GetFractureObject(GameObject obj)
    {
        Fracture fracture = obj.GetComponent<Fracture>(); // ������Ʈ�� Fracture ������Ʈ ��������

        if (fracture != null && !fractureObjects.ContainsKey(obj)) // Fracture ������Ʈ�� �ְ�, ���� ��ϵ��� �ʾ�����
        {
            fractureObjects.Add(obj, fracture); // ��ųʸ��� �ı��� ������Ʈ �߰�
            
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
        WHS_ItemManager.Instance.SpawnItem(obj.transform.position);

        // removeDelay�� �� ���� ����
        yield return new WaitForSeconds(removeDelay);

        // ���� ����        
        GameObject fragmentRoot = GameObject.Find($"{obj.name}Fragments"); // ~Fragments �̸��� ������ ���� ������Ʈ ã��
        if (fragmentRoot != null)
        {
            Destroy(fragmentRoot); // ���� ������Ʈ ����
        }

        if (obj != null)
        {
            Destroy(obj); // ���� ������Ʈ ����
        }

        fractureObjects.Remove(obj); // ��ųʸ����� ����
    }
}
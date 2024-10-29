using SETUtil.SceneUI;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class PlayerMonsterDetection : MonoBehaviour
{
    public float radius = 0f;
    public LayerMask layer;
    public Collider[] colliders;


    public List<GameObject> gameObjects = new List<GameObject>();
    int i = 0;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("UIActivePoint"))
        {
            if (gameObjects[i] == null) return;
            gameObjects[i].GetComponent<UnitToScreenBoundary>().isActiveUI = true;
            i++;
        }
    }

    void Update()
    {
        // Comment : �÷��̾� ���� ���� Enemy���̾ ���� ������Ʈ�� ã�� �Լ� ����, ���Ͱ� �������� �ʴ´ٸ� ���� ���·� �ʱ�ȭ
        colliders = Physics.OverlapSphere(transform.position, radius, layer);
    }

}

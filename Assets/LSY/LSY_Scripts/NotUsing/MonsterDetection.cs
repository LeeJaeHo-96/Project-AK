using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterDetection : MonoBehaviour
{
    public float radius = 0f;
    public LayerMask layer;
    public Collider[] colliders;

    public Image leftImage; 
    public Image rightImage; 
    public Transform playerforwardtf, monstertf;
    Transform playertf;
    public TextMeshProUGUI text;

    private void Awake()
    {
        playertf = transform;
    }

    Vector3 v1, v2;
    float dot, mag;
    float engle;

    // Comment : �÷��̾�� ���� ������ ������ ���ؼ� ������ �׷��� �� �ؽ�Ʈ�� ���� ǥ��/ ������ ���� ����� ���� �ٸ��� ǥ��
    private void OnDrawGizmos()
    {
        // Comment : ���� ���� ���� ������ ǥ��
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);

        // Comment : �����Ǵ� ���Ͱ� ���ٸ� return ����
        if (monstertf == null) return;

        v1 = playerforwardtf.position - playertf.position;
        v2 = monstertf.position - playertf.position;
        dot = Vector3.Dot(v1, v2);
        mag -= Vector3.Magnitude(v1) * Vector3.Magnitude(v2);

        if (dot == mag)
        {
            Gizmos.color = Color.white;
        }
        else if (dot == 0)
        {
            Gizmos.color = Color.red;
        }
        else if (dot == -mag)
        {
            Gizmos.color = Color.gray;
        }
        else if (dot < 0)
        {
            Gizmos.color = Color.yellow;
        }
        else if (dot > 0 && dot < mag)
        {
            Gizmos.color = Color.blue;
        }

        Gizmos.DrawLine(playertf.position, playerforwardtf.position);
        Gizmos.DrawLine(playertf.position, monstertf.position);

        engle = Mathf.Acos(
            Vector3.Dot(v1, v2) / Vector3.Magnitude(v1) / Vector3.Magnitude(v2)) * Mathf.Rad2Deg;

        text.text = engle.ToString();
    }

    void Update()
    {
        // Comment : �÷��̾� ���� ���� Enemy���̾ ���� ������Ʈ�� ã�� �Լ� ����, ���Ͱ� �������� �ʴ´ٸ� ���� ���·� �ʱ�ȭ
        colliders = Physics.OverlapSphere(transform.position, radius, layer);
        if (colliders.Length > 0)
        {
            PlayerMonsterDetection();
        }

        if (colliders.Length == 0)
        {
            PlayerMonsterNonDetection();
        }
    }

    // Comment : ���� ������ ���Ϳ� �÷��̾��� ������ ���ϱ� ���� monstertf�� �ش� transform�� �־���
    private void PlayerMonsterDetection()
    {
        monstertf = colliders[0].transform;

        // TODO : ���� ������ ���� UI�̹����� ��ȭ ���� ����
        if (engle > 45)
        {
            if (monstertf.position.x > playertf.position.x)
            {
                Debug.Log("�����ʿ� ���� ����");
                rightImage.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("���ʿ� ���� ����");
                leftImage.gameObject.SetActive(true);
            }
            Debug.Log("���� ����");
        }
        else
        {
            Debug.Log("���� ����");
            rightImage.gameObject.SetActive(false);
            leftImage.gameObject.SetActive(false);
        }
    }

    // Comment : OverlapSphere���� enemy���̾ ���� ������Ʈ�� ������� �ʱ�ȭ
    private void PlayerMonsterNonDetection()
    {
        Debug.Log("�ֺ��� �����Ǵ� ���� ����");
        monstertf = null;
        rightImage.gameObject.SetActive(false);
        leftImage.gameObject.SetActive(false);
    }


}

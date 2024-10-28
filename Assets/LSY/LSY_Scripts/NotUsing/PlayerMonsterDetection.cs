using SETUtil.SceneUI;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class PlayerMonsterDetection : BaseUI
{
    public float radius = 0f;
    public LayerMask layer;
    public Collider[] colliders;


    public List<GameObject> gameObjects = new List<GameObject>();
    int i = 0;


    public Transform playerforwardtf, monstertf;
     Transform playertf;
    Vector3 v1, v2;
    float dot, mag;
    float engle;
    [SerializeField] TextMeshProUGUI engletext;

    private void Awake()
    {
        Bind();
        playertf = transform;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);

        // Comment : ���� ���� ���� ������ ǥ��
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);

        // Comment : �����Ǵ� ���Ͱ� ���ٸ� return ����
        if (monstertf == null) return;

        v1 = playerforwardtf.position - playertf.position;
        v2 = monstertf.position - playertf.position;
        dot = Vector3.Dot(v1, v2);
        mag -= Vector3.Magnitude(v1) * Vector3.Magnitude(v2);

        Gizmos.DrawLine(playertf.position, playerforwardtf.position);
        Gizmos.DrawLine(playertf.position, monstertf.position);

        engle = Mathf.Acos(
            Vector3.Dot(v1, v2) / Vector3.Magnitude(v1) / Vector3.Magnitude(v2)) * Mathf.Rad2Deg;

        engletext.text = engle.ToString();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
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
        if (colliders.Length > 0)
        {
            isPlayerMonsterDetection();
        }

        if (colliders.Length == 0)
        {
            PlayerMonsterNonDetection();
        }


        Debug.Log($"����: {colliders.Length}");
    }

    // Comment : ���� ������ ���Ϳ� �÷��̾��� ������ ���ϱ� ���� monstertf�� �ش� transform�� �־���
    private void isPlayerMonsterDetection()
    {
        // ���Ͱ� ���� ���� �� ��,,,?�� foreach�� ��� �ൿ�� �ݺ��ؾ� �ϰ� �Ǵµ�...
        monstertf = colliders[0].transform;

        // TODO : ���� ������ ���� UI�̹����� ��ȭ ���� ����
        if (engle > 45)
        {
            if (monstertf.position.x > playertf.position.x)
            {
                Debug.Log("�����ʿ� ���� ����");
                UpdateScoreUI("RightEnemyCountUI", 1);
                UpdateScoreUI("MonsterCount", colliders.Length);
            }

            if (monstertf.position.x < playertf.position.x)
            {
                Debug.Log("���ʿ� ���� ����");
                UpdateScoreUI("LeftEnemyCountUI", 1);
                UpdateScoreUI("MonsterCount", colliders.Length);
            }
            Debug.Log("���� ����");
        }
        else
        {
            if (monstertf.position.x > playertf.position.x)
            {
                UpdateScoreUI("RightEnemyCountUI", 0);
            }

            if (monstertf.position.x < playertf.position.x)
            {
                UpdateScoreUI("LeftEnemyCountUI", 0);
            }
        }

    }

    // Comment : OverlapSphere���� enemy���̾ ���� ������Ʈ�� ������� �ʱ�ȭ
    private void PlayerMonsterNonDetection()
    {
        Debug.Log("�ֺ��� �����Ǵ� ���� ����");
        monstertf = null;
    }

    private void UpdateScoreUI(string scoreKey, object score)
    {
        GetUI<TextMeshProUGUI>(scoreKey).text = score.ToString();
    }


}

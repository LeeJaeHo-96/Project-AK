using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WHS_Item : MonoBehaviour
{
    // ȹ���� ������ ������Ʈ�� �߰��ؼ� ���

    [SerializeField] float hoverRange = 0.3f; // �������� ���Ʒ��� �����̴� ����
    [SerializeField] float moveSpeed = 5; // �����̴� �ӵ�
    public float rotationSpeed = 120; // ȸ�� �ӵ�

    private Vector3 startPos; // ������ ���� ��ġ
    private Transform playerTransform; // �÷��̾� ��ġ

    [Header("���� �� n�� �� ȹ��")]
    [SerializeField] float moveDelay = 1f; // ���� �� �÷��̾�� delay�� �� �̵� (1�ʵ� �������� ���ƿ� ȹ��)
    [Header("������ ���ƿ��� �ӵ�")]
    [SerializeField] float moveToPlayerSpeed = 10f; // �������� �ٰ����� �ӵ�

    [SerializeField] float itemGetRange = 1f; // ������ ���� ����

    private bool isMovingtoPlayer = false; // �÷��̾�� �̵�������

    [Header("�� �Ѿ� ����")]
    [SerializeField] int[] bulletAmounts = new int[3]; // �� �ε����� ���� �Ѿ� ����
    private int bulletIndex; // Ư�� �Ѿ� �ε���

    private void Start()
    {
        startPos = transform.position; // ������ ��ġ�� ����
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // �÷��̾� ��ġ

        bulletIndex = Random.Range(0, 3); // 0~2 ���� �ε��� ������

        StartCoroutine(MoveToPlayer()); // �÷��̾�� 1�� �� �̵�

    }

    private void Update()
    {
        if (!isMovingtoPlayer) // ���������� ������ ������ ���� ������
        {
            // hoverRange�� ���Ʒ��� �����̴� ����, moveSpeed�� �̵� �ӵ� ����
            float newY = startPos.y + Mathf.Sin(Time.time * moveSpeed) * hoverRange; // ���Ʒ��� �̵��ϴ� ȿ��
            transform.position = new Vector3(transform.position.x, newY, transform.position.z); // y ��ġ�� �̵���Ŵ
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
        else
        {
            // �������� �÷��̾�� �̵�
            transform.position = Vector3.Lerp(transform.position, playerTransform.position, moveToPlayerSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, playerTransform.position) < itemGetRange)
            {
                GetItem(); // �������� ������ ������ ������ ȹ��
            }
        }
    }

    // ������ ���� �� 1�ʵ� ȹ��
    private IEnumerator MoveToPlayer()
    {
        yield return new WaitForSeconds(moveDelay); // delay�� �� �̵�
        isMovingtoPlayer = true;
    }

    // ������ ����
    private void GetItem()
    {
        // PlayerSpecialBullet�� �ν��Ͻ�
        if(PlayerSpecialBullet.Instance != null)
        {
            int amountBullet = bulletAmounts[bulletIndex];
            PlayerSpecialBullet.Instance.SpecialBullet[bulletIndex] += amountBullet; // index�� �Ѿ� Amout��ŭ ȹ��

            Debug.Log($"{bulletIndex+1}�� źȯ {amountBullet}�� ȹ��");
        }
        else
        {
            Debug.Log("PlayerSpecialBullet �ν��Ͻ� ã�� �� ����");
        }
        Destroy(gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterCount : MonoBehaviour
{
    [Header("���� ���� ī��Ʈ UI")]
    [SerializeField] TextMeshProUGUI textMeshPro;

    [Header("���� �ε������� UI �̹���")]
    [SerializeField] Image monsterDetectionImageUI;

    [Header("���� �ε������� ��� �̹���")]
    [SerializeField] Image monsterCountBackgroundImage;

    [Header("�Ϲ� ���� ������ �̹���")]
    [SerializeField] Image normalEnemyIcon;

    [Header("���� ���� ������ �̹���")]
    [SerializeField] Image strongEnemyIcon;

   // public HYJ_Enemy lsy_Enemy;
    private float test_monsterAttackPower = 5;

    int score = 0;
    private Coroutine StrongAttackRoutine;

    private void Start()
    {
        textMeshPro.text = score.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // if (other.GetComponent<HYJ_Enemy>.isdied = true)
            // test_monsterAttackPower �κ��� ������ ���� ���ݷ��� �޾ƿͼ� if �ۼ�
            if (test_monsterAttackPower >= 3) //other.GetComponent<HYJ_Enemy>.���ݷ� >= 3 
            {
                //���� ��
            }
            else
            {
                //�Ϲ� ��
            }
            HandleEnemyEntry(other);
        }

        if (other.gameObject.CompareTag("EliteEnemy"))
        {
            HandleStrongEnemyEntry(other);
        }

        // ���Ͱ� �׾��� ���� exit�� �Ͱ� ���� �ڵ� �����ϵ��� �̺�Ʈ �߻�
        var enemy = other.GetComponent<HYJ_Enemy>();
        if (enemy != null)
        {
            //enemy.OnEnemyDied.AddListener(HandleEnemyExitOnDeath);
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    score++;
    //    UpdateScoreText();
    //}

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            HandleEnemyExit();
        }

        if (other.gameObject.CompareTag("EliteEnemy"))
        {
            HandleStrongEnemyExit();
        }
    }

    private void HandleEnemyEntry(Collider other)
    {
        score++;
        Debug.Log("�Ϲ� ���� ����");
        UpdateScoreText();

        if (other.GetComponent<UnitToScreenBoundary>() != null)
            other.GetComponent<UnitToScreenBoundary>().isActiveUI = true;

        normalEnemyIcon.gameObject.SetActive(true);
        strongEnemyIcon.gameObject.SetActive(false);
    }

    private void HandleStrongEnemyEntry(Collider other)
    {
        Debug.Log("���� ���� ����");
        score++;
            UpdateScoreText();

            if (other.GetComponent<UnitToScreenBoundary>() != null)
                other.GetComponent<UnitToScreenBoundary>().isActiveUI = true;



            monsterDetectionImageUI.color = Color.white;
            monsterCountBackgroundImage.color = Color.yellow;

            StrongAttackRoutine = StartCoroutine(StrongMonsterAttack());
    }

    private void HandleEnemyExit()
    {
        Debug.Log("�Ϲ� ���� ����");
        score--;
        UpdateScoreText();
        normalEnemyIcon.gameObject.SetActive(true);
        strongEnemyIcon.gameObject.SetActive(false);
    }

    private void HandleStrongEnemyExit()
    {
        Debug.Log("���� ���� ����");
        score--;
        UpdateScoreText();

        monsterDetectionImageUI.color = Color.yellow;
        monsterCountBackgroundImage.color = Color.white;

        normalEnemyIcon.gameObject.SetActive(true);
        strongEnemyIcon.gameObject.SetActive(false);

        if (StrongAttackRoutine != null)
        {
            StopCoroutine(StrongAttackRoutine);
            StrongAttackRoutine = null;
        }
    }

    private void HandleEnemyExitOnDeath(Collider collider)
    {
        if (collider.gameObject.CompareTag("Enemy") || collider.gameObject.CompareTag("EliteEnemy"))
        {
            HandleEnemyExit(); 
        }
    }

    private void UpdateScoreText()
    {
        textMeshPro.text = score.ToString();
    }

    private IEnumerator StrongMonsterAttack()
    {
        WaitForSeconds delay = new WaitForSeconds(0.2f);
        float elapsedTime = 0f;

        while (elapsedTime < 3f)
        {
            normalEnemyIcon.gameObject.SetActive(false);
            strongEnemyIcon.gameObject.SetActive(true);
            yield return delay;

            normalEnemyIcon.gameObject.SetActive(true);
            strongEnemyIcon.gameObject.SetActive(false);
            yield return delay;

            elapsedTime += 0.4f;
        }

        // 3�� �� ���� ������ ������ ������ ��� ���� �ʱ�ȭ
        monsterDetectionImageUI.color = Color.white;
        monsterCountBackgroundImage.color = Color.white;
        normalEnemyIcon.gameObject.SetActive(true);
        strongEnemyIcon.gameObject.SetActive(false);
        yield break;
    }

}

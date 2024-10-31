using System.Collections;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public enum ColliderType { RightMonster, LeftMonster }
public class MonsterCount : MonoBehaviour
{

    [Header("���� �ε������� ������ �̹���")]
    [SerializeField] Image monsterDetectionImageUI;

    [Header("���� �ε������� ��� �̹���")]
    [SerializeField] Image monsterCountBackgroundImage;

    [Header("�Ϲ� ���� ������ �̹���")]
    [SerializeField] Image normalEnemyIcon;

    [Header("���� ���� ������ �̹���")]
    [SerializeField] Image strongEnemyIcon;

    private Coroutine StrongAttackRoutine;
    private Coroutine MonsterDiedScoreMinusRoutine;

    public bool isMiddle = false;
    public ColliderType colType;

    [Header("�浹ü�� �����Ǵ� ���� ���� ���� ��ũ��Ʈ")]
    public MonsterCountUI monsterCountUI;

    private void OnTriggerEnter(Collider other)
    {
        // Comment : �浹ü�� ���� ������ �� ���ݷ¿� ���� �����ؼ� �Լ� ��������
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("EliteEnemy"))
        {
            if (other.GetComponent<HYJ_Enemy>() == null) return;

            if (other.GetComponent<HYJ_Enemy>().monsterShieldAtkPower >= 3)
            {
                HandleStrongEnemyEntry(other);
            }
            else
            {
                HandleEnemyEntry(other);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<HYJ_Enemy>() == null) return;

        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("EliteEnemy"))
        {

            if (other.GetComponent<HYJ_Enemy>().monsterShieldAtkPower >= 3)
            {
                // ���� ���� ����
                HandleStrongEnemyExit(other);
            }
            else
            {
                HandleEnemyExit(other);
            }
        }
    }

    // ���� ���� �浹ü �ȿ� ���� �� �ڷ�ƾ �߻����Ѽ� ���ڰŸ��� ȿ��
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
        //monsterDetectionImageUI.color = Color.white;
        //monsterCountBackgroundImage.color = Color.white;
        //normalEnemyIcon.gameObject.SetActive(true);
        //strongEnemyIcon.gameObject.SetActive(false);
        yield break;
    }

    // Comment : ���� �浹ü �ȿ� ������ ��
    private void HandleEnemyEntry(Collider other)
    {
        // Comment : �ش� �浹ü�� �´� ī��Ʈui�� ���ڸ� +1 ����
        monsterCountUI.counters[(int)colType]++;
        HYJ_Enemy monster = other.GetComponent<HYJ_Enemy>();
        monster.hyj_monsterCount = monsterCountUI;

        if (!monsterCountUI.Enemies.ContainsKey(monster))
        {
            monsterCountUI.Enemies.Add(monster, colType);
            monsterCountUI.isEnter[monster] = true;
        }
        else
        {
            monsterCountUI.isEnter[monster] = true; 
        }

        // Comment : �浹ü�� ���� collider�� ��ũ��Ʈ���� �ش� ���� �ε������� ui �������� ����
        if (other.GetComponent<UnitToScreenBoundary>() != null)
            other.GetComponent<UnitToScreenBoundary>().isActiveUI = true;

        normalEnemyIcon.gameObject.SetActive(true);
        strongEnemyIcon.gameObject.SetActive(false);
    }

    // Comment : �Ϲ����� �浹ü���� ������ �� ���� ī��Ʈ�� -1 ����
    private void HandleEnemyExit(Collider other)
    {
        monsterCountUI.counters[(int)colType]--;
        HYJ_Enemy monster = other.GetComponent<HYJ_Enemy>();
        monster.hyj_monsterCount = monsterCountUI;

        if (monsterCountUI.Enemies.ContainsKey(monster))
        {
            monsterCountUI.Enemies.Remove(monster); 
            monsterCountUI.isEnter[monster] = false; 
        }

        Debug.Log("�Ϲ� ���� ȭ�� ������ ����");
        normalEnemyIcon.gameObject.SetActive(true);
        strongEnemyIcon.gameObject.SetActive(false);
    }

    // Comment : ���� ���Ͱ� �浹ü�� ������ �� ���� ī��Ʈ�� +1 ���ְ� �ڷ�ƾ ����
    private void HandleStrongEnemyEntry(Collider other)
    {

        Debug.Log("���� ���� ȭ�� ������ ����");
        monsterCountUI.counters[(int)colType]++;
        HYJ_Enemy monster = other.GetComponent<HYJ_Enemy>();
        monster.hyj_monsterCount = monsterCountUI;
        if (!monsterCountUI.Enemies.ContainsKey(monster))
        {
            monsterCountUI.Enemies.Add(monster, colType);
            monsterCountUI.isEnter[monster] = true;
        }

        if (other.GetComponent<UnitToScreenBoundary>() != null)
            other.GetComponent<UnitToScreenBoundary>().isActiveUI = true;
        if (isMiddle == false)
        {
            monsterDetectionImageUI.color = Color.white;
            monsterCountBackgroundImage.color = Color.yellow;
        }
        else
        {
            monsterCountBackgroundImage.color = Color.yellow;
        }


        StrongAttackRoutine = StartCoroutine(StrongMonsterAttack());

    }

    // Comment : ���� ���Ͱ� �浹ü�� ������ �� ���� ī��Ʈ�� -1 ���ְ� �������̴� �ڷ�ƾ�� ����
    private void HandleStrongEnemyExit(Collider other)
    {
        monsterCountUI.counters[(int)colType]--;
        HYJ_Enemy monster = other.GetComponent<HYJ_Enemy>();
        monster.hyj_monsterCount = monsterCountUI;

        if (monsterCountUI.Enemies.ContainsKey(monster))
        {
            monsterCountUI.Enemies.Remove(monster); 
            monsterCountUI.isEnter[monster] = false; 
        }

        if (isMiddle == false)
        {
            monsterDetectionImageUI.color = Color.yellow;
            monsterCountBackgroundImage.color = Color.white;
        }
        else
        {
            monsterCountBackgroundImage.color = Color.white;
        }

        normalEnemyIcon.gameObject.SetActive(true);
        strongEnemyIcon.gameObject.SetActive(false);

        if (StrongAttackRoutine != null)
        {
            StopCoroutine(StrongAttackRoutine);
            StrongAttackRoutine = null;
        }
    }

}

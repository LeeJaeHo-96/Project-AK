using UnityEngine;

public class HYJ_EnemyHitPoint : MonoBehaviour
{
    [SerializeField] HYJ_Enemy enemy;
    [SerializeField] bool weak;
 
    private void Awake()
    {
        enemy = GetComponentInParent<HYJ_Enemy>();
    }
    
    public bool TakeDamage(float damage)
    {
        if (enemy.HitFlag == false)
        {
           
            if (weak)
            {
                Debug.Log("����");
                enemy.MonsterTakeDamageCalculation(damage * 1.5f);
                
            }
            else
            {
                Debug.Log("�Ϲ�");
                enemy.MonsterTakeDamageCalculation(damage);
            }

            enemy.HitFlag = true;
            enemy.StartHitFlagCoroutine();

            return true;
        }
        else
        {
            return false;
        }

      
    }

    public bool GetHitFlag()
    {
        return enemy.HitFlag;
    }
    
}

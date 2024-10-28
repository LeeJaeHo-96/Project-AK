using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WHS_BreakableObject : MonoBehaviour
{
    // 파괴할 오브젝트에 Fracture 컴포넌트와 함께 추가해서 사용

    [SerializeField] GameObject itemPrefab; // 아이템 프리팹 등록

    private void Start()
    {
        // FractureManager의 딕셔너리에 등록해 오브젝트를 파괴하고 아이템 생성
        WHS_FractureManager.Instance.GetFractureObject(gameObject, itemPrefab);
    }

    // OnDestroy()로 아이템을 생성하니 Fracture 후 n초뒤 삭제되서 아이템 생성이 늦게 되는 문제
    // OnDisable()로 생성해도 동작에 문제가 생겨서 FractureManager에서 생성하게함

    /*
    private void OnDestroy()
    {
        Vector3 dropPos = obj.transform.position + new Vector3(0, 1.5f, 0);
        Instantiate(itemPrefab, dropPos, Quaternion.identity); // 오브젝트가 파괴된 자리에 1.5높이에 아이템 생성
    }
    */
}
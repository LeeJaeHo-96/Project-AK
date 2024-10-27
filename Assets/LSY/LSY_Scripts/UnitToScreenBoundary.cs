using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UnitToScreenBoundary : MonoBehaviour
{
    [SerializeField] RectTransform ui;
    private void Update()
    {
        Vector3 dir = (transform.position - Camera.main.transform.position).normalized;
        if (Vector3.Dot(Camera.main.transform.forward, dir) > 0)  // ���Ͱ� �տ� ������
        {
            Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);
            Debug.Log(pos);
            pos.x = Mathf.Clamp(pos.x,0, Screen.width);
            pos.y = Mathf.Clamp(pos.y, 0, Screen.height);
            ui.anchoredPosition = pos;
        }
        else    // ���Ͱ� �ڿ� ������
        {

        }
    }
}
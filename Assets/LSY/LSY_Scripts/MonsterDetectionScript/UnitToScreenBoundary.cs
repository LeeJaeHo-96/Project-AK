using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
public class UnitToScreenBoundary : MonoBehaviour
{
    [Header("���� �ε������� ��� �̹���")]
    [SerializeField] public Image image;
    [Header("���� �ε������� ������ �̹���")]
    [SerializeField] public Image UIImage;

    [Header("���� �ε������� ������ Ȱ��ȭ ����")]
    [SerializeField] public bool isActiveUI = false;


    private void Update()
    {
       if (isActiveUI)
        {
           UIMovement();
        }
    }

    public void UIMovement()
    {
        //Comment : WorldToScreenPoint�� ������ �����ӿ� ���� UI�� ��ġ�� ��ũ������Ʈ�� ��ȯ�Ͽ� ���, ��ȯ�� pos�� ui�̵�
        Vector3 dir = (transform.position - Camera.main.transform.position).normalized;
        if (Vector3.Dot(Camera.main.transform.forward, dir) > 0)
        {
            if (UIImage == null)return;
            UIImage.gameObject.SetActive(true);
            Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);
            pos.x = Mathf.Clamp(pos.x, 0, image.rectTransform.rect.width);
            pos.y = Mathf.Clamp(pos.y, 40, image.rectTransform.rect.height / 2);
            UIImage.rectTransform.anchoredPosition = pos;

            // Comment : ���� �ε������� ��� ��ġ�� �Ѿ�� ��Ȱ��ȭ ������
            if (pos.x == image.rectTransform.rect.width || pos.y == 0 || pos.y == image.rectTransform.rect.width || pos.x == 0)
            {
                SetActiveFalse();
            }
        }

        if (gameObject.GetComponent<HYJ_Enemy>().isDie == true)
        {
            isActiveUI = false;
            SetActiveFalse();
        }
    }

    public void SetActiveFalse()
    {
        UIImage.gameObject.SetActive(false);
    }

}
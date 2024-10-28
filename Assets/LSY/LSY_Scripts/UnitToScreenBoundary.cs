using UnityEngine;
using UnityEngine.UI;
public class UnitToScreenBoundary : MonoBehaviour
{
    [SerializeField] public Image UIImage;
    [SerializeField] public bool isActiveUI = false;
    [SerializeField] Image image;

    private void Update()
    {
       if (isActiveUI)
        {
           UIMovement();
        }
    }

    public void UIMovement()
    {
        //Comment : WorldToScreenPoint�� ������ �����ӿ� ���� UI�� ��ġ�� ��ũ������Ʈ�� ��ȯ�Ͽ� ���, ��ȯ�� pos�� ui��  ��
        Vector3 dir = (transform.position - Camera.main.transform.position).normalized;
        if (Vector3.Dot(Camera.main.transform.forward, dir) > 0)
        {
            UIImage.gameObject.SetActive(true);
            Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);
            pos.x = Mathf.Clamp(pos.x, 0, image.rectTransform.rect.width );
            pos.y = Mathf.Clamp(pos.y, 0, image.rectTransform.rect.height / 2);
            UIImage.rectTransform.anchoredPosition = pos;

            //���Ͱ� ������ ������ ������ ��
            if (pos.x == image.rectTransform.rect.width || pos.y == 0 || pos.y == image.rectTransform.rect.width || pos.x == 0)
            {
                SetActiveFalse();
            }
        }
    }

    public void SetActiveFalse()
    {
        UIImage.gameObject.SetActive(false);
    }

}
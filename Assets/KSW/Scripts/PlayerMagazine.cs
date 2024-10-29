using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMagazine : MonoBehaviour
{
    GameObject leftController;

    [Header("- �÷��̾� ������ ���� ��ũ��Ʈ")]
    [SerializeField] PlayerOwnedWeapons playerOwnedWeapons;

    // Comment : ���̵��� ���� ����
    Material material;
    Color color;
    Coroutine fadeIn;
    WaitForSeconds fadeInWaitForSeconds;
    float timeTick;

    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
        fadeInWaitForSeconds = new WaitForSeconds(0.1f);

        leftController = GameObject.Find("Left Controller");

        transform.parent = leftController.transform;
        transform.localPosition = Vector3.zero;

    }

    // 2�� = 0.1�ʴ� 0.05  1/20
    // 1�� = 0.1�ʴ� 0.1   1/10
    // 0.5�� = 0.1�ʴ� 0.2 1/5
    // 1/(speed * 10) 
    private void OnEnable()
    {
        float speed = playerOwnedWeapons.GetCurrentWeapon().GetReloadSpeed();
        
        timeTick = 1 / (speed * 10f);

        ResetAlpha();
        fadeIn = StartCoroutine(FadeInCorouine());
    }

    private void OnDisable()
    {
        ResetAlpha();
        StopAllCoroutines();
        
    }

    private void ResetAlpha()
    {
        color.a = 0f;
        material.color = color;
    }

    IEnumerator FadeInCorouine()
    {
        while (material.color.a < 1f ) {

            yield return fadeInWaitForSeconds;
            color = material.color;
            color.a += timeTick;
            material.color = color;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (material.color.a >= 1f)
        {
            gameObject.SetActive(false);
            playerOwnedWeapons.ReloadMagazine();
        }
    }
}

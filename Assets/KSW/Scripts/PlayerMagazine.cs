using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMagazine : MonoBehaviour
{
    // Comment : 사운드
    [Header("- 재장전 사운드")]
    [SerializeField] private AudioClip reloadSound;


    GameObject leftController;

    [Header("- 플레이어 소유중 무기 스크립트")]
    [SerializeField] PlayerOwnedWeapons playerOwnedWeapons;

    [Header("- 장전 되는 숫자 UI ")]
    [SerializeField] GameObject magazineAmountUI;
    [SerializeField] TextMeshProUGUI magazineAmountTextUI;


    private BoxCollider boxCollider;


    // Comment : 페이드인 관련 변수
    Material material;
    Color color;
    Coroutine fadeIn;
    WaitForSeconds fadeInWaitForSeconds;
    float timeTick;

   
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        material = GetComponent<MeshRenderer>().material;
        fadeInWaitForSeconds = new WaitForSeconds(0.1f);

        leftController = GameObject.Find("Left Controller");

        transform.parent = leftController.transform;
        transform.localPosition = Vector3.zero;

    }

    private void OnEnable()
    {
        boxCollider.enabled = false;
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
        magazineAmountUI.SetActive(false);
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
       

        SetTextMagazine();


        boxCollider.enabled = true;
        magazineAmountUI.SetActive(true);
    }

    public void SetTextMagazine()
    {
        int magazine = playerOwnedWeapons.GetCurrentWeapon().GetMaxMagazine() - playerOwnedWeapons.GetCurrentWeapon().GetMagazine();

        if (playerOwnedWeapons.Index != 0)
        {
            if (magazine - PlayerSpecialBullet.Instance.SpecialBullet[playerOwnedWeapons.Index - 1] > 0)
            {
                magazine = PlayerSpecialBullet.Instance.SpecialBullet[playerOwnedWeapons.Index - 1];
            }


        }
        magazineAmountTextUI.text = magazine.ToString();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (material.color.a >= 1f)
        {
            gameObject.SetActive(false);
            playerOwnedWeapons.ReloadMagazine();
            AudioManager.Instance.PlaySE(reloadSound);
        }
    }
}

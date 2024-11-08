using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Audio;

public class LJH_Shield : MonoBehaviour
{
    [Header("오브젝트")]
    [Header("역장 수리 오브젝트")]
    [SerializeField] GameObject shieldRecover;
    [Header("역장 무적 오브젝트")]
    [SerializeField] GameObject invincibility;

    [Header("스크립트")]
    [Header("UIManager 스크립트")]
    [SerializeField] LJH_UIManager uiManagerScript;

    [Header("플레이어 위치")]
    [SerializeField] GameObject playerPos;

    [Header("키입력")]
    [Header("역장 온오프 키입력")]
    [SerializeField] InputActionReference shieldOnOff;
    [Header("총 발사 키입력")]
    [SerializeField] InputActionReference fire;

    [Header("변수")]
    [Header("역장 활성화 여부")]
    public bool isShield;                         // Comment: 역장 활성화 여부   필요없으면 삭제 예정
    [Header("역장 파괴/비파괴 여부")]
    public bool isBreaked;                        // Comment: 역장 파괴 상태
    [Header("역장 수리중 여부")]
    public bool isRecover;                        // Comment: 회복 실행 여부
    [Header("역장 무적 상태 여부")]
    public bool isInvincibility;
    [Header("역장 내구도")]
    public float durability;                      // Comment: 역장 내구도
    [Header("몬스터의 공격 알람용 Bool 변수")]
    public bool isNow;




    private void Awake()
    {
        gameObject.SetActive(false);
        isRecover = false;
        isShield = false;
        isBreaked = false;
        isInvincibility = false;

        durability = 5;

    }

    // Comment: 역장이 활성화 될 때
    private void OnEnable()
    {

            isRecover = false;
            // Comment: 트리거 버튼에서 ShieldOn 제거
            shieldOnOff.action.performed -= ShieldOn;

            // Comment: 트리거 버튼에서 ShiledOff 추가
            shieldOnOff.action.performed += ShieldOff;

    }

    // Comment: 역장이 비활성화 될 때
    private void OnDisable()
    {
            // Comment: 트리거 버튼에서 ShieldOn 추가
            shieldOnOff.action.performed += ShieldOn;

            // Comment: 트리거 버튼에서 ShiledOff 제거
            shieldOnOff.action.performed -= ShieldOff;

    }

    private void Update()
    {
        if (WHS_StageIndex.curStage == 1)
        {
            transform.position = new Vector3(0, 1, 0);
        }
        else
        // Comment: 역장의 위치는 플레이어 위치로 따라다니게
        transform.position = playerPos.transform.position;

        if (durability < 0)
        {
            durability = 0;
        }
        // Comment: 내구도가 0이 될 때, 역장 파괴
        if (durability <= 0)
        {
            BreakedShield();
        }


    }


    // Comment: 역장 활성화
    public void ShieldOn(InputAction.CallbackContext obj)
    {
        // Comment: 일시정지때 사용 불가
        if (MenuEvent.Instance.IsPause)
            return;

        // Comment: 방패 파괴 상태가 아닐때만 해당 함수 불러올 수 있도록
        if (!isBreaked)
        {
            // Comment: 방패 > 활성화 , 방패 수리 > 비활성화, 방패 여부 > 활성화
            if (this == null) return;

                gameObject.SetActive(true);
                shieldRecover.SetActive(false);
                isShield = true;
            


            // Comment: 총기 인풋 끄기
           PlayerInputWeapon.Instance.enabled = false;
           PlayerInputWeapon.Instance.IsShield = isShield;
        }
    }

    // Comment: 역장 비활성화
    public void ShieldOff(InputAction.CallbackContext obj)
    {
        // Comment: 일시정지때 사용 불가
        if (MenuEvent.Instance.IsPause)
            return;

        // Comment: 방패 > 비활성화, 방패 수리 > 활성화, 방패 여부 > 비활성화 
        isRecover = true;
        shieldRecover.SetActive(true);
        gameObject.SetActive(false);
        isShield = false;

        // Comment:총기 인풋 켜기

        PlayerInputWeapon.Instance.enabled = true;
        PlayerInputWeapon.Instance.IsShield = isShield;
    }

    // Comment: 역장 파괴, 역장이 비활성화되며 isBreaked 변수에 값 전달
    public void BreakedShield()
    {
        isRecover = true;
        isBreaked = true;
        isShield = false;
        shieldRecover.SetActive(true);

        PlayerInputWeapon.Instance.enabled = true;
        PlayerInputWeapon.Instance.IsShield = isShield;


        gameObject.SetActive(false);
    }
    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputWeapon : MonoBehaviour
{
    // Comment : ��ǲ �ý��� ����
    // TODO : ���� ��ǲ �ý��� ���� ���� �ʿ�

    private PlayerOwnedWeapons playerOwnedWeapons;
    private PlayerChangeWeapon playerChangeWeapon;

    [Header("- ������ ���� źâ UI")]
    [SerializeField] private GameObject changeViewUI;


    [Header("- �߻�")]
    [SerializeField] private InputActionReference fire;

    [Header("- ��Ʈ�ѷ� �ϴ� ����")]
    [SerializeField] private InputActionReference downReload;

    [Header("- �׸� ����")]
    [SerializeField] private InputActionReference gripReload;


    [Header("- ���ⱳü UI ���")]
    [SerializeField] private InputActionReference viewChangeUI;

    [Header("- ���ⱳü UI ���� ���̽�ƽ")]
    [SerializeField] private InputActionReference rightJoystcikAxis;

    [Header("- ���ⱳü ��� Ȯ��")]
    [SerializeField] bool onToggle;

    private void Awake()
    {
        playerOwnedWeapons = GetComponent<PlayerOwnedWeapons>();
        playerChangeWeapon = GetComponent<PlayerChangeWeapon>();
    }
    private void OnEnable()
    {
      
        downReload.action.performed += OnDownReload;

        gripReload.action.performed += OnGripReload;
        gripReload.action.canceled += OffGripReload;

        fire.action.performed += OnFire;
        fire.action.canceled += OffFire;

 
        viewChangeUI.action.performed += OnChangeView;

        rightJoystcikAxis.action.performed += OnRightJoystick;
        rightJoystcikAxis.action.canceled += OnRightJoystick;
    }
    private void OnDisable()
    {
        downReload.action.performed -= OnDownReload;

        gripReload.action.performed -= OnGripReload;
        gripReload.action.canceled -= OffGripReload;

        fire.action.performed -= OnFire;
        fire.action.canceled -= OffFire;


        viewChangeUI.action.performed -= OnChangeView;

        rightJoystcikAxis.action.performed -= OnRightJoystick;
        rightJoystcikAxis.action.canceled -= OnRightJoystick;
    }


    void OnDownReload(InputAction.CallbackContext obj)
    {
        Quaternion quaternion = obj.ReadValue<Quaternion>();


        // Comment : ��Ʈ�ѷ��� x ��ǥ ������ 45~60 ���� �� �� ������ ȣ��

        if(quaternion.eulerAngles.x > 45f && quaternion.eulerAngles.x < 60f)
        {
            playerOwnedWeapons.ReloadMagazine();
        }
    }

    void OnGripReload(InputAction.CallbackContext obj)
    {
        playerOwnedWeapons.ReloadGripOnMagazine();
    }
    void OffGripReload(InputAction.CallbackContext obj)
    {
        playerOwnedWeapons.ReloadGripOffMagazine();
    }

    public void OnFire(InputAction.CallbackContext obj)
    {
        if (onToggle)
        {
            playerChangeWeapon.ChangeWeapon();
        }
        else
        {

            playerOwnedWeapons.GetCurrentWeapon().OnFireCoroutine();
        }

    }
    public void OffFire(InputAction.CallbackContext obj)
    {
        playerOwnedWeapons.GetCurrentWeapon().OffFireCoroutine();
    }

    void OnChangeView(InputAction.CallbackContext obj)
    {
        playerOwnedWeapons.ChangeUIUpdate();
        
        changeViewUI.SetActive(!changeViewUI.activeSelf);
        onToggle = changeViewUI.activeSelf;
        playerOwnedWeapons.GetCurrentWeapon().OffFireCoroutine();
    }

    void OnRightJoystick(InputAction.CallbackContext obj)
    {
        playerChangeWeapon.MoveJoystick(obj.ReadValue<Vector2>());
    }
    void OffRightJoystick(InputAction.CallbackContext obj)
    {
        playerChangeWeapon.MoveJoystick(Vector2.zero);
    }
}

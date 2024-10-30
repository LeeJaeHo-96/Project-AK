using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputWeapon : MonoBehaviour
{
    // Comment : ��ǲ �ý��� ����
    // TODO : ���� ��ǲ �ý��� ���� ���� �ʿ�

    // �������� �޴� �̺�Ʈ
    MenuEvent menuEvent;

    private PlayerOwnedWeapons playerOwnedWeapons;
    private PlayerChangeWeapon playerChangeWeapon;

    [Header("- UI ����")]
    [SerializeField] private PlayerWeaponUI weaponUI;

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
    [SerializeField] private bool onToggle;


    private void Awake()
    {
        menuEvent = GameObject.Find("MenuInputManager").GetComponent<MenuEvent>();
        menuEvent.SetPlayerWeaponInput(this);
        playerOwnedWeapons = GetComponent<PlayerOwnedWeapons>();
        playerChangeWeapon = GetComponent<PlayerChangeWeapon>();
    }
    private void OnEnable()
    {
        playerOwnedWeapons.GetCurrentWeapon().OffFireCoroutine();


        downReload.action.performed += OnDownReload;

        gripReload.action.performed += OnGripReload;
        gripReload.action.canceled += OffGripReload;

        fire.action.performed += OnFire;
        fire.action.canceled += OffFire;

 
        viewChangeUI.action.performed += OnChangeView;
        viewChangeUI.action.canceled += OffChangeView;

        rightJoystcikAxis.action.performed += OnRightJoystick;
        rightJoystcikAxis.action.canceled += OnRightJoystick;
    }
    private void OnDisable()
    {
       
        
        CloseChangeView();

        downReload.action.performed -= OnDownReload;

        gripReload.action.performed -= OnGripReload;
        gripReload.action.canceled -= OffGripReload;

        fire.action.performed -= OnFire;
        fire.action.canceled -= OffFire;


        viewChangeUI.action.performed -= OnChangeView;
        viewChangeUI.action.canceled -= OffChangeView;

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
     
        weaponUI.OnOffChangeUI(true);
      
        onToggle = true;
        playerOwnedWeapons.GetCurrentWeapon().OffFireCoroutine();
    }

    void OffChangeView(InputAction.CallbackContext obj)
    {
        CloseChangeView();
    }


    void CloseChangeView()
    {

        playerChangeWeapon.MoveJoystick(Vector2.zero);
        weaponUI.OnOffChangeUI(false);

        onToggle = false;

        playerOwnedWeapons.GetCurrentWeapon().UpdateMagazine();
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

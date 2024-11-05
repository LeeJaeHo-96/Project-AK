using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputWeapon : MonoBehaviour
{
    private static PlayerInputWeapon instance;


    public static PlayerInputWeapon Instance
    {
        get
        {
            return instance;

        }
    }

    // Comment : ��ǲ �ý��� ����
    // TODO : ���� ��ǲ �ý��� ���� ���� �ʿ�


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



    // �ǵ� ���� üũ
    public bool isShield;

    public bool IsShield { get { return isShield; } set { isShield = value; } }

    private void Awake()
    {
        if (instance == null)
        {

            instance = this;
        }
        else
        {
            Destroy(this);
        }

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
       
        
        CloseChangeView(true);
        playerOwnedWeapons.ReloadGripOffMagazine();
        playerOwnedWeapons.GetCurrentWeapon().OffFireCoroutine();
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
            if (playerOwnedWeapons.GetCurrentWeapon().gameObject.activeSelf)
            {
              
                playerOwnedWeapons.GetCurrentWeapon().OnFireCoroutine();
            }
        }

    }
    public void OffFire(InputAction.CallbackContext obj)
    {
        playerOwnedWeapons.GetCurrentWeapon().OffFireCoroutine();
    }

    void OnChangeView(InputAction.CallbackContext obj)
    {
        if (playerOwnedWeapons.OntGrip)
        {
            return;
        }
     
        weaponUI.OnOffChangeUI(true, false);
      
        onToggle = true;
        playerOwnedWeapons.GetCurrentWeapon().OffFireCoroutine();
    }

    void OffChangeView(InputAction.CallbackContext obj)
    {
        CloseChangeView(false);
    }


    void CloseChangeView(bool disable)
    {
        if (!disable)
        {
            playerChangeWeapon.ChangeWeapon();

        }

        playerChangeWeapon.MoveJoystick(Vector2.zero);
        weaponUI.OnOffChangeUI(false, disable);

        onToggle = false;

        
        playerOwnedWeapons.GetCurrentWeapon().UpdateMagazine();
    }



    void OnRightJoystick(InputAction.CallbackContext obj)
    {
        if (onToggle)
            playerChangeWeapon.MoveJoystick(obj.ReadValue<Vector2>());
    }
    void OffRightJoystick(InputAction.CallbackContext obj)
    {
       
            playerChangeWeapon.MoveJoystick(Vector2.zero);

    }


}

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
    [SerializeField] private GameObject magazineViewUI;


    [Header("- �߻�")]
    [SerializeField] private InputActionReference fire;

    [Header("- ��ư ����")]
    [SerializeField] private InputActionReference reload;

    [Header("- ��Ʈ�ѷ� �ϴ� ����")]
    [SerializeField] private InputActionReference downReload;

    [Header("- �׸� ����")]
    [SerializeField] private InputActionReference gripReload;

    [Header("- ���� ��ü")]
    [SerializeField] private InputActionReference changeLeft;
    [SerializeField] private InputActionReference changeRight;

    [Header("- źâ UI ���")]
    [SerializeField] private InputActionReference viewMagazine;

    private void Awake()
    {
        playerOwnedWeapons = GetComponent<PlayerOwnedWeapons>();
        playerChangeWeapon = GetComponent<PlayerChangeWeapon>();
    }
    private void OnEnable()
    {
        reload.action.performed += OnReload;
        downReload.action.performed += OnDownReload;

        gripReload.action.performed += OnGripReload;
        gripReload.action.canceled += OffGripReload;

        fire.action.performed += OnFire;
        fire.action.canceled += OffFire;

        changeLeft.action.performed += OnChangeLeft;
        changeRight.action.performed += OnChangeRight;

        viewMagazine.action.performed += OnMagazineView;
    }
    private void OnDisable()
    {
        reload.action.performed -= OnReload;
        downReload.action.performed -= OnDownReload;

        gripReload.action.performed -= OnGripReload;
        gripReload.action.canceled -= OffGripReload;

        fire.action.performed -= OnFire;
        fire.action.canceled -= OffFire;

        changeLeft.action.performed -= OnChangeLeft;
        changeRight.action.performed -= OnChangeRight;

        viewMagazine.action.performed -= OnMagazineView;
    }


    void OnReload(InputAction.CallbackContext obj)
    {
        playerOwnedWeapons.ReloadMagazine();

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
        playerOwnedWeapons.GetCurrentWeapon().OnFireCoroutine();

    }
    public void OffFire(InputAction.CallbackContext obj)
    {
        playerOwnedWeapons.GetCurrentWeapon().OffFireCoroutine();
    }
    void OnChangeLeft(InputAction.CallbackContext obj)
    {
       // playerChangeWeapon.ChangeWeapon(true);

    }
    void OnChangeRight(InputAction.CallbackContext obj)
    {
        playerChangeWeapon.ChangeWeapon(false);

    }

    void OnMagazineView(InputAction.CallbackContext obj)
    {
        playerOwnedWeapons.MagazineUIUpdate();
        magazineViewUI.SetActive(!magazineViewUI.activeSelf);
        

    }

}

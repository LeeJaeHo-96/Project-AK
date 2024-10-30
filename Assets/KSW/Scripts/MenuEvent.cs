using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuEvent : MonoBehaviour
{

    [Header("- �Ͻ�����")]
    [SerializeField] private InputActionReference pause;
   
    PlayerInputWeapon playerInputManager;

    private void OnEnable()
    {

        pause.action.performed += OnPause;

   
    }
    private void OnDisable()
    {
        pause.action.performed -= OnPause;

  
    }

    public void SetPlayerWeaponInput(PlayerInputWeapon input)
    {
        playerInputManager = input;
    }

    void OnPause(InputAction.CallbackContext obj)
    {
        Debug.Log("����");
        if (Time.timeScale < 1)
        {
            Time.timeScale = 1f;
            playerInputManager.enabled = true;
        }
        else
        {
            Time.timeScale = 0f;
            playerInputManager.enabled = false;
        }

       
    }
}

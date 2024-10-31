using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuEvent : MonoBehaviour
{

    [Header("- �Ͻ�����")]
    [SerializeField] private InputActionReference pause;

    [Header("- ���Ұ�")]
    [SerializeField] private InputActionReference mute;

    [Header("- �޴� UI")]
    [SerializeField] private GameMenuUI menu;

    PlayerInputWeapon playerInputManager;
    AudioManager audioManager;

    private bool isPause;
    private bool isMute;


    // �̱���
    private static MenuEvent instance;
    public static MenuEvent Instance
    {
        get
        {
            return instance;

        }
    }
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

        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void OnEnable()
    {

        pause.action.performed += OnPause;
        mute.action.performed += OnMute;


    }
    private void OnDisable()
    {
        pause.action.performed -= OnPause;
        mute.action.performed -= OnMute;

    }

    public void SetPlayerWeaponInput(PlayerInputWeapon input)
    {
        playerInputManager = input;
    }

    void OnPause(InputAction.CallbackContext obj)
    {
        if (isPause)
        {
         
            Time.timeScale = 1f;
            playerInputManager.enabled = true;
            isPause = false;
         
        }
        else
        {
           
            Time.timeScale = 0f;
            playerInputManager.enabled = false;
            isPause = true;
           
        }

        menu.TogglePauseUI(isPause);
    }

    void OnMute(InputAction.CallbackContext obj)
    {
        if (isMute)
        {
            isMute = false;
          
        }
        else
        {
            isMute = true;
            
        }

        audioManager.Mute(isMute);
        menu.ToggleMuteUI(isMute);
    }





}

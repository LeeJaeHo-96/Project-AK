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


    AudioManager audioManager;

    private bool isPause;
    private bool isMute;

 
    public bool IsPause { get { return isPause; } }

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

   

    void OnPause(InputAction.CallbackContext obj)
    {
        if (isPause)
        {
         
            Time.timeScale = 1f;
           
            isPause = false;


            if (PlayerInputWeapon.Instance.isShield == false)
                PlayerInputWeapon.Instance.enabled = true;
        }
        else
        {
           
            Time.timeScale = 0f;
            PlayerInputWeapon.Instance.enabled = false;
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

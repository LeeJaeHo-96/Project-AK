using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WHS_DollyProgress : MonoBehaviour
{
    // �� �����Ȳ
    // DollyCart�� ���൵�� ���� 0���� 1���� �����̴� �̵�

    private static WHS_DollyProgress instance;

    public static WHS_DollyProgress Instance
    {
        get
        {
            return instance;
        }
    }

    [SerializeField] CinemachineDollyCart dollyCart;
    [SerializeField] Image progressBar;

    private CinemachinePathBase path;

    public float progress;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        path = dollyCart.m_Path; 
    }


    private void Update()
    {
        float progress = Mathf.Clamp01(dollyCart.m_Position / path.PathLength); // īƮ�� ��ġ / Ʈ���� ����
        progressBar.fillAmount = progress;
    }

}

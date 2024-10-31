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
    [SerializeField] Slider progressBar;

    private CinemachinePathBase path;

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

        progressBar.minValue = 0;
        progressBar.maxValue = 1;
    }

    /*
    private void Update()
    {
        float progress = Mathf.Clamp01(dollyCart.m_Position / path.PathLength); // īƮ�� ��ġ / Ʈ���� ����
        progressBar.value = progress;
    }
    */

    public void UpdateProgress(float dollyMPos)
    {
        if(path != null)
        {
            dollyMPos = dollyCart.m_Position;
            float progress = Mathf.Clamp01(dollyMPos / path.PathLength);
            progressBar.value = progress;
        }
    }
}

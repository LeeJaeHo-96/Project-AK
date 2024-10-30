using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WHS_DollyProgress : MonoBehaviour
{
    // �� �����Ȳ
    // DollyCart�� ���൵�� ���� 0���� 1���� �����̴� �̵�
    // UI ��ġ ���� �ʿ�

    [SerializeField] CinemachineDollyCart dollyCart;
    [SerializeField] Slider progressBar;

    private CinemachinePathBase path;

    private void Start()
    {
        path = dollyCart.m_Path;

        progressBar.minValue = 0;
        progressBar.maxValue = 1;
    }

    private void Update()
    {
        float progress = Mathf.Clamp01(dollyCart.m_Position / path.PathLength); // īƮ�� ��ġ / Ʈ���� ����
        progressBar.value = progress;
    }
}

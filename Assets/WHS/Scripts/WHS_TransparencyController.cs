using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class WHS_TransparencyController : MonoBehaviour
{
    // ���� �����鼭 1�ʵ��� ���������� �������
    // ���Ͱ� ���� �� Destroy ��� StartFadeOut() ���
    // ������ �巯���� ���Ͱ� �����Ҷ� StartFadeIn() ���
    
    private static WHS_TransparencyController instance;

    public static WHS_TransparencyController Instance
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
    }

    // ���������鼭 �����
    public void StartFadeOut(GameObject obj, float duration)
    {
        StartCoroutine(FadeOut(obj, duration));
    }

    private IEnumerator FadeOut(GameObject obj, float duration)
    {
        yield return new WaitForSeconds(0.5f); // 0.5�� (�״� �ִϸ��̼� �ð� ����) �ں��� �������

        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>(); // �ڽ� ������Ʈ���� ���� 

        // RenderingMode�� Transparent�� ����
        foreach (Renderer render in renderers)
        {
            foreach (Material material in render.materials)
            {
                SetMaterialToTransparent(material);
            }
        }

        float elapsedtime = 0f;

        while(elapsedtime < duration) // 1�ʵ���
        {
            elapsedtime += Time.deltaTime;
            float alpha = 1f - (elapsedtime / duration); // ���İ� ���� ����

            foreach(Renderer render in renderers)
            {
                foreach(Material material in render.materials)
                {
                    Color color = material.color;
                    color.a = alpha;
                    material.color = color; // ����� ���İ��� ���͸��� ����
                }
            }

            yield return null;
        }

        Destroy(obj);
    }

    // ������ ������Ʈ�� ������ �巯��
    public void StartFadeIn(GameObject obj, float duration)
    {
        StartCoroutine(FadeIn(obj, duration));
    }

    private IEnumerator FadeIn(GameObject obj, float duration)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>(); // �ڽ� ������Ʈ���� ���� 

        // ���͸����� ���� ��带 Transparent�� ����
        foreach (Renderer render in renderers)
        {
            foreach(Material material in render.materials)
            {
                SetMaterialToTransparent(material);
            }
        }

        float elapsedTime = 0f;
        while(elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = elapsedTime / duration; // ���İ� ������Ŵ

            foreach(Renderer render in renderers)
            {
                foreach(Material material in render.materials)
                {
                    Color color = material.color;
                    color.a = alpha;
                    material.color = color; // ����� ���İ��� ���͸��� ����
                }
            }

            yield return null;
        }

        // ���͸����� ���� ��带 �ٽ� Opaque�� ����
        foreach (Renderer render in renderers)
        {
            foreach(Material material in render.materials)
            {
                SetMaterialToOpaque(material);
            }
        }
    }

    // ���͸����� Rendering Mode�� Transparent�� ����
    private void SetMaterialToTransparent(Material material)
    {
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = 3000;
    }

    // ���͸����� Rendering Mode�� Opaque�� ����
    private void SetMaterialToOpaque(Material material)
    {
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        material.SetInt("_ZWrite", 1);
        material.DisableKeyword("_ALPHATEST_ON");
        material.DisableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = -1;
    }
}

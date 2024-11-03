using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveDialogue2Manager : MonoBehaviour
{
    public GameObject background;
    //public GameObject bossBackGround;

    public TextMeshProUGUI fairyText;
    public TextMeshProUGUI playertText;
    public TextMeshProUGUI narrationText;
    public TextMeshProUGUI bossText;

    private string[][] waveDialogues;

    private int currentWave = 0;

    void Start()
    {
        background.gameObject.SetActive(false);
        //bossBackGround.gameObject.SetActive(false);

        waveDialogues = new string[][]
        {
            // Comment : �������� 2 ���� 
            new string[] { "���ΰ�: ���Ⱑ �߼��ΰ�? ���� �����Ⱑ �̻��ѵ�?",
                            "����: ���� ������ �����̾� ���� �� ���� �����", 
                            "������ �����Ҹ��� ����´�."}, 
            // Comment : ù��° ���̺� ���� �� ��� �̵� �� ��� ���
            new string[] { "����: ���ۺ��� �ʹ��ѵ�? �츮�� �������� �߸��� �͵� ���ݾ�!",
                            "���ΰ�: ���õ��� �츮�� ������ ���� �ʴ°���",
                            "����: �� �ô뵵 ���� ������ �ִ°ǰ�?"}, 
            // Comment : �ι�° ���̺� ���� �� ��� �̵��� �ϰ� ��� ���
            new string[] { "����: �̰� �츮 �߸� ����ģ�� ����..?",
                            "����: ��... �װ� �´°�...�� ���� ���� ����ġ��!", 
                            "��Ż��: ũ������!!! ��ƶ� ���!!!"}, 
            // Comment : ����° ���̺긦 ��� �� ��� �̵��� �ϰ� ��� ���
            new string[] { "����: �츮 �������� �����ľ� �Ǵ°ž�!?",
                            "���ΰ�: ��! ���� ���� ����! ������� �����ĺ���!",
                            "����: �׷� �׷�! ���⼭ ������ ��û�ϸ� �ǰڴ�!"},
            // Comment : �׹�° ���̺긦 ��� �� ��� �̵��� �ϰ� ��� ���
            new string[] { "����: ���� �ٿԾ�! ���ݸ� ���� ����!",
                            "���ΰ�: ����.. ���� �� �����ϰ���?",
                            "����: ��.. �װ� �ƴѰ� ����",
                            "�� �߾ӿ� ���ִ� �� ���� ����"},
            // Comment : �ټ���° ���̺긦 ��ȭ �� ��� �̵��� �ϰ� ��� ���
            new string[] {"����: ��� �� �� ������ �ͼ��� ���� ������",
                            "���ΰ�: Ȥ�� ��ħ���̾�!? �� ���� ��ħ���� ��ġ�� �;�",
                            "����: �´°� ����! �� �ö󰡺���!"},
            // Comment : ������ ���� ��
            new string[] {"����: ��Ͷ�! ���� �װ���� �ʴٸ� ��ħ���� ������!",
                            "���ΰ�: �ȵ�! �̰� ������ �߿��Ѱž�!"}
            // TODO: ���� �� ���� �� ���� ��Ʈ�� ������ �ʿ��� ���� ���ʿ��� ���� ���ؾ� ��
        };

        StartWave();
    }


    private IEnumerator DisplayDialogue(string[] dialogues)
    {
        foreach (var line in dialogues)
        {
            if (line.StartsWith("����:"))
            {
                fairyText.text = line;
                yield return new WaitForSeconds(3f);
                fairyText.text = "";
            }
            else if (line.StartsWith("���ΰ�:"))
            {
                background.gameObject.SetActive(true);
                playertText.text = line;
                yield return new WaitForSeconds(2f);
                background.gameObject.SetActive(false);
                playertText.text = "";
            }
            else if (line.StartsWith("����:"))
            {
                //bossBackGround.gameObject.SetActive(true);
                playertText.text = line;
                yield return new WaitForSeconds(2f);
                //bossBackGround.gameObject.SetActive(false);
                playertText.text = "";
            }
            else
            {
                background.gameObject.SetActive(true);
                narrationText.text = line;
                yield return new WaitForSeconds(2f);
                background.gameObject.SetActive(false);
                narrationText.text = "";
            }
        }
    }

    public void StartWave()
    {
        StartCoroutine(DisplayDialogue(waveDialogues[currentWave]));
        currentWave++;
    }
}

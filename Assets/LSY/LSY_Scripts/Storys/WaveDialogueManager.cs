using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class WaveDialogueManager : MonoBehaviour
{
    public TextMeshProUGUI fairyText;
    public TextMeshProUGUI playertText;
    public TextMeshProUGUI narrationText;
    public TextMeshProUGUI bossText;

    private string[][] waveDialogues; 

    private int currentWave = 0;

    void Start()
    {
        waveDialogues = new string[][]
        {
            // Comment : ù��° ���̺� ���� ��
            new string[] { "����: ���� ���� �������״ϱ�, ���� Ʈ���� ��ư�� ���� ��� ������ ����.", 
                            "����: ������ Ʈ���� ��ư�� ���� �Ѿ��� �߻���!" }, 
            // Comment : ù��° ���̺� ���� ��
            new string[] { "����: ���ɼ� �����! �� ������ ���� �� ���ݾ�!", 
                            "���ΰ�: ��, �� ����! �ݵ����ΰ�?!", 
                            "����: �����̾�! �� ��ħ�� ���� ������ �ִ°� ����?!", 
                            "������ ��ſ��Լ� ��ħ���� ���� �����.", "����: �̰� ����, �� �������ݾ�!", 
                            "����: �� ������ �� ��ħ���� ������ �ƾ�!" }, 
            // Comment : �ι�° ���̺� ���� ��
            new string[] { "����: ���� �׸� ��ư�� ���� ���⸦ ��ü�� �� �־�",
                            "����: �Ѿ��� �����ϸ� ���� �׸� ��ư�� ����, ���� �������� ������ٰ�!", }, 
            // Comment : �ι�° ���̺� ���� ��
            new string[] { "����: ����-! ��մ�� ����� �콺�ν����� ��������!", 
                            "����: ���丮���� �ߵ� ���� �ٴϽô���! �ᱹ �׷� ���������� �ٴٴ�, ���� �� �� ���� ���� �������?",
                            "����: ������ ������ ��ħ���� �Ҿ�������� �ʾҾ �̷� ������!",
                            "����: �̸� ���������� �Ҿ���� �ϵ� ������ �� �Ƴ�! �����̶� �ʰ� ���� ������ �� ��ȸ�� ����.",
                            "����: ���� ���񿡰� �ѱ� �� �˰�?!",
                            "����: �졦�� �׷� �� �˾Ҿ�.",
                            "����: ��������",
                            "����: ��ϸ����"},
            // Comment : ����° ���̺� ���� ��
            new string[] { "����: ������ �� �� �ѷ�����! ���� �߰����� ���̴�! ��ħ���� �ν��� ���� ���� �ʰ� ���� �� �̰ܳ� �� ������!",
                            "����: ���ƶ�! ���� ������ ����!",
                            "����: �� ���� ������ �� ���ϵ��� �� �ܰ� ��ȭ�Ѵ�!",
                            "����: �����̶� �� ���� ���� �� ������",
                            "����: ���� �ھƶ�!",
                            "����: �� ������ �޷���!",
                            "����: �� ���� �¾ƶ�!"},
            // Comment : ����° ���̺� ���� �� ��Ʈ ����
            // Comment : �׹�° ���̺� ���� ��
            new string[] {"����: ���� ��ٷ���?", "����: ���� ���� ���� ������ָ�!" },
            // Comment : ���� óġ ��
            new string[] { "����: �� ��ȥ�ǡ��︲���� ��������" }
        };
    }


    private IEnumerator DisplayDialogue(string[] dialogues)
    {
        foreach (var line in dialogues)
        {
            if (line.StartsWith("����:"))
            {
                fairyText.text = line;
                yield return new WaitForSeconds(2f); 
                fairyText.text = ""; 
            }
            else if (line.StartsWith("���ΰ�:"))
            {
                playertText.text = line;
                yield return new WaitForSeconds(2f);
                playertText.text = ""; 
            }
            else if (line.StartsWith("����:"))
            {
                playertText.text = line;
                yield return new WaitForSeconds(2f);
                playertText.text = "";
            }
            else
            {
                narrationText.text = line; 
                yield return new WaitForSeconds(2f);
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

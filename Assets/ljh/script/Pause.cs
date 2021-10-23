using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

    public GameObject StopWindow; // �Ͻ������� ������ UI

    public GameObject StopButton; // �Ͻ����� ��ư(������ ���)

    public void StopButtonOnClick() // �Ͻ����� ��ư�� ������ ��
    {
        StopWindow.SetActive(true); // �Ͻ����� �� ������ UI ���̰� �ϱ�

        StopButton.SetActive(false); // �Ͻ����� ��ư �����
    }

    public void StopButtonOffClick() // �Ͻ����� ��Ȳ���� �Ͻ����� ��ư�� �ٽ� �������� / "Back" UI ��������
    {
        StopWindow.SetActive(false); // �Ͻ����� �� ������ UI �Ⱥ��̰� �ϱ�

        StopButton.SetActive(true); // �Ͻ����� ��ư ���̱�
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{

    public GameObject StopWindow; // �Ͻ����� �� ������ UI

    public GameObject PlayButton; // ��� ��ư(������ ���)

    public GameObject OptionWindow; // ����â

    public Dropdown Resolution;

    public bool IsFullScreen;

    public void DropDownTest(int index)
    {

    }
    public void OptionClick() // "Option" ��ư Ŭ�� ��
    {
        OptionWindow.SetActive(true); // ����â ���̱�

        PlayButton.SetActive(false); // ��� ��ư �����

        StopWindow.SetActive(false); // �Ͻ����� �� ������ UI �����
    }

    public void OptionBackClick() // ����â ������ ��ܿ� "Back" Ŭ�� ��
    {
        OptionWindow.SetActive(false); // ����â �����

        PlayButton.SetActive(true); // �����ư ���̱�

        StopWindow.SetActive(true); // �Ͻ����� �� ������ UI ���̱�

    }

    public void fullscreen(bool ison) // õ��ȭ�� ��ư�� ������ ��
    {
        if (ison)
        {
            Screen.fullScreen = true; // ��üȭ��
            IsFullScreen = true;
        }
    }

    public void WindowScreen(bool ison) // â��� ��ư�� ������ ��
    {
        if (ison)
        {
            Screen.fullScreen = false; // â���
            IsFullScreen = false;
        }
    }

    public void UpdateResolution()
    {
        int index = Resolution.value + 1;
        switch (index)
        {
            case 1:
                Screen.SetResolution(1920, 1080, IsFullScreen);
                break;
            case 2:
                Screen.SetResolution(1768, 992, IsFullScreen);
                break;
            case 3:
                Screen.SetResolution(1680, 1050, IsFullScreen);
                break;
            case 4:
                Screen.SetResolution(1600, 900, IsFullScreen);
                break;
            case 5:
                Screen.SetResolution(1366, 768, IsFullScreen);
                break;
            case 6:
                Screen.SetResolution(1360, 768, IsFullScreen);
                break;
            case 7:
                Screen.SetResolution(1280, 1024, IsFullScreen);
                break;
            case 8:
                Screen.SetResolution(1280, 960, IsFullScreen);
                break;
            case 9:
                Screen.SetResolution(1280, 800, IsFullScreen);
                break;
            case 10:
                Screen.SetResolution(1280, 720, IsFullScreen);
                break;
            case 11:
                Screen.SetResolution(1176, 664, IsFullScreen);
                break;
            case 12:
                Screen.SetResolution(1152, 864, IsFullScreen);
                break;
            case 13:
                Screen.SetResolution(1024, 768, IsFullScreen);
                Debug.Log("13");
                break;
        }
    }
}

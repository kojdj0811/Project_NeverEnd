using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{

    public GameObject StopWindow; // 일시정지 시 나오는 UI

    public GameObject PlayButton; // 재생 버튼(오른쪽 상단)

    public GameObject OptionWindow; // 설정창

    public Dropdown Resolution;

    public bool IsFullScreen;

    public void DropDownTest(int index)
    {

    }
    public void OptionClick() // "Option" 버튼 클릭 시
    {
        OptionWindow.SetActive(true); // 설정창 보이기

        PlayButton.SetActive(false); // 재생 버튼 숨기기

        StopWindow.SetActive(false); // 일시정지 시 나오는 UI 숨기기
    }

    public void OptionBackClick() // 설정창 오른쪽 상단에 "Back" 클릭 시
    {
        OptionWindow.SetActive(false); // 설정창 숨기기

        PlayButton.SetActive(true); // 재생버튼 보이기

        StopWindow.SetActive(true); // 일시정지 시 나오는 UI 보이기

    }

    public void fullscreen(bool ison) // 천제화면 버튼을 눌렀을 때
    {
        if (ison)
        {
            Screen.fullScreen = true; // 전체화면
            IsFullScreen = true;
        }
    }

    public void WindowScreen(bool ison) // 창모드 버튼을 눌렀을 때
    {
        if (ison)
        {
            Screen.fullScreen = false; // 창모드
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

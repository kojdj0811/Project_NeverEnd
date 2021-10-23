using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{

    public GameObject StopWindow; // 일시정지시 나오는 UI

    public GameObject StopButton; // 일시정지 버튼(오른쪽 상단)

    public void StopButtonOnClick() // 일시정지 버튼을 눌렀을 때
    {
        StopWindow.SetActive(true); // 일시정지 시 나오는 UI 보이게 하기

        StopButton.SetActive(false); // 일시정지 버튼 숨기기

        Time.timeScale = 0;
    }

    public void StopButtonOffClick() // 일시정지 상황에서 일시정지 버튼을 다시 눌렀을때 / "Back" UI 눌렀을때
    {
        StopWindow.SetActive(false); // 일시정지 시 나오는 UI 안보이게 하기

        StopButton.SetActive(true); // 일시정지 버튼 보이기

        Time.timeScale = 1f;
    }

    public void TitleButtonClick()
    {
        Time.timeScale = 1f;
        
        SceneManager.LoadScene("Main");
        
        
    }
}

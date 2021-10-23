using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeTest : MonoBehaviour
{
    public string SceneName;

    public void ChangeScene () {
        SceneManager.LoadScene(SceneName);
    }

    public void LoadUiScene () {
        SceneManager.LoadScene("UI TEST", LoadSceneMode.Additive);
    }
}

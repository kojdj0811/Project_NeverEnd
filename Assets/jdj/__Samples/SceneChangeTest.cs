using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeTest : MonoBehaviour
{
    public string SceneName;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            SceneManager.LoadScene(SceneName);
    }
}

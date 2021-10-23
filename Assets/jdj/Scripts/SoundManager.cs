using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager S;


    public GameObject mouseHoverSound;
    public GameObject mouseClickSound;




    private void Awake() {
        S = this;
    }


    public void PlaySound_ButtonMouseHover () {
        var go = Instantiate(mouseHoverSound);
        DontDestroyOnLoad(go);
        Destroy(go, 1.0f);
    }

    public void PlaySound_ButtonMouseDown () {
        var go = Instantiate(mouseClickSound);
        DontDestroyOnLoad(go);
        Destroy(go, 1.0f);
    }

    public void PlaySound_ButtonMouseUp () {
        var go = Instantiate(mouseClickSound);
        DontDestroyOnLoad(go);
        Destroy(go, 1.0f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager S;


    public GameObject mouseHoverSound;
    public GameObject mouseClickSound;



    private void Awake() {
        if (S == null)
            S = this;
    }


    public void PlaySound_ButtonMouseHover () {
        Destroy(Instantiate(mouseHoverSound), 1.0f);
    }

    public void PlaySound_ButtonMouseDown () {
        Destroy(Instantiate(mouseClickSound), 1.0f);
    }

    public void PlaySound_ButtonMouseUp () {
        Destroy(Instantiate(mouseClickSound), 1.0f);
    }
}

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
        Instantiate(mouseHoverSound);
    }

    public void PlaySound_ButtonMouseDown () {
        Instantiate(mouseClickSound);
    }

    public void PlaySound_ButtonMouseUp () {
        Instantiate(mouseClickSound);
    }
}

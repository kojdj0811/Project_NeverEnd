using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeMonitor : MonoBehaviour
{
    public Text text;

    private void Update() {
        if(Character.S != null)
            text.text = Character.S.Life.ToString();
    }
}

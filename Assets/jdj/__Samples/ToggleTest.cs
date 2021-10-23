using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleTest : MonoBehaviour
{
    public void PrintToggleOn (bool isOn) {
        if(isOn)
            Debug.Log(transform.name);
    }
}

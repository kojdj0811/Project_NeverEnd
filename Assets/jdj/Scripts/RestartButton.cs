using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : MonoBehaviour
{
    public void Button () {
        if(Ending_Manager.instance != null)
            Ending_Manager.instance.Restart();
    }
}

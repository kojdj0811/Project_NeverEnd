using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public GameObject TitleUI;

    void UIPlay()
    {
        TitleUI.SetActive(true);
    }
    public float speed = 0.005f;
    Vector3 target = new Vector3(-35, 0, 0);
    void Start()
    {
        Invoke("UIPlay", 5);
    }

    void Update()
    {
        

        Vector3 velo = Vector3.zero;

        transform.position = Vector3.Lerp(transform.position, target, speed);

    }
    
}

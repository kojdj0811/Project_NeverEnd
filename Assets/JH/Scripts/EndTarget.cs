using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTarget : MonoBehaviour
{
    
    public static EndTarget instance = null;
    public bool isEnd = false;
    private void Awake()
    {
        instance = this;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isEnd)
        {
            if (collision.CompareTag("Bird"))
            {
                Debug.Log("end");
                Ending_Manager.instance.GameFinish();
                isEnd = true;
            }
        }
    }
}

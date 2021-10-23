using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public int lifeMax = 2;
    [SerializeField]
    private int life = 0;
    public int Life {
        get => life;
        set {
            life = value;
            gameObject.SetActive(life > 0);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("dddddd");
    }
}

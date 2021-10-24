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
            if(life > lifeMax)
                life = lifeMax;
            if(life < 0)
                life = 0;

            gameObject.SetActive(life > 0);

            if(life > 0)
                Character.S.CurrentState = CharacterState.Shield;
            else
                Character.S.CurrentState = CharacterState.Flying;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("dddddd");
    }
}

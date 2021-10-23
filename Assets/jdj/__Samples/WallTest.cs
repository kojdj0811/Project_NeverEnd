using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTest : MonoBehaviour
{
    public string targetTag;
    private void OnCollisionEnter2D(Collision2D other) {
        if ( other.transform.CompareTag(targetTag)) {
            Character.S.Life--; 
            Character.S.CallBloodParticle(other);
        }
    }
}

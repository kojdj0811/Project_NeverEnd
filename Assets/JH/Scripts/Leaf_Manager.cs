using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Expand_JH;
public class Leaf_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Go_Leaf();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Shield"))
        {
            Character.S.shield.Life--;
            transform.gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Bird"))
        {
            if (Character.S.CurrentState == CharacterState.Flying)
            {
                Character.S.Life--;
                Character.S.CallBloodParticle(collision);
            }
        }
    }

    // Update is called once per frame
    void Go_Leaf()
    {
        StartCoroutine(Move_Leaf());
    }
    IEnumerator Move_Leaf()
    {
        yield return StartCoroutine(transform.RectMove(3, -20 , Random.Range(3.0f,4.0f), true));
        Destroy(transform.gameObject);
        yield break;
    }
}

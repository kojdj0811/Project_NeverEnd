using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Expand_JH;

public class Eagle_Manager : MonoBehaviour
{
    public AnimationCurve curve_Move;
    public Transform tr_rot;

    public float time = 0.5f;
    public float pos_y = 1.5f;
    public float speed = 10f;
    //Transform tr_MoveY;
    private void Awake()
    {
        //tr_MoveY = transform.FindGameObjectByName("Move_Y").transform;.
        tr_rot = transform.FindGameObjectByName("body").transform;
    }
    private void Start()
    {
        Go_Eagle();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bird"))
        {
            Debug.Log("�� �浹");
        }
    }




    public void Go_Eagle()
    {
        StartCoroutine(Move());
    }  
    IEnumerator Move()
    {
        int rand = Random.Range(0, 2);
        float Value = Random.Range(1.0f, 1.8f);
        if(rand == 0)
        {
            curve_Move.AddKey(5, Value);
            curve_Move.AddKey(15, -Value);
        }
        else
        {
            curve_Move.AddKey(5, -Value);
            curve_Move.AddKey(15, Value);
        }


        yield return StartCoroutine(transform.MoveByAnimationCurve(tr_rot, curve_Move, Random.Range(15,20)));

        curve_Move.RemoveKey(1);
        curve_Move.RemoveKey(2);

        Destroy(this.gameObject);
        yield break;
    }
}

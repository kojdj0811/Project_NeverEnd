using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Expand_JH;
public class Branch_Manager : MonoBehaviour
{
    Transform tr_branch;
    

    private void Awake()
    {
        tr_branch = transform.FindGameObjectByName("branch").transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bird"))
        {
            fall();
        }
    }


    // Start is called before the first frame update
    void fall()
    {
        StartCoroutine(tr_branch.RectMove(2, -1, 0.5f, true));
        tr_branch.gameObject.GetComponent<Rigidbody2D>().simulated = true;
    }
}

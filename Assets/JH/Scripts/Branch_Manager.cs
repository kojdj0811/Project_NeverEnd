using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Expand_JH;
public class Branch_Manager : MonoBehaviour
{
    public static Branch_Manager instance;
    branch_Trigger[] trigger;
    private void Awake()
    {
        instance = this;
        trigger = new branch_Trigger[3];
        for(int i=0; i<3; i++)
        {
            trigger[i] = transform.FindGameObjectByName("branch_Trigger" + i).AddComponent<branch_Trigger>(); 
        }        
    }

    public void Reset_branch()
    {
        for(int i=0; i<3; i++)
        {
            trigger[i].Reset_pos();
        }
    }
    //Transform tr_branch;
    //Vector3 origin_pos;
    //private void Awake()
    //{

    //    tr_branch = transform.FindGameObjectByName("branch").transform;
    //    origin_pos = tr_branch.position;
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Bird"))
    //    {
    //        fall();
    //    }
    //}
    //public void Reset_branch()
    //{
    //    tr_branch.gameObject.GetComponent<Rigidbody2D>().simulated = false;
    //    tr_branch.position = origin_pos;
    //}


    //// Start is called before the first frame update
    //void fall()
    //{
    //    //StartCoroutine(tr_branch.RectMove(2, -1, 0.5f, true));
    //    tr_branch.gameObject.GetComponent<Rigidbody2D>().simulated = true;
    //}

    class branch_Trigger : MonoBehaviour
    {
        Transform tr_branch;
        Vector3 origin_pos;
        Vector2 origin_rot;
        bool TriggerOn = false;
        private void Awake()
        {

            tr_branch = transform.FindGameObjectByName("branch").transform;
            origin_pos = tr_branch.position;
            origin_rot = tr_branch.eulerAngles;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!TriggerOn)
            {
                if (collision.gameObject.CompareTag("Bird"))
                {
                    fall();
                    TriggerOn = true;
                }
            }
        }
        public void Reset_pos()
        {
            //tr_branch.gameObject.GetComponent<Rigidbody2D>().simulated = false;
            if (TriggerOn)
            {
                TriggerOn = false;
                Destroy(tr_branch.gameObject.GetComponent<Rigidbody2D>());
                tr_branch.position = origin_pos;
                tr_branch.eulerAngles = origin_rot;
            }
        }


        // Start is called before the first frame update
        void fall()
        {
            //StartCoroutine(tr_branch.RectMove(2, -1, 0.5f, true));
            tr_branch.gameObject.AddComponent<Rigidbody2D>();
            //tr_branch.gameObject.GetComponent<Rigidbody2D>().simulated = true;
        }
    }

}

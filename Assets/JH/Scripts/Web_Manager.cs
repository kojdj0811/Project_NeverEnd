using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Expand_JH;
public class Web_Manager : MonoBehaviour
{
    public static Web_Manager instance;
    Spider_web[] Webs;
    private void Awake()
    {
        instance = this;
        Webs = new Spider_web[4];

        for(int i=0; i<4; i++)
        {
            Webs[i] = transform.GetChild(i).gameObject.AddComponent<Spider_web>();
        }
    }
    
    public void Reset_spider()
    {
        for(int i=0; i<4; i++)
        {
            Webs[i].gameObject.SetActive(true);
        }
    }


    class Spider_web : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("°Å¹ÌÁÙ");
            gameObject.SetActive(false);
        }
    }
}

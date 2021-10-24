using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Expand_JH;
public class Web_Manager : MonoBehaviour
{
    public static Web_Manager instance;
    bool Slowing = false;
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

    Vector2 OriginVelocity;
    class Spider_web : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Bird"))
            {
                if (!instance.Slowing)
                {
                    instance.Slowing = true;
                    Character.S.flyPower *= 0.4f;
                    instance.OriginVelocity = Character.S.rigid2D.velocity;
                    Character.S.rigid2D.velocity *= 0.4f;
                    Character.S.spriteRenderer.color = Color.gray;
                }

            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Bird"))
            {
                if (instance.Slowing)
                {
                    instance.Slowing = false;
                    Character.S.flyPower = Character.S.initFlyPower;
                    Character.S.rigid2D.velocity = instance.OriginVelocity;
                    Character.S.spriteRenderer.color = Color.white;
                }

            }
        }
    }
}

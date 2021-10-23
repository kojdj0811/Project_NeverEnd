using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Expand_JH;
public class Projectile_Manager : MonoBehaviour
{
    public GameObject obj_Eagle;
    public GameObject obj_Leaf;

    Transform Target;
    float Progress; // ÁøÇàµµ
    private void Awake()
    {
        Target = transform.FindGameObjectByName("TargetPoint").transform;
    }
    private void Start()
    {
        StartCoroutine(Go_Eagle());
        StartCoroutine(Go_Leaf());
    }
    private void Update()
    {
        Progress = (Character.S.transform.position.x / Target.position.x) * 100;            
    }

    public IEnumerator Go_Eagle()
    {
        float waitTime = 0;
        while(true)
        {
            if (Progress < 20)
            {
                waitTime = Random.Range(16, 21);
            }
            else if (Progress < 40)
            {
                waitTime = Random.Range(13, 16);
            }
            else if (Progress < 60)
            {
                waitTime = Random.Range(10, 13);
            }
            else if (Progress < 80)
            {
                waitTime = Random.Range(8, 10);
            }
            else
            {
                waitTime = Random.Range(6, 8);
            }
            yield return new WaitForSeconds(waitTime);
            Instantiate(obj_Eagle, Character.S.transform.position + new Vector3(10, Random.Range(-3, 3)), Quaternion.identity, transform);

        }
    }

    public IEnumerator Go_Leaf()
    {
        float waitTime;
        while(true)
        {
            if (Progress < 20)
            {
                waitTime = Random.Range(12, 15);
            }
            else if (Progress < 40)
            {
                waitTime = Random.Range(10, 12);
            }
            else if (Progress < 60)
            {
                waitTime = Random.Range(8, 10);
            }
            else if (Progress < 80)
            {
                waitTime = 7;
            }
            else
            {
                waitTime = 6;
            }
            yield return new WaitForSeconds(waitTime);
            Instantiate(obj_Leaf, Character.S.transform.position + new Vector3(10, Random.Range(-3, 3)), Quaternion.identity, transform);
        }
    }
}

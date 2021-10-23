using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Manager : MonoBehaviour
{
    public GameObject obj_Eagle;
    public GameObject obj_Leaf;

    public void Go_Eagle()
    {
        Instantiate(obj_Eagle, Character.S.transform.position+new Vector3(10,Random.Range(-3,3)), Quaternion.identity, transform); 
    }

    public void Go_Leaf()
    {
        Instantiate(obj_Leaf, Character.S.transform.position + new Vector3(10, Random.Range(-3, 3)), Quaternion.identity, transform);
    }
}

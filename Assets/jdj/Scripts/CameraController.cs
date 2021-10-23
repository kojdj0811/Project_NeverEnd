using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 initPos;


    private void Awake() {
        initPos = transform.position;
    }

    void Update()
    {
        Vector3 pos = Character.S.transform.position;
        pos.z = initPos.z;
        transform.position = pos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float characterTrackingInterpolation;
    private Vector3 initPos;


    private void Awake() {
        initPos = transform.position;
    }

    void FixedUpdate()
    {
        Vector3 pos = Character.S.transform.position;
        pos.z = initPos.z;
        transform.position = Vector3.Lerp(transform.position, pos, characterTrackingInterpolation);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Test : MonoBehaviour
{
    public static Character_Test S;
    public Rigidbody2D rigid2D;
    public float flyPower = 10.0f;
    public float lineLengthMulty = 2.0f;


    [HideInInspector]
    public bool isMouseDown;
    [HideInInspector]
    public Vector2 mouseDragBeginPosition;
    [HideInInspector]
    public Vector2 mouseDragEndPosition;
    [HideInInspector]
    public Vector2 currentMousePosition;
    [HideInInspector]
    public Vector2 previousMousePosition;
    [HideInInspector]
    public Vector2 deltaMousePosition;

    public Transform aimTrans;

    new Camera camera;




    private void Awake() {
        S = this;
        camera = Camera.main;
    }


    void Update()
    {
        currentMousePosition = camera.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x, Input.mousePosition.y, camera.nearClipPlane));
        deltaMousePosition = currentMousePosition - previousMousePosition;

        if (Input.GetMouseButtonDown(0)) {
            OnMouseButtonDown();
        }


        if (isMouseDown)
            OnMouseButtonDrag();



        if (Input.GetMouseButtonUp(0))
            OnMouseButtonUp();

        previousMousePosition = currentMousePosition;
    }


    private void LateUpdate() {
        
    }



    void OnMouseButtonDown () {
            isMouseDown = true;
            mouseDragBeginPosition = currentMousePosition;

            aimTrans.position = currentMousePosition;

    }


    void OnMouseButtonDrag () {
        aimTrans.localEulerAngles = Vector3.forward * GetAngle(currentMousePosition - new Vector2 (aimTrans.position.x, aimTrans.position.y));
        aimTrans.localScale = Vector3.one + Vector3.up * (mouseDragBeginPosition - currentMousePosition).magnitude * lineLengthMulty;
    }


    void OnMouseButtonUp () {
        float angleOffest = mouseDragBeginPosition.x > currentMousePosition.x ? 180.0f : 0.0f;

        if (mouseDragBeginPosition.x > currentMousePosition.x)
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);


//==============
//Move
        rigid2D.angularVelocity = 0.0f;
        rigid2D.velocity = -deltaMousePosition.normalized * flyPower;

        transform.localEulerAngles = Vector3.forward * (aimTrans.localEulerAngles.z + 90.0f + angleOffest);
//==============

        mouseDragEndPosition = currentMousePosition;
        isMouseDown = false;
    }


    float GetAngle (Vector2 delta) {
        return Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg - 90.0f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static Character S;
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


    private Rigidbody2D[] childrenRigid2D;
    private FixedJoint2D[] joint2Ds;
    private Vector2[] initConnectedAnchors;



    private void Awake() {
        S = this;
        camera = Camera.main;
        childrenRigid2D = GetComponentsInChildren<Rigidbody2D>(true);
        joint2Ds = GetComponentsInChildren<FixedJoint2D>(true);
        initConnectedAnchors = new Vector2[joint2Ds.Length];
        for (int i = 0; i < initConnectedAnchors.Length; i++)
        {
            initConnectedAnchors[i] = joint2Ds[i].connectedAnchor;
        }

        aimTrans.gameObject.SetActive(false);
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



    void OnMouseButtonDown () {
            isMouseDown = true;
            mouseDragBeginPosition = currentMousePosition;

            aimTrans.position = currentMousePosition;

    }


    void OnMouseButtonDrag () {
        aimTrans.gameObject.SetActive(true);
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
        rigid2D.velocity = -(currentMousePosition - new Vector2 (aimTrans.position.x, aimTrans.position.y)) * flyPower;
        transform.localEulerAngles = Vector3.forward * (aimTrans.localEulerAngles.z + 90.0f + angleOffest);

        for (int i = 0; i < childrenRigid2D.Length; i++)
        {
            childrenRigid2D[i].angularVelocity = 0.0f;
        }

        rigid2D.angularVelocity = 0.0f;
//==============


        aimTrans.gameObject.SetActive(false);

        mouseDragEndPosition = currentMousePosition;
        isMouseDown = false;
    }


    float GetAngle (Vector2 delta) {
        return Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg - 90.0f;
    }
}

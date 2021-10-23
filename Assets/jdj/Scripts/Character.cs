using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public enum CharacterState
{
    Sleep,
    Flying,
    Die,
    PowerMode,
    Shield,
}





public class Character : MonoBehaviour
{
    public static Character S;
    [SerializeField]
    private CharacterState currentState;
    public CharacterState CurrentState {
        get => currentState;
        set  {
            currentState = value;
            switch (currentState)
            {
                case CharacterState.Sleep : {
                    ActiveRigidbodys(false);
                    break;
                }

                case CharacterState.Flying : {
                    ActiveRigidbodys(true);
                    break;
                }

                case CharacterState.Die : {
                    StartReturnToStartPointAnimation();
                    break;
                }

                case CharacterState.PowerMode : {
                    break;
                }

                case CharacterState.Shield : {
                    break;
                }


                default:
                    break;
            }
        }
    }

    public Rigidbody2D rigid2D;
    public float flyPower = 10.0f;
    public float lineLengthMulty = 2.0f;

    [SerializeField]
    private int life = 99;
    public int Life {
        get => life;
        set {
            if(life > value) {
                CurrentState = CharacterState.Die;
            } else {
                //nothing...
            }

            life = value;
        }
    }










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


    [HideInInspector]
    public Vector3 initPosition;
    [HideInInspector]
    public Vector3 initEularAngles;

    private Rigidbody2D[] childrenRigid2D;
    private FixedJoint2D[] joint2Ds;
    private Vector2[] initConnectedAnchors;



    private void Awake() {
        S = this;
        initPosition = transform.position;
        initEularAngles = transform.eulerAngles;

        childrenRigid2D = GetComponentsInChildren<Rigidbody2D>(true);
        joint2Ds = GetComponentsInChildren<FixedJoint2D>(true);
        initConnectedAnchors = new Vector2[joint2Ds.Length];

        for (int i = 0; i < initConnectedAnchors.Length; i++)
        {
            initConnectedAnchors[i] = joint2Ds[i].connectedAnchor;
        }

        aimTrans.gameObject.SetActive(false);
        aimTrans.SetParent(null);



        CurrentState = CharacterState.Sleep;
    }



    void Update()
    {
        currentMousePosition = CameraController.S.mainCamera.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x, Input.mousePosition.y, CameraController.S.mainCamera.nearClipPlane));
        deltaMousePosition = currentMousePosition - previousMousePosition;

        if (Input.GetMouseButtonDown(0)) {
            OnMouseButtonDown();
        }


        if (isMouseDown)
            OnMouseButtonDrag();



        if (Input.GetMouseButtonUp(0))
            OnMouseButtonUp();

        previousMousePosition = currentMousePosition;









        if(Input.GetKeyDown(KeyCode.Space)) {
            if(CameraController.S.CurrentCameraState == CameraState.ZoomIn) {
                CameraController.S.CurrentCameraState = CameraState.ZoomOut;
            } else {
                CameraController.S.CurrentCameraState = CameraState.ZoomIn;
            }
        }
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
        if(CurrentState == CharacterState.Sleep)
            CurrentState = CharacterState.Flying;


        float angleOffest = mouseDragBeginPosition.x > currentMousePosition.x ? 180.0f : 0.0f;

        if (mouseDragBeginPosition.x > currentMousePosition.x)
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);


//==============
//Move
        if (CurrentState == CharacterState.Flying) {
            rigid2D.velocity = -(currentMousePosition - new Vector2 (aimTrans.position.x, aimTrans.position.y)) * flyPower;
            transform.localEulerAngles = Vector3.forward * (aimTrans.localEulerAngles.z + 90.0f + angleOffest);

            for (int i = 0; i < childrenRigid2D.Length; i++)
            {
                childrenRigid2D[i].angularVelocity = 0.0f;
            }

            rigid2D.angularVelocity = 0.0f;
        }
//==============


        aimTrans.gameObject.SetActive(false);

        mouseDragEndPosition = currentMousePosition;
        isMouseDown = false;
    }


    float GetAngle (Vector2 delta) {
        return Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg - 90.0f;
    }


    public void StartReturnToStartPointAnimation () {
        ActiveRigidbodys(false);
        StartCoroutine (ReturnToStartPointAnimation());
    }

    IEnumerator ReturnToStartPointAnimation () {
        CameraController.S.CurrentCameraState = CameraState.ZoomIn;
        yield return new WaitForSeconds(2.0f);
        CameraController.S.CurrentCameraState = CameraState.ZoomOut;

        transform.position = initPosition;
        transform.eulerAngles = initEularAngles;

        CurrentState = CharacterState.Sleep;
    }


    public void ActiveRigidbodys (bool active) {
        for (int i = 0; i < childrenRigid2D.Length; i++)
        {
            childrenRigid2D[i].bodyType = active ? RigidbodyType2D.Dynamic : RigidbodyType2D.Kinematic;
            childrenRigid2D[i].velocity = Vector2.zero;
            childrenRigid2D[i].angularVelocity = 0.0f;
        }
    }



    private void OnCollisionEnter2D(Collision2D other) {

    }

}
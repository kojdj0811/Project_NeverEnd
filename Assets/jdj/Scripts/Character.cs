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
    Finish,
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
                    if(Coroutine_PowerModeOn != null)
                        StopCoroutine(Coroutine_PowerModeOn);
                    Coroutine_PowerModeOn = StartCoroutine (PowerModeOn());
                    break;
                }

                case CharacterState.Shield : {
                    break;
                }

                case CharacterState.Finish : {
                    ActiveRigidbodys(false);
                    break;
                }


                default:
                    break;
            }
        }
    }

    public Sprite sprite_up;
    public Sprite sprite_down;

    public GameObject birdDeadSound;
    public GameObject birdResetSound;


    public Rigidbody2D rigid2D;
    public float flyPower = 10.0f;
    [HideInInspector]
    public float initFlyPower;
    public float lineLengthMulty = 2.0f;

    [SerializeField]
    private int life = 99;
    public int Life {
        get => life;
        set {
            if(life > value) {
                if (CurrentState != CharacterState.PowerMode) {
                    CurrentState = CharacterState.Die;
                    life = value;
                }
            } else {
                //nothing...
            }

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
    public Shield shield;
    public SpriteRenderer spriteRenderer;
    public Sprite[] powerModeSprites;
    public float powerModeFlashDelay = 0.2f;
    public float powerModeDuration = 4.0f;
    public GameObject bloodParticle;
    private Transform particleGarbage;



    [HideInInspector]
    public Vector3 initPosition;

    private Vector3 previousPosition;

    private Sprite initSprite;

    [HideInInspector]
    public Vector3 initEularAngles;

    private Rigidbody2D[] childrenRigid2D;
    private FixedJoint2D[] joint2Ds;
    private Vector2[] initConnectedAnchors;


    private Coroutine Coroutine_PowerModeOn;


    private void Awake() {
        S = this;
        initPosition = transform.position;
        initEularAngles = transform.eulerAngles;
        initSprite = spriteRenderer.sprite;
        initFlyPower = flyPower;

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


        particleGarbage = new GameObject().transform;
        particleGarbage.name = "particleGarbage";
        particleGarbage.position = Vector3.zero;
        particleGarbage.rotation = Quaternion.identity;
        particleGarbage.localScale = Vector3.one;

        shield.Life = 0;
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




        if (CurrentState == CharacterState.Flying || CurrentState == CharacterState.Shield) {
            spriteRenderer.sprite = rigid2D.velocity.y > 0.0f ? sprite_up : sprite_down;
            Debug.Log(rigid2D.velocity.y > 0.0f);
        }





        // Keyboard Test
        if(Input.GetKeyDown(KeyCode.UpArrow)) {
            shield.Life++;
        }
        if(Input.GetKeyDown(KeyCode.DownArrow)) {
            shield.Life--;
        }

        if(Input.GetKeyDown(KeyCode.Space)) {
            CurrentState = CharacterState.PowerMode;
        }
        //Test End



        previousMousePosition = currentMousePosition;
        previousPosition = transform.position;
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



        if (CurrentState == CharacterState.Flying || CurrentState == CharacterState.PowerMode) {

//=================
//Direction
        float angleOffest = mouseDragBeginPosition.x > currentMousePosition.x ? 180.0f : 0.0f;

        if (mouseDragBeginPosition.x > currentMousePosition.x)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
//=================


//==============
//Move
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

        if(Coroutine_PowerModeOn != null)
            StopCoroutine(Coroutine_PowerModeOn);
        spriteRenderer.sprite = initSprite;

        StartCoroutine (ReturnToStartPointAnimation());
    }

    IEnumerator ReturnToStartPointAnimation () {
        CameraController.S.CurrentCameraState = CameraState.ZoomIn;
        yield return new WaitForSeconds(2.0f);
        CameraController.S.CurrentCameraState = CameraState.ZoomOut;

        transform.position = initPosition;
        transform.eulerAngles = initEularAngles;

        if(MapSet_Manager.Instance != null)
            MapSet_Manager.Instance.ShuffleMap();

        CurrentState = CharacterState.Sleep;
        shield.Life = 0;
        Destroy(Instantiate(birdResetSound), 1.0f);
    }


    IEnumerator PowerModeOn () {

        for (int i = 0; i < childrenRigid2D.Length; i++)
        {
            childrenRigid2D[i].bodyType = RigidbodyType2D.Dynamic;
        }



        WaitForSeconds delay = new WaitForSeconds(powerModeFlashDelay);
        float endTime = Time.timeSinceLevelLoad + powerModeDuration;

        while (endTime > Time.timeSinceLevelLoad) {
             spriteRenderer.sprite = powerModeSprites[Random.Range(0, 3)];
            yield return delay;
        }
        spriteRenderer.sprite = initSprite;

        currentState = CharacterState.Flying;
    }




    public void ActiveRigidbodys (bool active) {
        for (int i = 0; i < childrenRigid2D.Length; i++)
        {
            childrenRigid2D[i].bodyType = active ? RigidbodyType2D.Dynamic : RigidbodyType2D.Kinematic;
            childrenRigid2D[i].velocity = Vector2.zero;
            childrenRigid2D[i].angularVelocity = 0.0f;
        }
    }



    public void CallBloodParticle (Collision2D other) {
        if (CurrentState == CharacterState.PowerMode) return;

        Transform particleTrans = Instantiate(bloodParticle).transform;
        particleTrans.SetParent(particleGarbage);
        particleTrans.forward = Vector2.Reflect((transform.position - previousPosition).normalized, other.contacts[0].normal);
        particleTrans.position = other.contacts[0].point + new Vector2(particleTrans.forward.x, particleTrans.forward.y) * 0.2f;
        PlaySound_BirdDead();
    }

    public void PlaySound_BirdDead () {
        Destroy(Instantiate(birdDeadSound), 1.0f);
    }


    public void ActiveShield () {
        shield.Life = shield.lifeMax;
    }
}

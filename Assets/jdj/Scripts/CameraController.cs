using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



[System.Serializable]
public enum CameraState
{
    Normal,
    ZoomIn,
    ZoomOut,
}


public class CameraController : MonoBehaviour
{
    public static CameraController S;

    [SerializeField]
    private CameraState currentCameraState;
    public CameraState CurrentCameraState {
        get => currentCameraState;
        set {
            if(currentCameraState != value) {
                currentCameraState = value;

                switch (currentCameraState)
                {
                    case CameraState.Normal : {
                        break; 
                    }

                    case CameraState.ZoomIn : {
                        targetCameraOrthographicSize = zoomInOrthographicSize;
                        break; 
                    }

                    case CameraState.ZoomOut: {
                        targetCameraOrthographicSize = zoomOutOrthographicSize;
                        break; 
                    }

                    default:
                        break;
                }
            }
        }
    }


    [Range(0.0f, 1.0f)]
    public float characterTrackingInterpolation = 0.2f;

    [Range(0.0f, 1.0f)]
    public float zoomInOutInterpolation = 0.2f;

    private float targetCameraOrthographicSize;

    private Vector3 initPos;


    public float zoomInOrthographicSize;
    public float zoomOutOrthographicSize;


    [HideInInspector]
    public Camera mainCamera;


    private void Awake() {
        transform.SetParent(null);
        S = this;

        initPos = transform.position;

        mainCamera = Camera.main;
        targetCameraOrthographicSize = mainCamera.orthographicSize;
    }

    private void Start() {
        foreach(var cam in Camera.allCameras) {
            if(cam != mainCamera)
                Destroy(cam.gameObject);
        }

        var eventSystems = Object.FindObjectsOfType<EventSystem>();
        if(eventSystems != null && eventSystems.Length > 1) {
            for (int i = 0; i < eventSystems.Length; i++)
            {
                if (i != 0)
                    Destroy(eventSystems[i].gameObject);
            }
        }

    }

    void FixedUpdate()
    {
        Vector3 pos = Character.S.transform.position;
        pos.z = initPos.z;
        transform.position = Vector3.Lerp(transform.position, pos, characterTrackingInterpolation);

        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetCameraOrthographicSize, zoomInOutInterpolation);
    }
}

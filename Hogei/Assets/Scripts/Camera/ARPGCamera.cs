using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ARPGCamera : MonoBehaviour {

    [Header("Camera Settings")]
    public Transform TrackingTarget = null;
    public float HeightOffset = 0.0f; 
    private float ZoomAmount = 5.0f;
    [Header("Camera Zoom")]
    [Tooltip("The number of intervals between the minimum and maximum zoom")]
    public float ZoomIntervals = 1.0f;
    [Tooltip("The minimum zoom the camera can go.")]
    public float ZoomMin = 1.0f;
    [Tooltip("The maximum zoom the camera can go")]
    public float ZoomMax = 10.0f;
    [Header("Camera Angle")]
    [Tooltip("The minimum angle the camera can go.")]
    public float CameraMin = 25.0f;
    [Tooltip("The maximum angle the camera can go")]
    public float CameraMax = 75.0f;
    private float CameraAngle = 25.0f;
    private float ZoomAngleRatio = 25.0f;
    
	// Use this for initialization
	void Start () {

        //Set intial arm position
        Vector3 ArmPosition = new Vector3(0.0f, HeightOffset, 0.0f);
        transform.position = ArmPosition;
        //Set Inital Camera Zoom
        Vector3 CameraPosition = new Vector3(0.0f, 0.0f, -ZoomAmount);
        Camera.main.transform.localPosition = CameraPosition;
        //Set Inital Camera Angle
        ZoomAngleRatio = (CameraMax - CameraMin) / ZoomMax;
        CameraAngle = CameraMin + ZoomAmount * ZoomAngleRatio;
        Vector3 CameraRotation = new Vector3(CameraAngle, 0.0f, 0.0f);
        transform.rotation = Quaternion.Euler(CameraRotation);

        if (!TrackingTarget)
        {
            Debug.LogWarning("Tracking target has not been set for the camera");
            TrackingTarget = GameObject.FindGameObjectWithTag("Player").transform;
        }

    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetAxis("Mouse ScrollWheel") > 0.0f)
        {
            if(ZoomAmount > ZoomMin)
            {
                //Update Camera Zoom and Angle
                ZoomAmount -= ZoomIntervals;
                CameraAngle = CameraMin + ZoomAmount * ZoomAngleRatio;

                MoveCamera(ZoomAmount, CameraAngle);
            }
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0.0f)
        {
            if (ZoomAmount < ZoomMax)
            {
                //Update Camera Zoom and Angle
                ZoomAmount += ZoomIntervals;
                CameraAngle = CameraMin + ZoomAmount * ZoomAngleRatio;

                MoveCamera(ZoomAmount, CameraAngle);
            }
        }
        //Update position to tracked object
        if(TrackingTarget) transform.position = TrackingTarget.position + new Vector3(0.0f, HeightOffset, 0.0f);
	}

    public void MoveCamera(float _Zoom, float _Angle)
    {
        Vector3 CameraPosition = new Vector3(0.0f, 0.0f, -_Zoom);
        Camera.main.transform.DOLocalMove(CameraPosition, 1.0f);

        Vector3 CameraRotation = new Vector3(_Angle, 0.0f, 0.0f);
        transform.DORotate(CameraRotation, 1.0f);
    }

    public static void ShakeCamera(float _Duration)
    {
        Transform CamTransform = Camera.main.transform;
        if(!CamTransform)
        {
            Debug.LogError("No camera exists to shake");
            return;
        }

        CamTransform.DOShakePosition(_Duration);
    }
}

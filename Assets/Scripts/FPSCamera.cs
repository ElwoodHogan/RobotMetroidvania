using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    public float VeticalMouseSensitivity, HorizontalMouseSensitivity = 50.0f;

    //turns the objects Y rotation with camera
    [SerializeField] bool TurnObjectWithCamera;
    float _VerticalAngle, _HorizontalAngle;

    [SerializeField] Transform Camera;
    bool CursorLocked = true;

    private void Start()
    {
        _VerticalAngle = 0;
        _HorizontalAngle = transform.localEulerAngles.y;

        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CursorLocked = !CursorLocked;
            Cursor.lockState = CursorLocked ? CursorLockMode.Locked : CursorLockMode.Locked;
        }
        if (!CursorLocked) return;

        float turnPlayer = Input.GetAxis("Mouse X") * (HorizontalMouseSensitivity);
        _HorizontalAngle = _HorizontalAngle + turnPlayer;
        if (_HorizontalAngle > 360) _HorizontalAngle -= 360.0f;
        if (_HorizontalAngle < 0) _HorizontalAngle += 360.0f;

        Vector3 currentAngles = transform.localEulerAngles;
        currentAngles.y = _HorizontalAngle;
        if(TurnObjectWithCamera)  transform.localEulerAngles = currentAngles;
        else Camera.localEulerAngles = currentAngles;

        float turnCam = -Input.GetAxis("Mouse Y");
        turnCam = turnCam * (VeticalMouseSensitivity);
        _VerticalAngle = Mathf.Clamp(turnCam + _VerticalAngle, -89.0f, 89.0f);
        currentAngles = Camera.transform.localEulerAngles;
        currentAngles.x = _VerticalAngle;
        Camera.transform.localEulerAngles = currentAngles;
    }
}

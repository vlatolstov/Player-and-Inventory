using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    
    [SerializeField] private float _mouseSense;

    private float _horizontal;
    private float _vertical;
    private float _mouseX;
    private float _mouseY;

    private Plane _worldPlane = new(Vector3.up, Vector3.zero);
    private Vector3 _forwardDirection;
    private Vector3 _rightDirection;

    public float MouseSensitivity => _mouseSense;
    public float Horizontal => _horizontal;
    public float Vertical => _vertical;
    public float MouseX => _mouseX;
    public float MouseY => _mouseY;
    public Vector3 ForwardDirection => _forwardDirection;
    public Vector3 RightDirection => _rightDirection;





    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        _mouseX = Input.GetAxis("Mouse X");
        _mouseY = Input.GetAxis("Mouse Y");
    }

    //sets the camera relative directions for further using in movement
    public void SetDirections(Transform cameraTransform)
    {
        Vector3 cameraForward = Vector3.ProjectOnPlane(cameraTransform.forward, _worldPlane.normal).normalized;
        Vector3 cameraRight = Vector3.ProjectOnPlane(cameraTransform.right, _worldPlane.normal).normalized;

        _forwardDirection = cameraForward;
        _rightDirection = cameraRight;
    }
}


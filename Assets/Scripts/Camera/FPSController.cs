using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{

    [SerializeField] private Transform _playerTransform;
    [Tooltip("Bounds of camera rotation in Y axis.")]
    [SerializeField] private float yBoundDegrees;
    private InputManager _input;
    private float xRotation;
    private float yRotation;

    private void Start()
    {
        _input = FindObjectOfType<InputManager>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        _input.SetDirections(transform);
    }

    private void FixedUpdate()
    {
        
    }
    private void LateUpdate()
    {
        RotateCamera();
        KeepPosition();
    }

    private void RotateCamera()
    {
        xRotation -= _input.MouseY * _input.MouseSensitivity;
        xRotation = Mathf.Clamp(xRotation, -yBoundDegrees, yBoundDegrees);
        yRotation += _input.MouseX * _input.MouseSensitivity;

        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0);
        transform.rotation = rotation;
    }
    //align with player
    private void KeepPosition()
    {
        transform.position = _playerTransform.position;
            //new Vector3
            //(playerTransform.position.x, playerTransform.position.y + 1.73f, playerTransform.position.z);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{

    [SerializeField] private Transform _playerTransform;
    [Tooltip("Bounds of camera rotation in Y axis.")]
    [SerializeField] private float _yBoundDegrees;
    private InputManager _input;
    private float _xRotation;
    private float _yRotation;

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
        _xRotation += _input.MouseX * _input.MouseSensitivity * Time.deltaTime;
        _yRotation -= _input.MouseY * _input.MouseSensitivity * Time.deltaTime;
        _yRotation = Mathf.Clamp(_yRotation, -_yBoundDegrees, _yBoundDegrees);
        Vector3 rotationChange = new(_yRotation, _xRotation, 0);
        Quaternion rotation = Quaternion.Euler(rotationChange);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, .4f);
    }

    private void KeepPosition()
    {
        transform.position = Vector3.Lerp(transform.position, _playerTransform.position, .7f);
    }
}

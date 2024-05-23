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
        xRotation += _input.MouseX * _input.MouseSensitivity;
        yRotation -= _input.MouseY * _input.MouseSensitivity;
        yRotation = Mathf.Clamp(yRotation, -yBoundDegrees, yBoundDegrees);
        Vector3 rotationEuler = new(yRotation, xRotation, 0);

        transform.eulerAngles = rotationEuler;
    }

    private void KeepPosition()
    {
        transform.position = _playerTransform.position;
    }
}

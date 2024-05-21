using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private float _mouseSense;
    [SerializeField] private float _horizontalInput;
    [SerializeField] private float _verticalInput;
    [SerializeField] private float _mouseXInput;
    [SerializeField] private float _mouseYInput;

    public float MouseSensitivity => _mouseSense;
    public float HorizontalInput => _horizontalInput;
    public float VerticalInput => _verticalInput;
    public float MouseXInput => _mouseXInput;
    public float MouseYInput => _mouseYInput;

    void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
        _mouseXInput = Input.GetAxis("Mouse X") * _mouseSense;
        _mouseYInput = Input.GetAxis("Mouse Y") * _mouseSense;
    }
}


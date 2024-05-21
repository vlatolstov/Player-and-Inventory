using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _jumpForce;
    [Space]
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform feetTransform;
    [SerializeField] private Rigidbody playerRigidbody;

    private InputManager _input;

    private void Start()
    {
        _input = FindObjectOfType<InputManager>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Jump();
    }

    private void MovePlayer()
    {
        Vector3 movementVector = _input.RightDirection * _input.Horizontal 
            +_input.ForwardDirection * _input.Vertical;
        Debug.Log($"Movement vector: {movementVector}");
        playerRigidbody.velocity = movementVector * _movementSpeed;
    }

    private void Jump()
    {
        if (Physics.CheckSphere(feetTransform.position, 0.1f, groundMask))
        {
            playerRigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }
}

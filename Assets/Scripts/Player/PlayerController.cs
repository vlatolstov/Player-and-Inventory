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
    private ViewRaycast _cameraRaycast;

    private void Start()
    {
        _input = FindObjectOfType<InputManager>();
        _cameraRaycast = Camera.main.GetComponent<ViewRaycast>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Jump();
        if (Input.GetKeyDown(KeyCode.E)) Action();
    }

    private void MovePlayer()
    {
        Vector3 movementVector = Vector3.ClampMagnitude(_input.RightDirection * _input.Horizontal
            + _input.ForwardDirection * _input.Vertical, 1);
        playerRigidbody.velocity = movementVector * _movementSpeed;
    }

    private void Jump()
    {
        if (Physics.CheckSphere(feetTransform.position, 0.1f, groundMask))
        {
            playerRigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }

    ///remove later
    private void Action()
    {
        if (_cameraRaycast.Hit.collider.TryGetComponent<Item>(out Item item))
        {
            item.Deactivate();
        }
        
        //var target = _cameraRaycast.Hit.collider.gameObject;
        //target.SetActive(false);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Inventory))]
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
    private Inventory _inventory;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _input = FindObjectOfType<InputManager>();
        _cameraRaycast = Camera.main.GetComponent<ViewRaycast>();
        _inventory = GetComponent<Inventory>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Jump();
        if (Input.GetKeyDown(KeyCode.E)) Action();
        if (Input.GetKeyDown(KeyCode.I)) _inventory.InventoryUIManager.ShowInventory();
        if (Input.GetKeyDown(KeyCode.F1)) ItemDatabase.ShowDatabase();
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

    private void Action()
    {
        if (_cameraRaycast.Hit.collider.TryGetComponent<PlacedItem>(out PlacedItem item))
        {
            item.PickUp(_inventory);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    [SerializeField] private float movemetSpeed;
    [SerializeField] private float jumpForce;
    [Space]
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform feetTransform;
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private Camera FPS;
    [SerializeField] private Camera TPS;

    private Vector3 movementInput;
    private Camera currentCamera;

    private void Start()
    {
        currentCamera = FPS;
        currentCamera.gameObject.SetActive(true);
    }
    void Update()
    {
        movementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        MovePlayer();

        if (Input.GetKeyDown(KeyCode.Space)) Jump();

        if (Input.GetKeyDown(KeyCode.Tab)) ChangeCamera();
    }

    private void MovePlayer()
    {
        Vector3 movementVector = Vector3.ClampMagnitude(transform.TransformDirection(movementInput), 1f) * movemetSpeed;
        playerRigidbody.velocity = new Vector3(movementVector.x, playerRigidbody.velocity.y, movementVector.z);
    }

    private void Jump()
    {
        if (Physics.CheckSphere(feetTransform.position, 0.1f, groundMask))
        {
            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void ChangeCamera()
    {
        if (currentCamera == FPS)
        {
            FPS.gameObject.SetActive(false);
            TPS.gameObject.SetActive(true);
            currentCamera = TPS;
        }
        else
        {
            TPS.gameObject.SetActive(false);
            FPS.gameObject.SetActive(true);
            currentCamera = FPS;
        }
    }
}

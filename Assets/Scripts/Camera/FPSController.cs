using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float MouseSensitivity;
    [Tooltip("Bounds of camera rotation in Y axis.")]
    [SerializeField] private float yBoundDegrees;

    private Vector2 MouseInput;
    private float xRot;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        MouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        RotatePlayer();
    }

    private void LateUpdate()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        //mouse rotation
        xRot -= MouseInput.y * MouseSensitivity;
        xRot = Mathf.Clamp(xRot, -yBoundDegrees, yBoundDegrees);
        transform.localRotation = Quaternion.Euler(xRot, playerTransform.localRotation.eulerAngles.y, 0);

        //align with player
        transform.localPosition = new Vector3(playerTransform.position.x, playerTransform.position.y + 1.73f, playerTransform.position.z);
    }

    private void RotatePlayer()
    {
        playerTransform.transform.Rotate(0, MouseInput.x * MouseSensitivity, 0);
    }
}

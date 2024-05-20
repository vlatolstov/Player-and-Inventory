using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float MouseSensitivity;
    [SerializeField] private Vector3 offSet;

    private Vector2 MouseInput;
    private float xRot;
    private Plane worldPlane;

    private void Start()
    {
        worldPlane = new Plane(Vector3.up, Vector3.zero);
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
        transform.localRotation = Quaternion.Euler(xRot, playerTransform.localRotation.eulerAngles.y, 0);

        //align with player
        Quaternion rotation = Quaternion.Euler(MouseInput.x, 0, 0);
        Vector3 position = playerTransform.position + rotation * Vector3.forward * offSet.z;
        position.x = playerTransform.position.x + offSet.x;
        position.y = playerTransform.position.y + offSet.y;
        transform.position = Vector3.Lerp(transform.position, position, 0.5f) ;

        
    }

    private void RotatePlayer()
    {
        playerTransform.Rotate(0, MouseInput.x * MouseSensitivity, 0);
    }
}

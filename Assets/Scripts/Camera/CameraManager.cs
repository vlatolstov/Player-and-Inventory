using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    [SerializeField] private static Camera FPS;
    [SerializeField] private static Camera TPS;
    private static Camera currentCamera;

    void Start()
    {
        currentCamera = FPS;
        currentCamera.gameObject.SetActive(true);
    }


    void Update()
    {

    }

    public static void ChangeCamera()
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

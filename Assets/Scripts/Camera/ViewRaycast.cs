using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Diagnostics;

public class ViewRaycast : MonoBehaviour
{
    [SerializeField] private float _raycastDistance = 1.5f;
    public RaycastHit Hit { get; private set; }

    private Ray _ray;
    private Vector3 _centerPoint = new(.5f, .5f, 0);
    
    void Start()
    {
        
    }

    
    void Update()
    {
        _ray = GetComponent<Camera>().ViewportPointToRay(_centerPoint);

        if (Physics.Raycast(_ray, out RaycastHit hit, _raycastDistance))
        {
            Hit = hit;
            Debug.Log($"I hit {hit.collider.gameObject.name}");
        }
        // some 'else'?
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, Hit.point);
    }
}

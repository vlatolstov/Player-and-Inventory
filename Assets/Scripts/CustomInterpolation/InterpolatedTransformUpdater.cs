using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(100)]
public class InterpolatedTransformUpdater : MonoBehaviour
{
    private InterpolatedTransform _interpolatedTransform;

    private void Awake()
    {
        _interpolatedTransform = GetComponent<InterpolatedTransform>();
    }

    private void FixedUpdate()
    {
        _interpolatedTransform.LateFixedUpdate();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DefaultExecutionOrder(-100)]
public class InterpolationController : MonoBehaviour
{
    private float[] _lastFixedUpdateTimes;
    private int _newTimeIndex;

    private static float _interpolationFactor;
    public static float InterpolationFactor => _interpolationFactor;

    void Start()
    {
        _lastFixedUpdateTimes = new float[2];
        _newTimeIndex = 0;
    }

    private void FixedUpdate()
    {
        _newTimeIndex = OldTimeIndex();
        _lastFixedUpdateTimes[_newTimeIndex] = Time.fixedTime;
    }

    void Update()
    {
        float newTime = _lastFixedUpdateTimes[_newTimeIndex];
        float oldTime = _lastFixedUpdateTimes[OldTimeIndex()];

        if (newTime != oldTime) _interpolationFactor = (Time.time - newTime) / (newTime - oldTime);
        else _interpolationFactor = 1f;
    }

    private int OldTimeIndex() => _newTimeIndex == 0 ? 1 : 0;
}

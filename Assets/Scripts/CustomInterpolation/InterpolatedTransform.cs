using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-50)]
[RequireComponent(typeof(InterpolatedTransformUpdater))]
public class InterpolatedTransform : MonoBehaviour
{
    private TransformData[] _lastTransforms;
    private int _newTransformIndex;

    private void OnEnable()
    {
        ForgetPreviousTransforms();
    }

    public void ForgetPreviousTransforms()
    {
        _lastTransforms = new TransformData[2];
        TransformData t = new(
            transform.localPosition,
            transform.localRotation,
            transform.localScale);
        _lastTransforms[0] = t;
        _lastTransforms[1] = t;
        _newTransformIndex = 0;
    }

    private void FixedUpdate()
    {
        TransformData newestTransform = _lastTransforms[_newTransformIndex];
        transform.localPosition = newestTransform.position;
        transform.localRotation = newestTransform.rotation;
        transform.localScale = newestTransform.scale;
    }

    public void LateFixedUpdate()
    {
        _newTransformIndex = OldTransformIndex();
        _lastTransforms[_newTransformIndex] = new(
            transform.localPosition,
            transform.localRotation,
            transform.localScale);
    }

    private void Update()
    {
        TransformData newTransform = _lastTransforms[_newTransformIndex];
        TransformData oldTransform = _lastTransforms[OldTransformIndex()];

        transform.localPosition = Vector3.Lerp(
            oldTransform.position, newTransform.position, InterpolationController.InterpolationFactor);
        transform.localRotation = Quaternion.Slerp(
            oldTransform.rotation, newTransform.rotation, InterpolationController.InterpolationFactor);
        transform.localScale = Vector3.Lerp(
            oldTransform.scale, newTransform.scale, InterpolationController.InterpolationFactor);
    }

    private int OldTransformIndex() => _newTransformIndex == 0 ? 1 : 0;

    private struct TransformData
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;

        public TransformData(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
        }
    }
}

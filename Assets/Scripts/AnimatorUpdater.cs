using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorUpdater : MonoBehaviour
{
    private Animator _animator;
    private Transform _transform;
    private Rigidbody _rb;
    public float speed;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        _transform = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
         speed = _rb.velocity.magnitude;
        _animator.SetFloat("Speed", speed);
    }
}

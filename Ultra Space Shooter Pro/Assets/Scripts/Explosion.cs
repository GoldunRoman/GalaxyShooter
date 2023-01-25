using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

    }

    void Start()
    {
        _animator.SetTrigger("OnAsteroidDestroy");
    }

    
}

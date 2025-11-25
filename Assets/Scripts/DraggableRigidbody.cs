using System;
using UnityEngine;

public class DraggableRigidbody : MonoBehaviour,  IDraggable
{
    [SerializeField] private Rigidbody _rigidbody;

    private void Awake()
    {
        if (_rigidbody == null)
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
    }

    public void MoveTo(Vector3 targetPosition)
    {
        _rigidbody.MovePosition(targetPosition);
    }
}

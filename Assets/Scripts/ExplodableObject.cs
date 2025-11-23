using System;
using UnityEngine;

public class ExplodableObject : MonoBehaviour, IExplodable
{
    [SerializeField] private Rigidbody _rigidbody;

    private void Awake()
    {
        if (_rigidbody == null)
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
    }

    public void ApplyExplosion(Vector3 position, float force, float radius)
    {
        _rigidbody.AddExplosionForce(force, position, radius);
    }
}
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DraggableObject : MonoBehaviour, IDraggable
{
    [SerializeField] private bool _useRigidbody;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        if (_useRigidbody == true)
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
    }

    public void MoveTo(Vector3 targetPosition)
    {
        float currentY;

        if (_useRigidbody == true && _rigidbody != null)
        {
            currentY = _rigidbody.position.y;
        }
        else
        {
            currentY = transform.position.y;
        }

        Vector3 clampedPosition = new Vector3(
            targetPosition.x,
            currentY,          
            targetPosition.z
        );
        
        if (_useRigidbody == true && _rigidbody != null)
        {
            _rigidbody.MovePosition(clampedPosition);
            _rigidbody.velocity = Vector3.zero;    
        }
        else
        {
            transform.position = clampedPosition;
        }
    }
}
using UnityEngine;

public class DragController
{
    private readonly float _maxRayDistance;

    private IDraggable _selectedObject;
    private Transform _selectedTransform;

    private Vector3 _grabOffsetXZ;
    private float _selectedY;

    public DragController(float maxRayDistance)
    {
        _maxRayDistance = maxRayDistance;
    }

    public void StartDrag(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hitInfo, _maxRayDistance) == false)
        {
            return;
        }

        IDraggable draggable = hitInfo.collider.GetComponent<IDraggable>();

        if (draggable == null)
        {
            return;
        }

        _selectedObject = draggable;
        _selectedTransform = hitInfo.collider.transform;
        
        _selectedY = _selectedTransform.position.y;
        
        Vector3 objectPos = _selectedTransform.position;

        Vector3 hitPosOnSameY = new Vector3(
            hitInfo.point.x,
            objectPos.y,
            hitInfo.point.z);

        _grabOffsetXZ = objectPos - hitPosOnSameY;
    }

    public void ContinueDrag(Ray ray)
    {
        if (_selectedObject == null || _selectedTransform == null)
        {
            return;
        }

        if (Physics.Raycast(ray, out RaycastHit hitInfo, _maxRayDistance) == false)
        {
            return;
        }
        
        Vector3 hitPoint = hitInfo.point;
        
        Vector3 targetPosition = new Vector3(
            hitPoint.x,
            _selectedY,
            hitPoint.z);
        
        targetPosition += _grabOffsetXZ;

        _selectedObject.MoveTo(targetPosition);
    }

    public void StopDrag()
    {
        _selectedObject = null;
        _selectedTransform = null;
    }
}
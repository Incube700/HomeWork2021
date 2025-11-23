using UnityEngine;

public class DragController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _maxRayDistance = 100f;

    private IDraggable _selectedObject;

    private Vector3 _grabOffset;

    private void Update()
    {
        HandleMouseInput();
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryStartDrag();
        }

        if (Input.GetMouseButton(0))
        {
            ContinueDrag();
        }

        if (Input.GetMouseButtonUp(0))
        {
            StopDrag();
        }
    }

    private void TryStartDrag()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, _maxRayDistance))
        {
            IDraggable draggable = hitInfo.collider.gameObject.GetComponent<IDraggable>();

            if (draggable != null)
            {
                _selectedObject = draggable;
                Vector3 objectPosition = hitInfo.collider.transform.position;
                _grabOffset = objectPosition - hitInfo.point;
            }
        }
    }

    private void ContinueDrag()
    {
        if (_selectedObject == null)
        {
            return;
        }
        
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, _maxRayDistance))
        {
            Vector3 hitPoint = hitInfo.point;
            
            Vector3 targetPosition = hitInfo.point + _grabOffset;
            
            _selectedObject.MoveTo(targetPosition);
        }
    }

    private void StopDrag()
    {
        _selectedObject = null;
    }
}
    
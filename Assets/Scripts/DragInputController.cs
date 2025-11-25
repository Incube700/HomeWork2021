using UnityEngine;

public class DragInputController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _maxRayDistance = 100f;

    private DragController _dragController;

    private void Awake()
    {
        _dragController = new DragController(_maxRayDistance);
    }

    private void Update()
    {
        if (_camera == null)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            _dragController.StartDrag(ray);
        }

        if (Input.GetMouseButton(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            _dragController.ContinueDrag(ray);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _dragController.StopDrag();
        }
    }
}
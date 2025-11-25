using UnityEngine;

public class ShipController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _shipTransform;
    [SerializeField] private Transform _sailTransform;
    [SerializeField] private WindController _windController;

    [Header("Movement")]
    [SerializeField] private float _turnSpeedDegreesPerSecond = 60f;
    [SerializeField] private float _maxSpeed = 5f;

    [Header("Sail")]
    [SerializeField] private float _sailRotateSpeedDegreesPerSecond = 60f;
    [SerializeField] private float _maxSailAngleDegrees = 90f;

    private float _currentSailAngle;
    private float _currentSpeed;

    private void Update()
    {
        HandleShipRotation();
        HandleSailRotation();

        UpdateSpeedFromWind();

        MoveShip();
    }

    private void HandleShipRotation()
    {
        float turnInput = 0f;
        
        if (Input.GetKey(KeyCode.Q))
        {
            turnInput = -1f;
        }

        if (Input.GetKey(KeyCode.E))
        {
            turnInput = 1f;
        }

        float deltaAngle = turnInput * _turnSpeedDegreesPerSecond * Time.deltaTime;

        _shipTransform.Rotate(0f, deltaAngle, 0f);
    }

    private void HandleSailRotation()
    {
        float sailInput = 0f;
        
        if (Input.GetKey(KeyCode.A))
        {
            sailInput = -1f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            sailInput = 1f;
        }

        _currentSailAngle += sailInput * _sailRotateSpeedDegreesPerSecond * Time.deltaTime;

        _currentSailAngle = Mathf.Clamp(
            _currentSailAngle,
            -_maxSailAngleDegrees,
            _maxSailAngleDegrees);
        
        Quaternion shipRotation = _shipTransform.rotation;
        Quaternion sailRotation = shipRotation * Quaternion.Euler(0f, _currentSailAngle, 0f);

        _sailTransform.rotation = sailRotation;
    }

    private void UpdateSpeedFromWind()
    {
        if (_windController == null)
        {
            _currentSpeed = 0f;
            return;
        }
        
        Vector3 shipForward = _shipTransform.forward;
        shipForward.y = 0f;
        shipForward = shipForward.normalized;
        
        Vector3 sailForward = _sailTransform.forward;
        sailForward.y = 0f;
        sailForward = sailForward.normalized;
        
        Vector3 wind = _windController.WindDirection;
        
        float alignmentWindSail = Vector3.Dot(wind, sailForward);
        
        float windEffect = Mathf.Max(0f, alignmentWindSail);

        float alignmentSailShip = Mathf.Abs(Vector3.Dot(sailForward, shipForward));
        
        float power = windEffect * alignmentSailShip; 

        _currentSpeed = power * _maxSpeed;
    }

    private void MoveShip()
    {
        if (_currentSpeed <= 0.001f)
        {
            return;
        }

        Vector3 shipForward = _shipTransform.forward;
        shipForward.y = 0f;
        shipForward = shipForward.normalized;

        Vector3 displacement = shipForward * _currentSpeed * Time.deltaTime;

        _shipTransform.position += displacement;
    }
}

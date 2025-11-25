using UnityEngine;

public class WindController : MonoBehaviour
{
    [SerializeField] private Transform _windArrowTransform;
    [SerializeField] private float _changeSpeedDegreesPerSecond = 20f;
    [SerializeField] private bool _autoRotate = false;

    public Vector3 WindDirection { get; private set; }

    private float _currentAngleY;

    private void Start()
    {
        _currentAngleY = 45f;
        UpdateWindDirection();
    }

    private void Update()
    {
        if (_autoRotate == true)
        {
            _currentAngleY += _changeSpeedDegreesPerSecond * Time.deltaTime;
            if (_currentAngleY > 360f)
            {
                _currentAngleY -= 360f;
            }

            UpdateWindDirection();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _currentAngleY -= _changeSpeedDegreesPerSecond * Time.deltaTime;
            UpdateWindDirection();
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            _currentAngleY += _changeSpeedDegreesPerSecond * Time.deltaTime;
            UpdateWindDirection();
        }
    }

    private void UpdateWindDirection()
    {
        Quaternion rotation = Quaternion.Euler(0f, _currentAngleY, 0f);

        Vector3 direction = rotation * Vector3.forward;
        direction.y = 0f;
        direction = direction.normalized;

        WindDirection = direction;

        if (_windArrowTransform != null)
        {
            _windArrowTransform.rotation = rotation;
        }
    }
}
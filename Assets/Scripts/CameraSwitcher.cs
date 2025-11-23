using UnityEngine;
using Cinemachine;


public class CameraSwitcherCinemachine : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] _virtualCameras;

    [SerializeField] private int _startIndex = 0;

    private int _currentIndex;

    private void Start()
    {
        SetActiveCamera(_startIndex);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetActiveCamera(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetActiveCamera(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetActiveCamera(2);
        }
    }

    private void SetActiveCamera(int index)
    {
        if (_virtualCameras == null)
        {
            return;
        }

        if (index < 0 || index >= _virtualCameras.Length)
        {
            return;
        }

        _currentIndex = index;

        for (int i = 0; i < _virtualCameras.Length; i++)
        {
            CinemachineVirtualCamera virtualCamera = _virtualCameras[i];

            if (virtualCamera == null)
            {
                continue;
            }

            if (i == _currentIndex)
            {
                virtualCamera.Priority = 20;
            }
            else
            {
                virtualCamera.Priority = 0;
            }
        }
    }
}
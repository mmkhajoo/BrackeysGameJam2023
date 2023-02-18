using System;
using Cinemachine;
using UnityEngine;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        public static CameraManager Instance;

        [SerializeField] private int _priorityNumber = 50;

        [SerializeField] private int _baseNumber = 10;
        
        [SerializeField] private CinemachineVirtualCamera _mainCamera;

        [SerializeField] private CinemachineVirtualCamera _sleepCamera;
        
        [SerializeField] private CinemachineVirtualCamera _wolfCamera;

        [SerializeField] private CinemachineVirtualCamera _IceShaterOnWolf;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                
                DontDestroyOnLoad(gameObject);
            }
        }

        public void ChangeCamera(CameraType cameraType)
        {
            ResetCameraNumbers();
            
            switch (cameraType)
            {
                case CameraType.Main:
                    _mainCamera.Priority = _priorityNumber;
                    break;
                case CameraType.Sleep:
                    _sleepCamera.Priority = _priorityNumber;
                    break;
                case CameraType.Wolf:
                    _wolfCamera.Priority = _priorityNumber;
                    break;
                case CameraType.Ice:
                    _IceShaterOnWolf.Priority = _priorityNumber;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cameraType), cameraType, null);
            }
        }

        private void ResetCameraNumbers()
        {
            _mainCamera.Priority = _baseNumber;
            _sleepCamera.Priority = _baseNumber;
            _wolfCamera.Priority = _baseNumber;
            _IceShaterOnWolf.Priority = _baseNumber;
        }
        
       
    }
    
    public enum CameraType
    {
        None = 0,
        Main =1,
        Sleep = 2,
        Wolf = 3,
        Ice =  4,
    }
}
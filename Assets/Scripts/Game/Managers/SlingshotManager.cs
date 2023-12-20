using System.Collections.Generic;
using Game.Interactable;
using UnityEngine;
using Zenject;

namespace Game.Managers
{
    public class SlingshotManager : MonoBehaviour
    {
        [Inject] private InputManager _inputManager;
        [Inject] private TrajectoryManager _trajectoryManager;
        [Inject] private LevelManager _levelManager;
    
        [SerializeField] private List<ThrowMan> throwMans;
        [SerializeField] private Transform originalProjectilePosition;
        [SerializeField] private Transform sling;
        [SerializeField] private float maxPullStrength;
        [SerializeField] private float launchStrength;
        [SerializeField] private float maxPullDistance;
        [SerializeField] private float sensitivity;

        private Vector3 _originalSlingPosition;  
        private bool _isPulling;
        private float _pullStrength;
        private int _throwManCount = 0;
        private ThrowMan _currentThrowMan;

        void Start()
        {
            Subscribe();
            _currentThrowMan = throwMans[_throwManCount];
            _originalSlingPosition = sling.position;
        }

        void Subscribe()
        {
            _inputManager.Clicked += OnClicked;
            _inputManager.ClickHold += OnClickHold;
            _inputManager.ClickedUp += OnClickedUp;
        }    
    
        void Unsubscribe()
        {
            _inputManager.Clicked -= OnClicked;
            _inputManager.ClickHold -= OnClickHold;
            _inputManager.ClickedUp -= OnClickedUp;
        }

        private void OnClicked()
        {
            StartPull();
        }

        private void OnClickHold()
        {
            ContinuePull();
        }

        private void OnClickedUp()
        {
            Release();
        }

        void StartPull()
        {
            _isPulling = true;
        }

        void ContinuePull()
        {
            _pullStrength = Mathf.Clamp((Screen.height - Input.mousePosition.y) / Screen.height * maxPullStrength, 0f, maxPullStrength);
            UpdateThrowObjectPosition();
            _trajectoryManager.UpdateTrajectory(_pullStrength,launchStrength);
        }

        void Release()
        {
            Launch();
            ResetPositions();
            _trajectoryManager.HideTrajectory();
        }

        void ResetPositions()
        {
            sling.position = _originalSlingPosition;
        }

        void UpdateThrowObjectPosition()
        {
            Vector3 launchDirection = transform.forward;
            Vector3 projectileCurrentPosition = originalProjectilePosition.position;
            Vector3 slingCurrentPosition = _originalSlingPosition;

            float pullDistance = Mathf.Min(maxPullDistance, _pullStrength * sensitivity);

            Vector3 updatedProjectilePosition = projectileCurrentPosition - launchDirection * pullDistance;
            Vector3 updatedSlingPosition = slingCurrentPosition - launchDirection * pullDistance;
            _currentThrowMan.transform.position = updatedProjectilePosition;
            sling.position = updatedSlingPosition;
        }

        void Launch()
        {
            _currentThrowMan.Throw(_pullStrength,launchStrength);
            _pullStrength = 0f;
            _inputManager.SetSituation(false);
        }

        public void NextThrowMan()
        {
            if (throwMans.Count <= _throwManCount + 1)
            {
                _levelManager.StartCountDown();
                return;
            }
            _throwManCount++;
            _currentThrowMan = throwMans[_throwManCount];
            _currentThrowMan.transform.position = originalProjectilePosition.position;
            _inputManager.SetSituation(true);
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }
    }
}

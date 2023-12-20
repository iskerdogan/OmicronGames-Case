using UnityEngine;

namespace Game.Managers
{
    public class TrajectoryManager : MonoBehaviour
    {
        [SerializeField] private int trajectoryPointCount;
        [SerializeField] private GameObject trajectoryPointPrefab;
        [SerializeField] private Transform originalTrajectoryPosition;

        private GameObject[] _trajectoryPoints;

        void Start()
        {
            InitializeTrajectoryPoints();
        }

        void InitializeTrajectoryPoints()
        {
            _trajectoryPoints = new GameObject[trajectoryPointCount];

            for (int i = 0; i < trajectoryPointCount; i++)
            {
                _trajectoryPoints[i] = Instantiate(trajectoryPointPrefab, Vector3.zero, Quaternion.identity);
                _trajectoryPoints[i].SetActive(false);
            }
        }

        public void UpdateTrajectory(float pullStrength, float launchStrength)
        {
            Vector3 launchDirection = transform.forward;
            float timeStep = 0.02f;

            for (int i = 0; i < trajectoryPointCount; i++)
            {
                float t = i * timeStep;
                Vector3 position = CalculateTrajectoryPoint(originalTrajectoryPosition.position, launchDirection * (pullStrength + launchStrength), t);
                _trajectoryPoints[i].transform.position = position;
                _trajectoryPoints[i].SetActive(true);
            }
        }

        Vector3 CalculateTrajectoryPoint(Vector3 initialPosition, Vector3 initialVelocity, float time)
        {
            Vector3 gravity = Physics.gravity;
            return initialPosition + initialVelocity * time + 0.5f * gravity * time * time;
        }

        public void HideTrajectory()
        {
            foreach (GameObject point in _trajectoryPoints)
            {
                point.SetActive(false);
            }
        }
    }
}
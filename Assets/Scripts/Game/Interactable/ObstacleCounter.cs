using Game.Interfaces;
using Game.Managers;
using UnityEngine;
using Zenject;

namespace Game.Interactable
{
    public class ObstacleCounter : MonoBehaviour,IInteractable
    {
        [Inject] private LevelManager _levelManager;
        private void OnTriggerEnter(Collider other)
        {
            var obstacle = other.GetComponent<Obstacle>();
            if (obstacle != null)
            {
                _levelManager.IncreaseInteractObstacleCount();
            }
        }

        public void Interact()
        {
            //
        }
    }
}
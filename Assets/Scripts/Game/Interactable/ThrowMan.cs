using Game.Interfaces;
using Game.Managers;
using UnityEngine;
using Zenject;

namespace Game.Interactable
{
    public class ThrowMan : MonoBehaviour
    {
        [Inject] private SlingshotManager _slingshotManager;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float explosionForce = 10f; 
        [SerializeField] private float explosionRadius = 1f; 
        private bool _isFirstInteract;

        public void Throw(float pullStrength, float launchStrength)
        {
            rb.freezeRotation = false;
            rb.useGravity = true;
            rb.AddForce(transform.forward * (pullStrength + launchStrength), ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision other)
        {
            var interactable = other.transform.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
                if (_isFirstInteract) return;
                Explode();
                _slingshotManager.NextThrowMan();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            var interactable = other.GetComponent<IInteractable>();
            if (interactable != null)
            {
                if (_isFirstInteract) return;
                _slingshotManager.NextThrowMan();
            }
        }

        void Explode()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius); 

            foreach (Collider hitCollider in colliders)
            {
                var obstacleComponent = hitCollider.GetComponent<Obstacle>();
                if (obstacleComponent == null) continue;
                if (obstacleComponent.rb == null) continue;
                obstacleComponent.Release();
                obstacleComponent.rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
            _isFirstInteract = true;
        }
    }
}
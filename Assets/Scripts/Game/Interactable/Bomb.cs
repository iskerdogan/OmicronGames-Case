using Game.Interfaces;
using UnityEngine;

namespace Game.Interactable
{
    public class Bomb : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject model; 
        [SerializeField] private BoxCollider bombCollider; 
        [SerializeField] private float explosionForce = 10f; 
        [SerializeField] private float explosionRadius = 5f; 

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

            bombCollider.enabled = false;
            model.SetActive(false);
        }

        public void Interact()
        {
            Explode();
        }
    }
}
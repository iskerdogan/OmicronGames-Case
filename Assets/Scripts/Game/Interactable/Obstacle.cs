using Game.Interfaces;
using UnityEngine;

namespace Game.Interactable
{
    public class Obstacle : MonoBehaviour,IInteractable
    {
        public Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision other)
        {
            var obstacle = other.transform.GetComponent<Obstacle>();
            if (obstacle != null)
            {
                obstacle.Release();
            }
        }

        public void Release()
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }

        public void Interact()
        {
            Release();
        }
    }
}

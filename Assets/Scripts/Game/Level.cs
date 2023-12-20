using Game.Interactable;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class Level : MonoBehaviour
    {
        public GameObject level;
        public Obstacle[] obstacles;
    }
}
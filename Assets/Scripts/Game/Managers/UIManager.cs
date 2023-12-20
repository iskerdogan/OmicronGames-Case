using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

namespace Game.Managers
{
    public class UIManager : MonoBehaviour
    {
        [Inject] private InputManager _inputManager;
        [Inject] private GameManager _gameManager;
        [Inject] private LevelManager _levelManager;
        
        [SerializeField] private Button next;
        [SerializeField] private GameObject failPanel;
        [SerializeField] private GameObject tapToStart;
        [SerializeField] private TextMeshProUGUI levelText;


        private void Start()
        {
            _inputManager.InitialClick += InitialClick;
            tapToStart.SetActive(true);
            UpdateLevelText();
        }

        public void OnLevelComplete()
        {
            Invoke(nameof(NextGameObject), 1f);
        }

        private void NextGameObject()
        {
            next.gameObject.SetActive(true);
        }

        public void OnLevelFailed()
        {
            failPanel.SetActive(true);
        }

        public void OnRetryButton()
        {
            _levelManager.ReLoadScene();
        }

        public void OnNextButton()
        {
            _levelManager.ReLoadScene();
        }

        void InitialClick()
        {
            _inputManager.InitialClick -= InitialClick;
            tapToStart.SetActive(false);
        }
        
        public void UpdateLevelText() => levelText.text = "level " + (_gameManager.levelNumberToBuildLevel + 1);

        private void OnDestroy()
        {
            _inputManager.InitialClick -= InitialClick;
        }
        
    }
}
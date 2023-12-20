using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Managers
{
    public class LevelManager : MonoBehaviour
    {
        [Inject] private GameManager _gameManager;
        [Inject] private UIManager _uiManager;

        [SerializeField] public Level[] levels;
        [SerializeField] public float countdownTime = 10f;
    
        [HideInInspector] public Level currentLevel;

        private int _interactObstacleCount;
        private bool _isComplate;
        private void Start()
        {
            BuildLevel();
        }

        void BuildLevel()
        {
            var level = _gameManager.levelNumberToBuildLevel;
            if (levels.Length < 1) return;
            currentLevel = levels[level];
            currentLevel.level.SetActive(true);
        }
    
        public void StartCountDown()
        {
            StartCoroutine(CountDown());
        }

        private IEnumerator CountDown()
        {
            yield return new WaitForSeconds(countdownTime);
        
            if (CheckInteractObstacleCount())
            {
                LevelComplete();
            }
            else
            {
                LevelFailed();
            }
        }
        bool CheckInteractObstacleCount()
        {
            return currentLevel.obstacles.Length <= _interactObstacleCount;
        }
    
        public void IncreaseInteractObstacleCount()
        {
            _interactObstacleCount++;
            if (CheckInteractObstacleCount())
            {
                LevelComplete();
            }
        }

        #region LevelEnd

        public void LevelComplete()
        {
            if (_isComplate) return;
            _isComplate = true;
            _gameManager.LevelUp();
            _uiManager.OnLevelComplete();
        }

        public void LevelFailed()
        {
            _uiManager.OnLevelFailed();
        }

        public void ReLoadScene()
        {
            SceneManager.LoadSceneAsync(sceneBuildIndex: SceneManager.GetActiveScene().buildIndex);
            _uiManager.UpdateLevelText();
        }

        #endregion
    }
}
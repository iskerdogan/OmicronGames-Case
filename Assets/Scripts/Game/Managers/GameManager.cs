using UnityEngine;

namespace Game.Managers
{
    public class GameManager : MonoBehaviour
    {
        public int levelNumberToDisplay;
        public int levelNumberToBuildLevel;
        private readonly int _levelLoopMin = 0;
        private readonly int _levelLoopMax = 2;

        private void Awake()
        {
            LoadAllData();
        }

        public void LevelUp()
        {
            levelNumberToDisplay++;
            levelNumberToBuildLevel++;
            LoopLevels();
            SaveLevelNumber();
        }

        void LoopLevels() => levelNumberToBuildLevel = levelNumberToBuildLevel > _levelLoopMax ? _levelLoopMin : levelNumberToBuildLevel;

        #region SaveLoadData

        void LoadAllData()
        {
            LoadLevelNumber();
        }

        void SaveAllData()
        {
            SaveLevelNumber();
        }

        public void LoadLevelNumber()
        {
            levelNumberToDisplay = PlayerPrefs.GetInt("levelNumberToDisplay", 0);
            levelNumberToBuildLevel = PlayerPrefs.GetInt("levelNumberToBuildLevel", 0);
        }

        void SaveLevelNumber()
        {
            PlayerPrefs.SetInt("levelNumberToDisplay", levelNumberToDisplay);
            PlayerPrefs.SetInt("levelNumberToBuildLevel", levelNumberToBuildLevel);
        }

        #endregion
    }
}
using System;
using Game.Scripts.Consts;
using Game.Scripts.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Managers
{
    public class GameManager : Singleton<GameManager>, ISaveable
    {
        public GridManager GridManager;
        public PathFinder PathFinder;
        public EventManager EventManager;
        public ObjectPooling ObjectPooling;
        public EnemyManager EnemyManager;
        public UIManager UIManager;

        private bool _isFirstTimeStart;
        public bool IsFirstTimeStart => _isFirstTimeStart;

        private int _level;
        public int Level => _level;
        
        private void Start()
        {
            LoadData();
            
            ObjectPooling.Initialize();
            UIManager.Initialize();
            GridManager.Initialize();
            EnemyManager.Initialize();
            PathFinder.Initialize();

            SaveData();
            EventManager.OnGameLose += LoseGame;
        }

        private void LoseGame(object sender, EventArgs args)
        {
            
        }

        public void SaveData()
        {
            PlayerPrefs.SetInt(LevelConsts.GameStart, 1);
            _isFirstTimeStart = true;
            
            PlayerPrefs.SetInt(LevelConsts.Level, _level);
        }

        public void LoadData()
        {
            if (PlayerPrefs.GetInt(LevelConsts.GameStart) == 0)
                _isFirstTimeStart = true;
            else
                _isFirstTimeStart = false;
            
            _level = PlayerPrefs.GetInt(LevelConsts.Level);
        }

        public void ResetData()
        {
            PlayerPrefs.SetInt(LevelConsts.GameStart, 0);
            PlayerPrefs.SetInt(LevelConsts.Level, 0);
        }
        
        [Button]
        public void ResetSave()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}

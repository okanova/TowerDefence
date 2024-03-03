using System;
using Game.Scripts.Consts;
using Game.Scripts.Interfaces;
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
            GridManager = FindObjectOfType<GridManager>();
            PathFinder = FindObjectOfType<PathFinder>();
            EventManager = FindObjectOfType<EventManager>();
            ObjectPooling = FindObjectOfType<ObjectPooling>();
            EnemyManager = FindObjectOfType<EnemyManager>();
            UIManager = FindObjectOfType<UIManager>();
            
            GridManager.Initialize();
            PathFinder.Initialize();
            ObjectPooling.Initialize();
            EnemyManager.Initialize();
            UIManager.Initialize();
            
            SaveData();
            EventManager.GameLose += LoseGame;
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
            throw new NotImplementedException();
        }
    }
}

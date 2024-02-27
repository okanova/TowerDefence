using UnityEngine;

namespace Game.Scripts.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public GridManager GridManager;
        public PathFinder PathFinder;
        public EventManager EventManager;
        public ObjectPooling ObjectPooling;
        public EnemyManager EnemyManager;
        
        private void Start()
        {
            GridManager = FindObjectOfType<GridManager>();
            PathFinder = FindObjectOfType<PathFinder>();
            EventManager = FindObjectOfType<EventManager>();
            ObjectPooling = FindObjectOfType<ObjectPooling>();
            EnemyManager = FindObjectOfType<EnemyManager>();
            
            GridManager.Initialize();
            PathFinder.Initialize();
            ObjectPooling.Initialize();
            EnemyManager.Initialize();
        }
    }
}

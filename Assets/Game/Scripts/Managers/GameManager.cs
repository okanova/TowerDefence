using UnityEngine;

namespace Game.Scripts.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public GridManager GridManager;
        public PathFinder PathFinder;
        public EventManager EventManager;
        public ObjectPooling ObjectPooling;
        
        private void Start()
        {
            GridManager = FindObjectOfType<GridManager>();
            PathFinder = FindObjectOfType<PathFinder>();
            EventManager = FindObjectOfType<EventManager>();
            ObjectPooling = FindObjectOfType<ObjectPooling>();
            
            GridManager.Initialize();
            PathFinder.Initialize();
            ObjectPooling.Initialize();
        }
    }
}

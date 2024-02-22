using UnityEngine;

namespace Game.Scripts.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public GridManager GridManager;
        public PathFinder PathFinder;
        public EventManager EventManager;
        
        private void Start()
        {
            GridManager = FindObjectOfType<GridManager>();
            PathFinder = FindObjectOfType<PathFinder>();
            EventManager = FindObjectOfType<EventManager>();
            
            GridManager.Initialize();
            PathFinder.Initialize();
        }
    }
}

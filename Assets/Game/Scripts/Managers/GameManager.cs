using UnityEngine;

namespace Game.Scripts.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public GridManager GridManager;
        private void Start()
        {
            GridManager = FindObjectOfType<GridManager>();
            GridManager.Initialize();
        }
    }
}

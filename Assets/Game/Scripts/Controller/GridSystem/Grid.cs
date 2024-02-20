using System.Collections.Generic;
using Game.Scripts.Managers;
using UnityEngine;

namespace Game.Scripts.Controller.GridSystem
{
    public class Grid : MonoBehaviour
    {
        private Vector2Int _gridPosition;

        private bool _isGridEmpty = true;
        public bool IsGridEmpty {get { return _isGridEmpty; }}

       

        public List<Grid> Neighbours;

        public void Initialize()
        {
            _gridPosition = new Vector2Int((int)transform.localPosition.x, (int)transform.localPosition.z);
        }

        public void SetGridEmpty(bool value)
        {
            _isGridEmpty = value;
        }
    }
}

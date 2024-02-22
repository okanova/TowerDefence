using System;
using System.Collections.Generic;
using Game.Scripts.Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts.Controller.GridSystem
{
    public class Grid : MonoBehaviour
    {
        private Vector2Int _gridPosition;
        private GridSituation _gridSituation;

        public Material Material;
        
        public GridSituation GridSituation => _gridSituation;
        public List<Grid> Neighbors;
        
        public void Initialize()
        {
            _gridPosition = new Vector2Int((int)transform.localPosition.x, (int)transform.localPosition.z);
            Material = GetComponent<Renderer>().material;
        }

        public void SetGridSituation(GridSituation value)
        {
            _gridSituation = value;
        }

        public void OnMouseDown()
        {
            GameManager.Instance.GridManager.ClickGrid = this;
            GameManager.Instance.EventManager.TriggerPathFindEvent();
        }
    }
}

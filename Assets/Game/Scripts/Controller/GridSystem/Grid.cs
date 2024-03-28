using System;
using System.Collections.Generic;
using Game.Scripts.Controller.TowerSystem;
using Game.Scripts.Interfaces;
using Game.Scripts.Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts.Controller.GridSystem
{
    public class Grid : MonoBehaviour, ISaveable
    {
        private Vector2Int _gridPosition;
        private GridSituation _gridSituation;

        private Material _material;

        public Tower Tower;
        
        public GridSituation GridSituation => _gridSituation;
        public List<Grid> Neighbors;
        
        public void Initialize()
        {
            _gridPosition = new Vector2Int((int)transform.localPosition.x, (int)transform.localPosition.z);
            _material = GetComponent<Renderer>().material;
        }

        public void SetGridSituation(GridSituation value)
        {
            _gridSituation = value;
        }

        public void SetColor(Color color)
        {
            _material.SetColor("_BaseColor", color);
        }

        public void OnMouseDown()
        {
            if (!GameManager.Instance.GridManager.CanClick)
                return;

            GameManager.Instance.GridManager.ClickGrid = this;
            SetColor(Color.white);
            
            if (Tower == null)
            {
                GameManager.Instance.EventManager.TriggerPathFindEvent();
            }
            else
            {
                GameManager.Instance.UIManager.TowerUpgradePanelView.OpenPanel(Tower);
            }
        }

        public void ClearGrid()
        {
            if (Tower != null)
                GameManager.Instance.ObjectPooling.ReturnObjectToPool(Tower.gameObject);
            
            SetColor(Color.gray);
            GameManager.Instance.GridManager.ClickGrid = null;
        }

        public void CreateTower(TowerType type)
        {
            Tower tower = GameManager.Instance.ObjectPooling.GetObject(ObjectPoolType.Tower, transform).GetComponent<Tower>();
            tower.Initialize(type);
        }

        public void SaveData()
        {
            throw new NotImplementedException();
        }

        public void LoadData()
        {
            throw new NotImplementedException();
        }

        public void ResetData()
        {
            throw new NotImplementedException();
        }
    }
}

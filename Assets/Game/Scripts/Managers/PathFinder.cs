using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Events;
using UnityEngine;
using UnityEngine.Serialization;
using Grid = Game.Scripts.Controller.GridSystem.Grid;

namespace Game.Scripts.Managers
{
    public class PathFinder : MonoBehaviour
    {
        public Grid First;
        public Grid Last;

        public Grid[] Path;
        private Dictionary<List<Grid>, float> _pathDictionary = new Dictionary<List<Grid>, float>();

        private string _state;
        private int _key;

        public void Initialize()
        {
            OnEnable();
        }

        private void OnEnable()
        {
            GameManager.Instance.EventManager.OnChangePath += FindNewPath;
        }

        private void OnDisable()
        {
            GameManager.Instance.EventManager.OnChangePath -= FindNewPath;
        }

        private void FindNewPath(object sender, EventArgs args)
        {
            _pathDictionary.Clear();
            
            if (First == GameManager.Instance.GridManager.ClickGrid 
                || Last == GameManager.Instance.GridManager.ClickGrid)
                return;
            
            _state = "Search";
            _pathDictionary.Add(new List<Grid>{First}, 0);
            _key = 0;
            int count = 1;
            

            while (_state == "Search")
            {
                SearchNeighbors();
                count++;
                
                if (count == GameManager.Instance.GridManager.Grids.Length)
                {
                    _state = "NotFound";
                }
            }

            if (_state == "Found")
            {
                Path = _pathDictionary.ElementAt(_key).Key.ToArray();
                GameManager.Instance.GridManager.ClickGrid.SetGridSituation(GridSituation.Tower);
                GameManager.Instance.GridManager.CheckGrids();
            }
        }

        private void SearchNeighbors()
        {
            FindMinimumDistance();

            Grid lastGrid = _pathDictionary.ElementAt(_key).Key[^1];
            Grid[] temp = new Grid[_pathDictionary.ElementAt(_key).Key.Count + 1];

            for (int i = 0; i < _pathDictionary.ElementAt(_key).Key.Count; i++)
            {
                temp[i] = _pathDictionary.ElementAt(_key).Key[i];
            }
            
            _pathDictionary.Remove(_pathDictionary.ElementAt(_key).Key);
            
            foreach (var neighbor in lastGrid.Neighbors)
            {
                if (neighbor.GridSituation != GridSituation.Tower 
                     && neighbor != GameManager.Instance.GridManager.ClickGrid && (_pathDictionary.Count == 0 
                         || !_pathDictionary.ElementAt(_key).Key.Contains(neighbor)))
                {
                    temp[^1] = neighbor;
                    float distance = FindDistance(neighbor);

                    _pathDictionary.TryAdd(temp.ToList(), temp.Length + distance);

                    if (distance == 0)
                    {
                        _state = "Found";
                        _key = _pathDictionary.Count - 1;
                        return;
                    }
                }
            }
        }

        private void FindMinimumDistance()
        {
            float min = Int32.MaxValue;
        
            for (int i = 0; i < _pathDictionary.Count; i++)
            {
                if (_pathDictionary[_pathDictionary.Keys.ElementAt(i)] < min)
                {
                    min = _pathDictionary[_pathDictionary.Keys.ElementAt(i)];
                    _key = i;
                }
            }
        }

        private float FindDistance(Grid lastGrid)
        {
            return Vector3.Distance(Last.transform.position, lastGrid.transform.position);
        }
    }
}
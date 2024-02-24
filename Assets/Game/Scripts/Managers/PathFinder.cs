using System;
using System.Collections;
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

        private List<Grid> _searcherList = new List<Grid>();

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
            if (_state == "Search") 
                return;
            
            _pathDictionary.Clear();
            _searcherList.Clear();

            if (First == GameManager.Instance.GridManager.ClickGrid
                || Last == GameManager.Instance.GridManager.ClickGrid)
                return;
            
            _state = "Search";
            _pathDictionary.Add(new List<Grid>{First}, 0);
            _key = 0;
            
            while (_state == "Search")
            {
                SearchNeighbors();
                
                if (_searcherList.Count == GameManager.Instance.GridManager.Grids.Length)
                {
                    _state = "NotFound";
                    return;
                }
            }

            if (_state == "Found")
            {
                GameManager.Instance.GridManager.ClickGrid.SetGridSituation(GridSituation.Tower);
                GameManager.Instance.GridManager.CheckGrids();
            }
        }

        private void SearchNeighbors()
        {
            FindMinimumDistance();
            
            Grid lastGrid = _pathDictionary.ElementAt(_key).Key[^1];
            _searcherList.Add(lastGrid);

            Grid[] temp = new Grid[_pathDictionary.ElementAt(_key).Key.Count + 1];

            for (int i = 0; i < _pathDictionary.ElementAt(_key).Key.Count; i++)
            {
                temp[i] = _pathDictionary.ElementAt(_key).Key[i];
            }
            

            foreach (var neighbor in lastGrid.Neighbors)
            {
                if (neighbor.GridSituation != GridSituation.Tower 
                     && neighbor != GameManager.Instance.GridManager.ClickGrid &&
                     !_pathDictionary.ElementAt(_key).Key.Contains(neighbor) && !_searcherList.Contains(neighbor))
                {
                    temp[^1] = neighbor;
                    float distance = FindDistance(neighbor);

                    _pathDictionary.TryAdd(temp.ToList(), temp.Length + distance);

                    if (distance == 0)
                    {
                        _state = "Found";
                        Path = temp;
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
                if (_pathDictionary[_pathDictionary.Keys.ElementAt(i)] <= min
                    && !_searcherList.Contains(_pathDictionary.ElementAt(i).Key[^1]))
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
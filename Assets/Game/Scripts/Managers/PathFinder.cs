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
        public PathClass[] Paths;
       
        private Dictionary<List<Grid>, float> _pathDictionary = new Dictionary<List<Grid>, float>();

        private string _state;
        public string PathState => _state;
        
        private int _key;
        private int _pathCount;

        private List<Grid> _searcherList = new List<Grid>();

        public string BuyState;

        public void Initialize()
        {
            BuyState = "CanBuy";
            OnEnable();

            for (int i = 0; i < GameManager.Instance.GridManager.Grids.GetLength(0); i++)
            {
                for (int j = 0; j < GameManager.Instance.GridManager.Grids.GetLength(1); j++)
                {
                    for (int k = 0; k < Paths.Length; k++)
                    {
                        if (GameManager.Instance.GridManager.Grids[i, j] == Paths[k].Enter)
                            GameManager.Instance.GridManager.Grids[i, j].SetGridSituation(GridSituation.Enter);
                        else if (GameManager.Instance.GridManager.Grids[i, j] == Paths[k].Exit)
                            GameManager.Instance.GridManager.Grids[i, j].SetGridSituation(GridSituation.Exit);
                    }
                }
            }

            object obj = new object();
            EventArgs args = new EventArgs();
            FindNewPath(obj, args);
          
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
            
            
            if (GameManager.Instance.GridManager.ClickGrid != null && (
                    GameManager.Instance.GridManager.ClickGrid.GridSituation == GridSituation.Enter
                || GameManager.Instance.GridManager.ClickGrid.GridSituation == GridSituation.Exit 
                || GameManager.Instance.GridManager.ClickGrid.GridSituation == GridSituation.Tower))
                return;
            
            for (int i = 0; i < Paths.Length; i++)
            {
                _pathDictionary.Clear();
                _searcherList.Clear();
                _pathCount = 0;

                _state = "Search";
                _pathDictionary.Add(new List<Grid>{Paths[i].Enter}, 0);
                _key = 0;
            
                while (_state == "Search")
                {
                    SearchNeighbors(i);
                
                    if (_searcherList.Count == GameManager.Instance.GridManager.Grids.Length)
                    {
                        _state = "NotFound";
                        BuyState = "NotClear";
                        return;
                    }

                    if (_pathCount == Paths.Length)
                        _state = "Found";
                }
            }
            
            if (_state == "Found")
            {
                if (BuyState != "CanBuy")
                {
                    BuyState = "CanBuy";
                    GameManager.Instance.UIManager.OpenBuyTowerPanel();
                    GameManager.Instance.GridManager.CanClick = false;
                }
                else
                {
                    GameManager.Instance.GridManager.ClearGridColors();
                    if ( GameManager.Instance.GridManager.ClickGrid != null)
                        GameManager.Instance.GridManager.ClickGrid.SetGridSituation(GridSituation.Tower);
                
                    SetGridForms();
                    GameManager.Instance.GridManager.CheckGrids();
                    BuyState = "Clear";
                    GameManager.Instance.GridManager.CanClick = true;
                }
            }
        }

        private void SearchNeighbors(int count)
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
                    float distance = FindDistance(neighbor, Paths[count].Exit);

                    _pathDictionary.TryAdd(temp.ToList(), temp.Length + distance);

                    if (distance == 0)
                    {
                        _pathCount = count + 1;
                        _state = "NextStep";
                        
                        if (BuyState == "CanBuy")
                            Paths[count].Path = temp;
                        
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

        private float FindDistance(Grid current, Grid end)
        {
            return Vector3.Distance(end.transform.position, current.transform.position);
        }

        private void SetGridForms()
        {
            for (int i = 0; i < Paths.Length; i++)
            {
                for (int j = 0; j < Paths[i].Path.Length; j++)
                {
                    if (Paths[i].Path[j].GridSituation != GridSituation.Enter && Paths[i].Path[j].GridSituation != GridSituation.Exit)
                        Paths[i].Path[j].SetGridSituation(GridSituation.Path);
                }
            }
        }
    }


    [Serializable]
    public class PathClass
    {
        public Grid Enter;
        public Grid Exit;
        public Grid[] Path;
    }
}
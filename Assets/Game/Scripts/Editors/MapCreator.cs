using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Managers;
using Sirenix.OdinInspector;
using UnityEngine;
using Grid = Game.Scripts.Controller.GridSystem.Grid;

namespace Game.Scripts.Editors
{
    public class MapCreator : MonoBehaviour
    {
        [TabGroup("GRID")][SerializeField] private Transform _gridParent;
        [TabGroup("GRID")][SerializeField] private Grid _grid;
        [TabGroup("GRID")][SerializeField] private Vector2 _gridCoordinateValue;
        
        [Button]
        public void CreateGrids()
        {
            List<Grid> grids = FindObjectsOfType<Grid>().ToList();
            
            if (grids.Count > 0)
            {
                while (grids.Count > 0)
                {
                    Grid grid = grids[0];
                    grids.Remove(grid);
                    DestroyImmediate(grid.gameObject);
                }
            }
            
            Grid[,] gridArray = new Grid[(int)_gridCoordinateValue.x,(int)_gridCoordinateValue.y];

            for (int i = 0; i < _gridCoordinateValue.y; i++)
            {
                for (int j = 0; j < _gridCoordinateValue.x; j++)
                {
                    Grid grid = Instantiate(_grid);
                    grid.transform.SetParent(_gridParent);
                    grid.transform.localPosition = new Vector3(j, 0, i);
                    gridArray[j, i] = grid;
                }
            }

            GridManager gridManager = FindObjectOfType<GridManager>();
            gridManager.Grids = gridArray;
            
            SetCameraPosition();
            FindNeighbours();
            SetGridNames();
        }

        [Button]
        public void SetCameraPosition()
        {
            Camera camera = Camera.main;
            camera.transform.position = new Vector3(_gridCoordinateValue.x / 2, 10, _gridCoordinateValue.y / 2);
        }

        [Button]
        public void FindNeighbours()
        {
            GridManager gridManager = FindObjectOfType<GridManager>();
            
            for (int i = 0; i < gridManager.Grids.GetLength(0); i++)
            {
                for (int j = 0; j < gridManager.Grids.GetLength(1); j++)
                {
                    if (j > 0)
                        gridManager.Grids[i,j].Neighbors.Add(gridManager.Grids[i,j - 1]);
                    
                    if (j < gridManager.Grids.GetLength(1) - 1)
                        gridManager.Grids[i,j].Neighbors.Add(gridManager.Grids[i,j + 1]);
                    
                    if (i > 0)
                        gridManager.Grids[i,j].Neighbors.Add(gridManager.Grids[i - 1,j]);
                    
                    if (i < gridManager.Grids.GetLength(0) - 1)
                        gridManager.Grids[i,j].Neighbors.Add(gridManager.Grids[i + 1,j]);
                }
            }
        }

        [Button]
        public void SetGridNames()
        {
            
            List<Grid> grids = FindObjectsOfType<Grid>().ToList();
            grids.Reverse();
            for (int i = 0; i < grids.Count; i++)
            {
                grids[i].name = "" + i;
            }
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using Grid = Game.Scripts.Controller.GridSystem.Grid;

namespace Game.Scripts.Managers
{
    public class GridManager : MonoBehaviour
    {
        public Grid[,] Grids;
        public Grid ClickGrid;

        public bool CanClick;
        
        public void Initialize()
        {
            if (Grids == null || Grids.Length == 0)
            {
                List<Grid> grids = FindObjectsOfType<Grid>().ToList();

                int x = 0;
                int z = 0;
                
                for (int i = 0; i < grids.Count; i++)
                {
                    if (grids[i].transform.localPosition.x > x)
                        x = (int)grids[i].transform.localPosition.x;
                    if (grids[i].transform.localPosition.z > z)
                        z = (int)grids[i].transform.localPosition.z;
                }

                Grids = new Grid[x + 1, z + 1];

                for (int i = 0; i < grids.Count; i++)
                {
                    Grids[(int)grids[i].transform.localPosition.x, (int)grids[i].transform.localPosition.z] = grids[i];
                }
            }
            
            
            for (int i = 0; i < Grids.GetLength(0); i++)
            {
                for (int j = 0; j < Grids.GetLength(1); j++)
                {
                    Grids[i,j].Initialize();
                }
            }
        }

        public void ClearGridColors()
        {
            for (int i = 0; i < Grids.GetLength(0); i++)
            {
                for (int j = 0; j < Grids.GetLength(1); j++)
                {
                    Grids[i, j].SetColor(Color.gray);
                    if (Grids[i,j].GridSituation == GridSituation.Path)
                        Grids[i,j].SetGridSituation(GridSituation.Empty);
                }
            }
        }

        public void CheckGrids()
        {
            for (int i = 0; i < Grids.GetLength(0); i++)
            {
                for (int j = 0; j < Grids.GetLength(1); j++)
                {
                    if (Grids[i,j].GridSituation == GridSituation.Enter || Grids[i,j].GridSituation == GridSituation.Exit)
                    {
                        Grids[i, j].SetColor(Color.yellow);
                    }
                    else if (Grids[i,j].GridSituation == GridSituation.Tower)
                    {
                        Grids[i, j].SetColor(Color.red);
                    }
                    else if (Grids[i,j].GridSituation == GridSituation.Path)
                    {
                        Grids[i, j].SetColor(Color.cyan);
                    }
                    else
                    {
                        Grids[i, j].SetColor(Color.gray);
                    }
                }
            }
        }
    }
}

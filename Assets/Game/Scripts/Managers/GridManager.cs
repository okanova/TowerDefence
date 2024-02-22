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

        public void CheckGrids()
        {
            for (int i = 0; i < Grids.GetLength(0); i++)
            {
                for (int j = 0; j < Grids.GetLength(1); j++)
                {
                    if (Grids[i,j] == GameManager.Instance.PathFinder.First || Grids[i,j] == GameManager.Instance.PathFinder.Last)
                    {
                        Grids[i,j].Material.SetColor("_BaseColor",Color.green);
                    }
                    else if (Grids[i,j].GridSituation == GridSituation.Tower)
                    {
                        Grids[i,j].Material.SetColor("_BaseColor",Color.red);
                    }
                    else if (GameManager.Instance.PathFinder.Path.Contains(Grids[i, j]))
                    {
                        Grids[i, j].SetGridSituation(GridSituation.Path);
                        Grids[i,j].Material.SetColor("_BaseColor",Color.cyan);
                    }
                    else
                    {
                        Grids[i,j].Material.SetColor("_BaseColor",Color.gray);
                    }
                }
            }
        }
    }
}

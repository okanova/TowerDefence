using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using Grid = Game.Scripts.Controller.GridSystem.Grid;

namespace Game.Scripts.Managers
{
    public class GridManager : Singleton<GridManager>
    {
        public Grid[,] Grids;


        public void Initialize()
        {
            if (Grids.Length == 0)
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

                Grids = new Grid[x, z];
                
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
    }
}

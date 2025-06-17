using UnityEngine;
using System.Collections.Generic;
using Utils;

namespace Navigation
{
    public class Pathfinder : MonoBehaviour
    {
        HexGrid grid;

        int EstimateCost(HexCell start, HexCell end)
        {
            int result = HexCell.Difference(start, end);
            return result;
        }

        public Path FindPath(HexCell start, HexCell goal)
        {
            Path result = new();
            result.Add(start);

            PriorityQueue<HexCell, int> openSet = new();

            Dictionary<HexCell, Path> nodePahths= new();

            List<HexCell> neighbors = grid.GetNeighbors();

            return result;
        }

        void Start()
        {
        }
    }

}

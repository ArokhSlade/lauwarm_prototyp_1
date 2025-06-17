using UnityEngine;
using System.Collections.Generic;
using Utils;

namespace Navigation
{
    public class Pathfinder : MonoBehaviour
    {
        [SerializeField] HexGrid grid;

        int EstimateRestCost(HexCell start, HexCell end)
        {
            int result = HexCell.Difference(start, end);
            return result;
        }

        int EstimateFullCost(Path path, HexCell goal)
        {
            int result;
            result = path.Length + EstimateRestCost(path.End, goal);

            return result;
        }

        public Path FindPath(HexCell start, HexCell goal)
        {
            Path result = new();
            result.Add(start);

            PriorityQueue<HexCell, int> openSet = new();

            Dictionary<HexCell, Path> nodePaths = new();

            List<HexCell> neighbors = grid.GetNeighbors(start);

            return result;
        }

        void Start()
        {
        }
    }

}

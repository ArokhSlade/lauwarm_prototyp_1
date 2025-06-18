using System.Collections.Generic;
using UnityEngine;



namespace Navigation
{
    public class HexAgent : MonoBehaviour
    {
        [SerializeField] HexCell start;
        [SerializeField] HexCell goal;
        [SerializeField] Pathfinder pathfinder;
        [SerializeField] float debugRadius;

        List<Vector3> GetPathPositions()
        {
            List<Vector3> result = new();

            if (!(pathfinder && start && goal))
            {
                return null;
            }

            Path path = pathfinder.FindPath(start, goal);
            HexGrid hexGrid = pathfinder.HexGrid;
            Grid grid = hexGrid.Grid;

            foreach (var hexCell in path)
            {
                Vector3 position = grid.CellToWorld(hexGrid.ToVec3Int(hexCell.Coords));
                result.Add(position);
            }

            return result;
        }

        private void OnDrawGizmos()
        {
            if (debugRadius <= 0f)
            {
                return;
            }

            Gizmos.color = Color.yellow;
            var positions = GetPathPositions();
            foreach (var position in positions)
            {
                Gizmos.DrawSphere(position, debugRadius);
            }
        }
    }
}

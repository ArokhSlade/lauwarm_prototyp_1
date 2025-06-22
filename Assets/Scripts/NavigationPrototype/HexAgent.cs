using System.Collections.Generic;
using UnityEngine;



namespace Navigation
{
    public class HexAgent : MonoBehaviour
    {
        [SerializeField] Transform start;
        [SerializeField] Transform goal;
        [SerializeField] Pathfinder pathfinder;
        [SerializeField] float debugRadius;

        List<Vector3> GetPathPositions()
        {
            List<Vector3> result = new();

            if (!(pathfinder && start && goal))
            {
                return null;
            }

            HexGrid hexGrid = pathfinder.HexGrid;
            Grid grid = hexGrid.Grid;
            var gridCell = grid.WorldToCell(start.position);
            HexCell startHex = new(gridCell.x,gridCell.y);
            Debug.Log($"{start.position}, {gridCell}, {startHex}");
            gridCell = grid.WorldToCell(goal.position);
            HexCell goalHex = new (gridCell.x, gridCell.y);

            Path path = pathfinder.FindPath(startHex, goalHex);

            foreach (var hexCell in path)
            {
                Vector3 position = grid.GetCellCenterWorld(new Vector3Int(hexCell.X, hexCell.Y, 0));
                result.Add(position);
            }

            return result;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var positions = GetPathPositions();
                foreach (var position in positions)
                {
                    Debug.Log($"{position}");
                }
            }
        }

        //private void OnDrawGizmos()
        //{
        //    if (debugRadius <= 0f)
        //    {
        //        return;
        //    }

        //    Gizmos.color = Color.yellow;
        //    var positions = GetPathPositions();
        //    foreach (var position in positions)
        //    {
        //        Gizmos.DrawSphere(position, debugRadius);
        //    }
        //}
    }
}

using System.Collections.Generic;
using UnityEngine;

namespace Navigation
{
    public class HexGrid : MonoBehaviour
    {
		[SerializeField] Vector2Int dimensions;
        [SerializeField] Grid grid;
        [SerializeField] float debugRadius;
		[SerializeField] Vector3 worldCenter;

		public Grid Grid => grid;

		public Vector3Int ToVec3Int(Vector2Int coords)
		{
			var result = new Vector3Int(coords.x, 0, coords.y);
			return result;
		}

        private void OnDrawGizmos()
        {
			for (int x = 0; x < dimensions.x; ++x)
			{
				for (int y = 0; y < dimensions.y; ++y)
				{
					Gizmos.color = Color.blue;

					var cellCoords = new Vector2Int(x, y);
					cellCoords -= dimensions / 2;

					Vector3 cellCenter = grid.CellToWorld(ToVec3Int(cellCoords));

					Gizmos.DrawSphere(cellCenter, debugRadius);
				}
			}
        }

        public List<HexCell> GetNeighbors(HexCell cell)
		{
			List<HexCell> result = new();
			HexCell top, topLeft, topRight, bottom, bottomLeft, bottomRight;

			// if hex-grid is flat-top style
			int x = cell.X;
			int y = cell.Y;
			top = new(x, y + 1);
			topLeft = new(x - 1, y + 1);
			topRight = new(x + 1, y + 1);
            bottom = new(x, y - 1);
            bottomLeft = new(x - 1, y - 1);
            bottomRight = new(x + 1, y - 1);
			result.Add(top);
			result.Add(topLeft);
			result.Add(topRight);
			result.Add(bottom);
			result.Add(bottomLeft);
			result.Add(bottomRight);

            return result;
		}
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace Navigation
{
	enum HexType
	{
		PointyTop,
		FlatTop
	}

    public class HexGrid : MonoBehaviour
    {
		[SerializeField] Vector2Int dimensions;
        [SerializeField] Grid grid;
        [SerializeField] float debugRadius = 1f;
		[SerializeField] Vector3 worldCenter;
		[SerializeField] List<Vector2Int> blockedCells = new();
		[SerializeField] HashSet<Vector2Int> blockedCellsHashSet = new();

		const HexType hexType = HexType.PointyTop;
		
		static HexCell invalidCell = new(int.MaxValue, int.MaxValue);
        public static HexCell INVALID_CELL => invalidCell;

		public Grid Grid => grid;


		private void OnDrawGizmos()
		{
			for (int x = 0; x < dimensions.x; ++x)
			{
				for (int y = 0; y < dimensions.y; ++y)
				{
					Gizmos.color = Color.blue;
					var cellCoords = new Vector2Int(x, y);
					cellCoords -= dimensions / 2;

					Vector3 cellCenter = grid.GetCellCenterWorld(new Vector3Int(cellCoords.x, cellCoords.y, 0));

					Gizmos.DrawSphere(cellCenter, debugRadius);
				}
			}

			//Debug.Log($"{grid.GetLayoutCellCenter()}");
			//for (int x = 0; x < dimensions.x; ++x)
			//{
			//	for (int y = 0; y < dimensions.y; ++y)
			//	{
			//		var cellCoords = new Vector2Int(x, y);
			//		cellCoords -= dimensions / 2;
			//		var cellV3 = new Vector3Int(x, 0, y);
			//		Vector3 cellCenter = grid.GetCellCenterWorld(cellV3);
			//	}
			//}
		}


		public List<HexCell> GetNeighbors(HexCell cell)
		{
			List<HexCell> result = new();
			HexCell top, topLeft, topRight, bottom, bottomLeft, bottomRight, left, right;

			// if hex-grid is flat-top style
			int x = cell.X;
			int y = cell.Y;
			switch(hexType)
			{
				case HexType.FlatTop:
					top = TryGetCellAt(x, y + 1);
					topLeft = TryGetCellAt(x - 1, y + 1);
					topRight = TryGetCellAt(x + 1, y + 1);
					bottom = TryGetCellAt(x, y - 1);
					bottomLeft = TryGetCellAt(x - 1, y - 1);
					bottomRight = TryGetCellAt(x + 1, y - 1);
					result.Add(topLeft);
					result.Add(top);
					result.Add(topRight);
					result.Add(bottom);
					result.Add(bottomRight);
					result.Add(bottomLeft);
					break;
				case HexType.PointyTop:
					bool rowIsOdd = (y & 1) == 1;

					left = TryGetCellAt(x - 1, y);
					right = TryGetCellAt(x + 1, y);

					if (rowIsOdd)
					{
						topLeft = TryGetCellAt(x, y + 1);
						topRight = TryGetCellAt(x+1, y + 1);
						bottomLeft = TryGetCellAt(x, y - 1);
						bottomRight = TryGetCellAt(x + 1, y - 1);                        
                    }
					else
					{
						topLeft = TryGetCellAt(x - 1, y + 1);
						topRight = TryGetCellAt(x, y + 1);
						bottomLeft = TryGetCellAt(x - 1, y - 1);
						bottomRight = TryGetCellAt(x, y - 1);
                    }

                    if (left != INVALID_CELL) result.Add(left);
                    if (topLeft != INVALID_CELL) result.Add(topLeft);
                    if (topRight != INVALID_CELL) result.Add(topRight);
                    if (right != INVALID_CELL) result.Add(right);
                    if (bottomRight != INVALID_CELL) result.Add(bottomRight);
                    if (bottomLeft != INVALID_CELL) result.Add(bottomLeft);

                    break;
			}

            return result;
		}

		HexCell TryGetCellAt(int x, int y)
		{
			HexCell result = new HexCell(x, y);

            if (blockedCells.Contains(new Vector2Int(x,y)))
			{
				result = INVALID_CELL;
			}

			return result;
		}


    }
}
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
		const HexType hexType = HexType.PointyTop;

		public Grid Grid => grid;

		public Vector3Int ToVec3Int(Vector2Int coords)
		{
			var result = new Vector3Int(coords.x, 0, coords.y);
			return result;
		}

		public HexCell FromVec3Int(Vector3Int coords)
		{
			HexCell result = new HexCell(coords.x, coords.z);

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

					Vector3 cellCenter = grid.GetCellCenterWorld(ToVec3Int(cellCoords));

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
					break;
				case HexType.PointyTop:
					bool rowIsOdd = (y | 1) == 1;

					left = new(x - 1, y);
					right = new(x + 1, y);

					if (rowIsOdd)
					{
						topLeft = new(x, y + 1);
						topRight = new(x+1, y + 1);
						bottomLeft = new(x, y - 1);
						bottomRight = new(x + 1, y - 1);                        
                    }
					else
					{
						topLeft = new(x - 1, y + 1);
						topRight = new(x, y + 1);
						bottomLeft = new(x - 1, y - 1);
						bottomRight = new(x, y - 1);
                    }
                    result.Add(left);
                    result.Add(topLeft);
                    result.Add(topRight);
                    result.Add(right);
                    result.Add(bottomLeft);
                    result.Add(bottomRight);
                    break;
			}

            return result;
		}
    }
}
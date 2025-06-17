using System.Collections.Generic;
using UnityEngine;

namespace Navigation
{
    public class HexGrid : MonoBehaviour
    {
        Grid grid;
		
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
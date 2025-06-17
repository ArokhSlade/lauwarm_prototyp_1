using System.Collections;
using UnityEngine;

namespace Navigation
{
    public class HexCell : MonoBehaviour
    {
        Vector2Int coords;

        public HexCell(int x, int y)
        {
            coords = new Vector2Int(x, y);
        }

        public int X => coords.x;
        public int Y => coords.y;

        static int Abs(int x)
        {
            return x < 0 ? -x : x;
        }

        public static int Difference(HexCell first, HexCell second)
        {
            int result = Abs(second.coords.y - first.coords.y) - Abs(second.coords.x - first.coords.x);

            return result;
        }
    }
}
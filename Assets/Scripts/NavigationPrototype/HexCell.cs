using System;
using System.Collections;
using UnityEngine;

namespace Navigation
{
    public struct HexCell : IEquatable<HexCell>
    {
        Vector2Int coords;
        public Vector2Int Coords => coords;

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

        /// <summary>
        /// manhatten distance.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static int Difference(HexCell first, HexCell second)
        {
            int result = Abs(second.coords.y - first.coords.y) + Abs(second.coords.x - first.coords.x);

            return result;
        }

        public override string ToString()
        {
            return coords.ToString();
        }

        public bool Equals(HexCell other)
        {
            bool result = coords == other.coords;
            return result;
        }

        public static bool operator ==(HexCell lhs, HexCell rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(HexCell lhs, HexCell rhs)
        {
            return !lhs.Equals(rhs);
        }
    }
}
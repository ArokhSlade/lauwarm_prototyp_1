using System.Collections.Generic;
using UnityEngine;

namespace Navigation
{
    public class Path
    {
        List<HexCell> path;

        public int Length()
        {
            return path.Count;
        }

        public void Add(HexCell node)
        {
            path.Add(node);
        }
    }
}
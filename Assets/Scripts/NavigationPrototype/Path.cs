using System.Collections.Generic;
using UnityEngine;

namespace Navigation
{
    public class Path
    {
        List<HexCell> path;

        public HexCell Begin => path[0];
        public HexCell End => path[-1];

        public int Length => path.Count;

        public void Add(HexCell node)
        {
            path.Add(node);
        }
    }
}
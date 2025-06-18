using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Navigation
{
    public class Path : IEnumerable<HexCell>
    {
        List<HexCell> path = new();

        public HexCell Begin => path.Count > 0 ? path[0] : new HexCell(0,0);
        public HexCell End => path.Count > 0 ? path[path.Count-1] : new HexCell(0, 0);

        public int Length => path.Count;

        public void Add(HexCell node)
        {
            path.Add(node);
        }

        /// <summary>
        /// deep copy
        /// </summary>
        /// <returns></returns>
        public Path Clone()
        {
            Path result = new Path();
            result.path.AddRange(this.path);
            return result;
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return path.GetEnumerator();
        }

        public IEnumerator<HexCell> GetEnumerator()
        {
            return path.GetEnumerator();
        }
    }
}
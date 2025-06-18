using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Navigation
{
    public class Path : IEnumerable<HexCell>
    {
        List<HexCell> path;

        public HexCell Begin => path[0];
        public HexCell End => path[-1];

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
            return (IEnumerator)path;
        }

        public IEnumerator<HexCell> GetEnumerator()
        {
            return (IEnumerator<HexCell>)path;
        }
    }
}
using UnityEngine;
using System.Collections.Generic;
using Utils;
using Unity.VisualScripting;
using NUnit.Framework;

namespace Navigation
{
    public class Pathfinder : MonoBehaviour
    {
        [SerializeField] HexGrid hexGrid;
        public HexGrid HexGrid => hexGrid;

        int EstimateRestCost(HexCell start, HexCell end)
        {
            int result = HexCell.Difference(start, end);
            return result;
        }

        int EstimateFullCost(Path path, HexCell goal)
        {
            int result;
            result = path.Length + EstimateRestCost(path.End, goal);

            return result;
        }

        void UpdateFringe(SortedSet<Path> paths, Dictionary<HexCell, Path> fringe, Path path, List<HexCell> neighbors)
        {
            paths.Remove(path);

            foreach (var neighbor in neighbors)
            {
                Path newPath = path.Clone();
                newPath.Add(neighbor);

                bool alreadyDiscovered = fringe.ContainsKey(neighbor);
                if (alreadyDiscovered)
                {
                    Path oldPath = fringe[neighbor];
                    bool newPathIsCheaper = newPath.Length < oldPath.Length;

                    if (newPathIsCheaper)
                    {
                        paths.Remove(oldPath);
                        paths.Add(newPath);
                        fringe[neighbor] = newPath;
                    }
                }
                else
                {

                    paths.Add(newPath);
                    fringe[neighbor] = newPath;
                }
            }
        }

        // TODO(Gerald, 2025 06 18): terminate if no path to the goal exists.
        public Path FindPath(HexCell start, HexCell goal)
        {
            Path result = null;
            Path currentPath = new Path();
            currentPath.Add(start);

            Dictionary<HexCell, Path> fringe = new();
            fringe[start] = currentPath;

            IComparer<Path> comparer = Comparer<Path>.Create((p1, p2) => {
                if (p1 == p2)
                {
                    return 0;
                }
                else if (p1.Length < p2.Length)
                { 
                    return -1; 
                }
                else
                {
                    return 1;
                }
            });

            SortedSet<Path> paths = new(comparer);
            paths.Add(currentPath);

            bool searchSuccessful = false;

            while (!searchSuccessful)
            {
                List<HexCell> neighbors = hexGrid.GetNeighbors(currentPath.End );
                Debug.Log($"{paths}, {fringe}, {currentPath}, {neighbors}");
                UpdateFringe(paths, fringe, currentPath, neighbors);            

                if (fringe.ContainsKey(goal))
                {
                    result = fringe[goal];
                    searchSuccessful = true;
                } else
                {
                    currentPath = paths.Min;
                }
            }

            Debug.Assert(result != null);
            Debug.Assert(paths.Contains(result));
            Debug.Assert(fringe.ContainsKey(goal));

            return result;
        }

        void Start()
        {
        }
    }

}

using UnityEngine;
using System.Collections.Generic;
using Utils;
using Unity.VisualScripting;
using NUnit.Framework;

namespace Navigation
{
    public class Pathfinder : MonoBehaviour
    {
        [SerializeField] HexGrid grid;

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

        List<Path> UpdateFringe(SortedSet<Path> paths, Path path, List<HexCell> neighbors)
        {
            List<Path> result = new ();
            paths.Remove(path);
            
            foreach(var neighbor in neighbors)
            {
                Path newPath = path.Clone();
                newPath.Add(neighbor);
                paths.Add(newPath);
                result.Add(newPath);
            }

            return result;
        }

        void UpdateCostEstimatesAndFringe(Dictionary<HexCell, int> costEstimates, Dictionary<HexCell, Path> fringeCellsToPathsMap, List<Path> newPaths, HexCell goal)
        {
            foreach(Path path in newPaths)
            {
                HexCell cell = path.End;
                int newCostEstimate = EstimateFullCost(path, goal);
                if (!costEstimates.ContainsKey(cell) || costEstimates[cell] < newCostEstimate)
                {
                    costEstimates[cell] = newCostEstimate;
                    fringeCellsToPathsMap[cell] = path;
                }
            }
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

            IComparer<Path> comparer = Comparer<Path>.Create((p1, p2) => p1.Length.CompareTo(p2.Length));

            SortedSet<Path> paths = new(comparer);
            paths.Add(currentPath);

            bool searchSuccessful = false;

            while (!searchSuccessful)
            {
                List<HexCell> neighbors = grid.GetNeighbors(start);
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

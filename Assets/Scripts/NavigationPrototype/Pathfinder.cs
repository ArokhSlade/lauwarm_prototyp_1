using UnityEngine;
using System.Collections.Generic;
using Utils;
using Unity.VisualScripting;
using NUnit.Framework;
using System.Linq;

namespace Navigation
{
    class HexBucketList
    {
        class HexBucket
        {
            Queue<HexCell> cells;
        }

        SortedDictionary<int, HexBucket> prioBuckets;

    }

    public class Pathfinder : MonoBehaviour
    {
        [SerializeField] HexGrid hexGrid;
        public HexGrid HexGrid => hexGrid;

        // TODO(Gerald, 2025 06 18): terminate if no path to the goal exists.
        // NOTE(Gerald, 2025 06 22): optimization idea: instead of storing paths, store predecessor for each head
        // then we can reconstruct the shortest path once a head equals goal.
        public Path FindPath(HexCell start, HexCell goal)
        {
            Path result = null;
            Path currentPath = new Path();
            HexCell currentHead = start;
            currentPath.Add(currentHead);

            SortedDictionary<int, HashSet<HexCell>> fringe = new();
            Dictionary<HexCell, int> visited = new();
            Dictionary<HexCell, Path> paths = new();

            int currentCost = EstimateFullCost(currentPath, goal);
            AddToFringe(fringe, currentCost, currentHead);
            
            paths[currentHead] = currentPath;

            bool searchSuccessful = false;
            int debugCount = 0;
            while (!searchSuccessful && debugCount < 1000)
            {
                if (debugCount >= 500)
                {
                    Debug.LogError("probably infinite loop");
                }
                if (paths.ContainsKey(goal))
                {
                    result = paths[goal];
                    searchSuccessful = true;
                }
                else
                {
                    List<HexCell> neighbors = hexGrid.GetNeighbors(currentHead);
                    // - remove currentHead from fringe
                    Debug.Assert(fringe.ContainsKey(currentCost));
                    Debug.Assert(fringe[currentCost].Contains(currentHead));
                    fringe[currentCost].Remove(currentHead);
                    // - put currentHead into visited
                    Debug.Assert(false == visited.ContainsKey(currentHead));
                    visited[currentHead] = currentCost;
                    // - process neighbor nodes as follows (i.e. add them to the fringe if applicable, update where necessary):
                    foreach (HexCell neighbor in neighbors)
                    {
                        // - if they don't exist (as keys) in paths:
                        if (!paths.ContainsKey(neighbor))
                        {
                            // |- create a path for them and store it in paths,
                            Path neighborPath = currentPath.Clone();
                            neighborPath.Add(neighbor);
                            paths[neighbor] = neighborPath;

                            // |- add them to the fringe in the appropriate bucket
                            //    (potential optimization: put them at the front of the queue,
                            //     i.e. prefer longer paths / depth first search)
                            int neighborCost = EstimateFullCost(neighborPath, goal);
                            AddToFringe(fringe, neighborCost, neighbor);
                        }
                        // - else (i.e. they do exist already):
                        else // true == paths.ContainsKey(neighbor)
                        {
                            // |- re-compute their old cost from their path-entry
                            int oldNeighborCost = EstimateFullCost(paths[neighbor], goal);
                            // |- estimate their new cost with the current path
                            int newNeighborCost = EstimateRestCost(neighbor, goal) + 1 + currentPath.Length;
                            // |- if their new cost is cheaper:
                            if (newNeighborCost < oldNeighborCost)
                            {
                                //  |- create a new path for them and update their entry in paths
                                Path neighborPath = currentPath.Clone();
                                neighborPath.Add(neighbor);
                                paths[neighbor] = neighborPath;

                                //  |- if they are in visited:
                                if (visited.ContainsKey(neighbor))
                                {
                                    //   |- remove them from visited
                                    visited.Remove(neighbor);
                                    //   |- re-add them in the fringe (in their new bucket)
                                    Debug.Assert(fringe.ContainsKey(newNeighborCost));
                                    Debug.Assert(false == fringe[newNeighborCost].Contains(neighbor));
                                    fringe[newNeighborCost].Add(neighbor);
                                //  |- else (i.e. they are NOT in visited):
                                }
                                else
                                {
                                    //   |- remove them from their current bucket
                                    Debug.Assert(fringe.ContainsKey(oldNeighborCost));
                                    Debug.Assert(fringe[oldNeighborCost].Contains(neighbor));
                                    fringe[oldNeighborCost].Remove(neighbor);
                                    //   |- re-add them to their new bucket
                                    AddToFringe(fringe, newNeighborCost, neighbor);

                                }
                            }
                        }
                    }
                    // - set currentHead to the "Min" element of the fringe,
                    //   i.e. any element in the first bucket.
                    bool fringeElementFound = false;
                    foreach (var bucket in fringe)
                    {
                        if (bucket.Value.Count > 0)
                        {
                            fringeElementFound = true;

                            // get first element from the bucket
                            var bucketEnumerator = bucket.Value.GetEnumerator();
                            bucketEnumerator.MoveNext();

                            currentHead = bucketEnumerator.Current;
                            break;
                        }
                    }
                    Debug.Assert(fringeElementFound);

                    // - update CurrentPath and currentCost
                    Debug.Assert(paths.ContainsKey(currentHead));
                    currentPath = paths[currentHead];
                    currentCost = EstimateFullCost(currentPath, goal);

                    debugCount++;
                }
            }
            if (debugCount >= 1000)
            {
                Debug.LogError("looks like infinite loop");
            }

            Debug.Assert(result != null);
            Debug.Assert(paths.ContainsKey(goal));
            int resultCost = EstimateFullCost(paths[goal], goal);
            Debug.Assert(fringe.ContainsKey(resultCost));
            Debug.Assert(fringe[resultCost].Contains(goal));
            Debug.Assert(!visited.ContainsKey(goal));

            return result;
        }

        void AddToFringe(SortedDictionary<int, HashSet<HexCell>> fringe, int cost, HexCell cell)
        {
            if (!fringe.ContainsKey(cost))
            {
                fringe[cost] = new HashSet<HexCell>();
            }
            fringe[cost].Add(cell);
        }

        
        int EstimateRestCost(HexCell start, HexCell end)
        {
            int result = HexCell.Difference(start, end);
            return result;
        }

        int EstimateFullCost(Path path, HexCell goal)
        {
            Debug.Assert(path != null);
            int result;
            result = path.Length + EstimateRestCost(path.End, goal);

            return result;
        }
    }

}

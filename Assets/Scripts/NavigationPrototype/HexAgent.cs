using UnityEngine;


namespace Navigation
{
    public class HexAgent : MonoBehaviour
    {
        [SerializeField] HexCell start;
        [SerializeField] HexCell goal;
        [SerializeField] Pathfinder pathfinder;

        void PlotPath()
        {
            Path path = pathfinder.FindPath(start, goal);

            foreach (var hexCell in path)
            {

            }
        }

    }

}

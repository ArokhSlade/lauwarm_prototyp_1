using UnityEngine;

namespace TicTacToe
{
    public class Field : MonoBehaviour
    {
        // TODO(Gerald, 2025 06 15):
        // automate /enforce synchronization between marked-state and current-marker
        // i.e. enforce single-source-of-truth / invariant
        [SerializeField] FieldState markedState = FieldState.Empty;
        [SerializeField] Vector2Int gridCoords;
        [SerializeField] Board board;
        [SerializeField] GameObject currentMarker = null;

        public Vector2Int Coords => gridCoords;

        void Start()
        {
            Debug.Assert(board != null);
        }

        public bool IsMarked()
        {
            bool result = markedState != FieldState.Empty;

            return result;
        }

        public void RequestMark()
        {
            board.RequestMark(gridCoords);
        }

        public void SubmitMark(FieldState mark)
        {
            if (markedState == mark)
            {
                return;
            }
            if (markedState != FieldState.Empty)
            {
                Destroy(currentMarker);
            } 
            markedState = mark;
            currentMarker = Markers.Instance.Create(mark, this.transform);
            return;

        }
    }
}
using UnityEngine;

namespace TicTacToe
{
    public class Field : MonoBehaviour
    {
        // TODO(Gerald, 2025 06 15):
        // synchronize marked-state with the existence of a corresponding child marker-object
        // i.e. enforce single-source-of-truth / invariant
        [SerializeField] FieldState markedState = FieldState.Empty;
        [SerializeField] Vector2Int gridCoords;
        [SerializeField] Board board;

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

        public void RequestMark(FieldState mark)
        {
            board.RequestMark(mark, gridCoords);
        }

        public void SubmitMark(FieldState markerType)
        {
            Markers.Instance.Create(markerType, this.transform);
        }
    }
}
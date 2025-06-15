using UnityEngine;

namespace TicTacToe
{
    public class Field : MonoBehaviour
    {
        // TODO(Gerald, 2025 06 15):
        // synchronize marked-state with the existence of a corresponding child marker-object
        // i.e. enforce single-source-of-truth / invariant
        FieldState markedState = FieldState.Empty;

        public bool IsMarked()
        {
            bool result = markedState != FieldState.Empty;

            return result;
        }
        public void Mark(FieldState state)
        {
            if (IsMarked())
            {
                return;
            }

            GameObject marker = null;
            FieldState markerType = FieldState.Empty;

            switch (state)
            {
                case FieldState.O:
                    markerType = FieldState.O;

                    break;
                case FieldState.X:
                    markerType = FieldState.X;
                    break;
                default:
                    break;
            }

            marker = Markers.Instance.Create(markerType, this.transform);
            markedState = state;
        }
    }
}
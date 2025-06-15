using System.Collections;
using UnityEngine;
using UnityEngine.Timeline;

namespace TicTacToe
{
    public class Field : MonoBehaviour
    {
        public void Mark(FieldState state)
        {
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
        }
    }
}
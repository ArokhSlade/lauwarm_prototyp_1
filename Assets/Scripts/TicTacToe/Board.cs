using System.Collections;
using UnityEngine;

namespace TicTacToe
{
    public enum FieldState
    {
        Empty, X, O
    }

    public class Board : MonoBehaviour
    {
        public FieldState[,] boardState = new FieldState[3,3];
        public Field[,] fields;
    }
}
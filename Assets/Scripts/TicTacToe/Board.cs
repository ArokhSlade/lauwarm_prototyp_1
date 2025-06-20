﻿#nullable disable

using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace TicTacToe
{
    public enum FieldState
    {
        Empty, X, O
    }

    /// <summary>
    /// Tic Tac Toe Board.
    /// Data Model of the game state.
    /// Think [Rows, Columns].
    /// </summary>
    public class Board : MonoBehaviour
    {
        [SerializeField] Game game;

        public UnityAction MarkSubmitted;

        FieldState[,] boardState = new FieldState[3, 3];

        //TODO(Gerald, 2025 06 15): generate fields automatically
        Field[,] fields;

        void Update()
        {

        }

        void Awake()
        {
            for (int row = 0; row < 3; ++row)
            {
                for (int col = 0; col < 3; ++col)
                {
                    boardState[row, col] = FieldState.Empty;
                }
            }
        }

        void OnEnable()
        {
            MarkSubmitted += game.OnMarkSubmitted;
        }

        void OnDisable()
        {

            MarkSubmitted -= game.OnMarkSubmitted;
        }

        void Start()
        {
            Field[] childFields = transform.GetComponentsInChildren<Field>();
            Debug.Assert(childFields.GetLength(0) == 3 * 3);
            fields = new Field[3, 3];
            foreach (Field field in childFields)
            {
                Vector2Int fieldCoords = field.Coords;
                fields[fieldCoords.x, fieldCoords.y] = field;
            }
        }

        public void SubmitMark(FieldState mark, Vector2Int coords)
        {
            boardState[coords.x, coords.y] = mark;
            Field field = GetField(coords);
            field.SubmitMark(mark);
        }

        public void OnMarkSubmitted()
        {
            MarkSubmitted(); // notify game
        }

        public void RequestMark(Vector2Int coords)
        {
            game.RequestMark(coords);
        }

        public bool ValidateCoords(Vector2Int coords)
        {
            RectInt bounds = new RectInt(0, 0, 3, 3);
            Debug.Assert(bounds.Contains(coords));
            return bounds.Contains(coords);
        }

        Field GetField(Vector2Int coords)
        {
            if (!ValidateCoords(coords))
            {
                return null;
            }

            Field result = fields[coords.x, coords.y];
            return result;
        }

        public bool IsFieldEmpty(Vector2Int coords)
        {
            bool result = boardState[coords.x, coords.y] == FieldState.Empty;
            return result;
        }

        public void UpdateModel(Vector2Int coords, FieldState state)
        {
            RectInt bounds = new RectInt(0, 0, 3, 3);
            Debug.Assert(bounds.Contains(coords));
            boardState[coords.x, coords.y] = state;
        }

        public FieldState DetermineWinner()
        {
            FieldState result = FieldState.Empty;

            FieldState rowResult = CheckRows();
            FieldState columnResult = CheckColumns();
            FieldState diagonalResult = CheckDiagonals();

            if (rowResult != FieldState.Empty)
            {
                return rowResult;
            }
            if (columnResult != FieldState.Empty)
            {
                return columnResult;
            }
            if (diagonalResult != FieldState.Empty)
            {
                return diagonalResult;
            }
            return result;
        }

        FieldState CheckRows()
        {
            FieldState winner = FieldState.Empty;
            for (int row = 0; row < 3; ++row)
            {
                // if this row has a winner, its mark will be in all slots, so also in the first slot.
                winner = boardState[row, 0];
                // if this slot is empty, there's no winner here, so try next row
                if (winner == FieldState.Empty)
                {
                    continue;
                }
                // otherwise, make sure the other slots in this row have the same mark
                for (int col = 1; col < 3; ++col)
                {
                    if (boardState[row, col] != winner)
                    {
                        winner = FieldState.Empty;
                        break;
                    }
                }
                // if winner "survived", we're done.
                if (winner != FieldState.Empty)
                {
                    return winner;
                }
            }

            return winner;
        }

        FieldState CheckColumns()
        {
            {
                FieldState winner = FieldState.Empty;
                for (int col = 0; col < 3; ++col)
                {
                    // if this col has a winner, its mark will be in all slots, so also in the first slot.
                    winner = boardState[0, col];
                    // if this slot is empty, there's no winner here, so try next col
                    if (winner == FieldState.Empty)
                    {
                        continue;
                    }
                    // otherwise, make sure the other slots in this col have the same mark
                    for (int row = 1; row < 3; ++row)
                    {
                        if (boardState[row, col] != winner)
                        {
                            winner = FieldState.Empty;
                            break;
                        }
                    }
                    // if winner "survived", we're done.
                    if (winner != FieldState.Empty)
                    {
                        return winner;
                    }
                }

                return winner;
            }
        }

        FieldState CheckDiagonals()
        {
            // first diagonal, start in one corner
            FieldState winner = boardState[0, 0];

            // unless corner is marked, skip to check other diagonal
            if (winner != FieldState.Empty)
            {
                // all elements on the diagonal must match
                for (int i = 1; i < 3; ++i)
                {
                    // if one doesn't reset and before trying other diagonal
                    if (boardState[i, i] != winner)
                    {
                        winner = FieldState.Empty;
                        break;
                    }
                }
                // if they do, we're done
                if (winner != FieldState.Empty)
                {
                    return winner;
                }
            }

            // try other corner.
            winner = boardState[0, 2];
            if (winner == FieldState.Empty)
            {
                return FieldState.Empty;
            }

            // mind transposed indices
            for (int row = 1, col = 1; row < 3; ++row, --col)
            {
                if (boardState[row, col] != winner)
                {
                    return FieldState.Empty;
                }
            }

            return winner;
        }
    }


}
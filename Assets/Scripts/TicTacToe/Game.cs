using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace TicTacToe
{

    public class Game : MonoBehaviour
    {

        enum GameState
        {
            PreGame, Turn, GameOver
        }

        [SerializeField] Board board;

        int playerCount = 2;
        [SerializeField] FieldState currentPlayer;
        GameState state = GameState.PreGame;

        UnityAction TurnFinished;

        void Start()
        {
            StartGame(FieldState.X);
        }

        void StartGame(FieldState firstPlayer)
        {
            currentPlayer = firstPlayer;
            state = GameState.Turn;
        }

        public void FinishTurn()
        {
            FieldState winner = board.DetermineWinner();
            if (winner != FieldState.Empty)
            {
                GameOver();
            }
            currentPlayer = NextPlayer();
        }

        FieldState NextPlayer()
        {
            FieldState result;
            if (currentPlayer == FieldState.X)
            {
                result = FieldState.O;
            } else
            {
                result = FieldState.X;
            }

            return result;
        }

        void GameOver()
        {
            bool isOver = board.DetermineWinner() != FieldState.Empty;
            if (isOver)
            {
                state = GameState.GameOver;
            }
        }

        public void RequestMark(Vector2Int coords)
        {
            if (!board.ValidateCoords(coords))
            {
                return;
            }
            ConsiderMark(currentPlayer, coords);
        }

        void ConsiderMark(FieldState currentPlayerMark, Vector2Int coords)
        {
            if (state != GameState.Turn)
            {
                return;
            }

            if (currentPlayerMark == FieldState.Empty)
            {
                return;
            }
            if (board.IsFieldEmpty(coords))
            {
                board.SubmitMark(currentPlayerMark, coords);
            }
        }


        public void OnMarkSubmitted()
        {
            FinishTurn();
        }
    }
}
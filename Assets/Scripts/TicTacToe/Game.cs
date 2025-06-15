using System.Collections;
using UnityEngine;

namespace TicTacToe
{

    public class Game : MonoBehaviour
    {

        enum GameState
        {
            PreGame, Turn, GameOver
        }

        int playerCount = 2;
        FieldState currentPlayer;
        [SerializeField] Board board;
        GameState state = GameState.PreGame;

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

        }
    }
}
using System.Collections;
using TMPro;
using UnityEngine;

namespace TicTacToe
{
    public class DebugPanel : MonoBehaviour
    {
        [SerializeField] TMPro.TMP_Text winnerLabel;
        [SerializeField] Board board;
        public void UpdateWinnerLabel()
        {
            FieldState winner = board.DetermineWinner();
            switch (winner)
            {
                case FieldState.X:
                    winnerLabel.text = "Winner = X";
                    winnerLabel.color = Color.blue;
                    break;
                case FieldState.O:
                    winnerLabel.text = "Winner = O";
                    winnerLabel.color = Color.red;
                    break;
                case FieldState.Empty:
                default:
                    winnerLabel.text = "Winner = Nobody";
                    winnerLabel.color = Color.yellow;
                    break;
            }
        }
    }
}
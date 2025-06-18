#nullable disable

using UnityEngine;

namespace TicTacTwo
{
    public class Game : MonoBehaviour
    {
        Board board;

        void Start()
        {
            board = GetComponentInChildren<Board>();

            if (board == null)
            {
                Debug.LogError("No child with Board component found!");
            }
        }
    }
}

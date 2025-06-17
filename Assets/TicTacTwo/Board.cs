using System.Collections.Generic;
using UnityEngine;

namespace TicTacTwo
{
    [RequireComponent(typeof(Grid))]
    public class Board : MonoBehaviour
    {
        public const int COLUMNS = 3;
        public const int ROWS = 3;

        Dictionary<Vector2Int, Field> fields;
        Grid grid;

        void Start()
        {
            fields = new Dictionary<Vector2Int, Field>();

            for (int x = 0; x < COLUMNS; x++)
            {
                for (int y = 0; y < ROWS; y++)
                {
                    Vector2Int coordinate = new Vector2Int(x, y);
                    fields[coordinate] = new Field();
                }
            }

            grid = GetComponent<Grid>();
        }

        void Update()
        {
            // Render fields

        }

        public Field GetFieldAtCellCoordinate(Vector2Int coordinate)
        {
            if (coordinate.x > COLUMNS || coordinate.y > ROWS)
            {
                Debug.Log($"Trying to access field at column {coordinate.x}, row {coordinate.y}, but board only has {COLUMNS} columns and {ROWS} rows!");
            }

            return fields[coordinate];
        }

        public void SetFieldAtCellCoordinate(Vector2Int coordinate, FieldState state)
        {
            Field field = GetFieldAtCellCoordinate(coordinate);

            field.state = state;
        }

        public Field GetFieldAtWorldCoordinate(Vector3 worldCoordinate)
        {
            Vector3Int cellCoordinate = grid.WorldToCell(worldCoordinate);

            return GetFieldAtCellCoordinate(new Vector2Int(cellCoordinate.x, cellCoordinate.z));
        }

        public void SetFieldAtWorldCoordinate(Vector3 worldCoordinate, FieldState state)
        {
            Vector3Int cellCoordinate = grid.WorldToCell(worldCoordinate);

            SetFieldAtCellCoordinate(new Vector2Int(cellCoordinate.x, cellCoordinate.z), state);
        }
    }
}

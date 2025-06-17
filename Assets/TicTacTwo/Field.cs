namespace TicTacTwo
{
    public enum FieldState
    {
        Empty = 0,
        Player1 = 10,
        Player2 = 20,
    }

    public struct Field
    {
        public FieldState state;

        public void State()
        {
            state = FieldState.Empty;
        }
    }
}
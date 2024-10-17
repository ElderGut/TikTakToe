namespace tictactou.Models
{
    public class Game
    {


        public char[][] Board { get; set; }
        public char CurrentPlayer { get; set; }
        public char? Winner { get; set; }
        public bool IsDraw { get; set; }

        public Game()
        {
            Board = new char[][]
            {
                new char[] { '-', '-', '-' },
                new char[] { '-', '-', '-' },
                new char[] { '-', '-', '-' }
            };
            CurrentPlayer = 'X';
            Winner = null;
            IsDraw = false;
        }

    }
}

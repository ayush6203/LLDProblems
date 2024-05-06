namespace LLDProblems.StandardProblems.SnakeLadder
{
    public class SnakeLadderGame
    {
        public Queue<Player> Players { get; private set; }
        public GameBoard Board { get; private set; }

        public SnakeLadderGame(IList<Player> players)
        {
            Players = new Queue<Player>();
            Board = new GameBoard();

            foreach (Player player in players)
                Players.Enqueue(player);

            Board.InitBoard();
        }

        public bool PlayMove(Player player)
        {
            return true;
        }
    }

    public class GameBoard
    {
        public Cell[][] Cells { get; private set; }
        private Dice Dice { get; set; }
        public void InitBoard()
        {
            Cells = new Cell[10][];
            for (int i = 0; i < 10; i++)
                Cells[i] = new Cell[10];

            Snake snake1 = new Snake(15, 3);
            Snake snake2 = new Snake(25, 13);
            Snake snake3 = new Snake(52, 39);
            Snake snake4 = new Snake(93, 17);


            Ladder ladder1 = new Ladder(14, 36);
            Ladder ladder2 = new Ladder(7, 33);
            Ladder ladder3 = new Ladder(22, 44);
            Ladder ladder4 = new Ladder(3, 49);
            Ladder ladder5 = new Ladder(59, 72);

            this.Dice = new Dice();
        }

        public bool Move()
        {
            return true;
        }
    }

    public class Cell
    {
        public int CellNumber { get; private set; }
        public Piece Piece { get; private set; }
        public Linker Linker { get; private set; }  
    }

    public class Piece
    {
        public Color Color { get; private set; }
    }

    public class Player
    {
        public Account Account { get; set; }
        public Color PieceColor { get; set; }
        public int Position { get; set; }
    }

    public class Account
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
    }

    public enum Color
    {
        Red,
        Green,
        Blue,
        Yellow
    }

    public class Snake : Linker
    {
        public Snake(int source, int destination) : base(LinkerType.Snake, source, destination)
        {

        }
    }

    public class Ladder : Linker
    {
        public Ladder(int source, int destination) : base(LinkerType.Snake, source, destination)
        {

        }
    }

    public  class Linker
    {
        public Linker(LinkerType type, int source, int destination)
        {
            Type = type;
            Source = source;
            Destination = destination;
        }

        public LinkerType Type { get; private set; }
        public int Source { get; private set; }
        public int Destination { get; private set; }
    }

    public enum LinkerType
    {
        Snake,
        Ladder
    }

    public class Dice
    {
        public int RollDice()
        {
            Random random = new Random();
            return random.Next(1, 7);
        }
    }
}

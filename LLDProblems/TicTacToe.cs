namespace LLDProblems
{
    public class TicTacToe
    {
        Queue<Player> players;
        Board board;

        public TicTacToe()
        {
            InitGame();
        }

        private void InitGame()
        {
            players = new Queue<Player>();
            board = new Board(3);

            var player1 = new Player("Ayush", new PieceX());
            var player2 = new Player("Piyush", new PieceO());
            players.Enqueue(player1);
            players.Enqueue(player2);
        }

        public void StartGame()
        {
            PrintBoard();
            while (true)
            {
                if (board.IsBoardFull())
                {
                    Console.WriteLine("Game is Draw");
                    return;
                }
                var currPlayer = players.Peek();
                PrintTurnMessage(currPlayer);
                string cordinates = Console.ReadLine();
                var tokenised = cordinates.Split(',');
                int x = int.Parse(tokenised[0]);
                int y = int.Parse(tokenised[1]);
                if(x < 0 || y <0 || x >= board.BoardSize || y >= board.BoardSize)
                {
                    Console.WriteLine($"Please enter piece within the range {0} to {board.BoardSize}");
                    continue;
                }

                if (!board.AddPieceToTheBoard(x, y, currPlayer.Piece))
                {
                    Console.WriteLine($"Please enter piece at blank cell");
                    continue;
                }

                players.Enqueue(players.Dequeue());
                PrintBoard();
            }
        }

        private void PrintTurnMessage(Player player)
        {
            Console.WriteLine($"it's player ${player.Name} turn, Please place a piece into the board");
        }

        private void PrintBoard()
        {
            for(int i = 0; i < board.BoardSize; i++)
            {
                for(int j = 0; j < board.BoardSize; j++)
                {
                    if (board.Playingboard[i][j] is null)
                        Console.Write("_ ");
                    else
                        Console.Write(board.Playingboard[i][j].PieceType + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }

    class Player
    {
        public string Name;
        public Piece Piece;

        public Player(string name, Piece piece)
        {
            Name = name;
            Piece = piece;
        }
    }

    class Board
    {
        public int BoardSize;
        public Piece[][] Playingboard;

        public Board(int boardSize)
        {
            BoardSize = boardSize;
            Playingboard = new Piece[boardSize][];
            for (int i = 0; i < boardSize; i++)
                Playingboard[i] = new Piece[boardSize];
        }

        public bool AddPieceToTheBoard(int x, int y, Piece piece)
        {
            if (Playingboard[x][y] is not null)
                return false;

            Playingboard[x][y] = piece;
            return true;
        }

        public bool IsBoardFull()
        {
            for(int i = 0; i < BoardSize; i++)
            {
                for(int j = 0; j < BoardSize; j++)
                {
                    if (Playingboard[i][j] is null)
                        return false;
                }
            }

            return true;
        }

        public bool IsPlayerWonMatch(Piece piece)
        {
            bool isWinner = false;
            //Check both diagonals
            //Check all vertical
            //check all horizontals

            return isWinner;
        }
    }

    class PieceO : Piece
    {
        public PieceO() : base(PieceType.O)
        {

        }
    }

    class PieceX : Piece
    {
        public PieceX() : base(PieceType.X)
        {

        }
    }

    class Piece
    {
        public PieceType PieceType { get; set; }
        public Piece(PieceType pieceType)
        {
            PieceType = pieceType;
        }
    }

    enum PieceType
    {
        X,
        O
    }
}

namespace LLDProblems.StandardProblems.Chess
{
    // Enum for Piece Colors
    public enum Color { White, Black }

    // Class representing a chess game
    public class ChessGame
    {
        public ChessBoard Board { get; private set; }
        public Player[] Players { get; private set; }
        public int CurrentPlayerIndex { get; private set; }
        public bool IsGameOver { get; private set; }

        public ChessGame()
        {
            Board = new ChessBoard();
            Players = new Player[2];
            Players[0] = new Player(Color.White);
            Players[1] = new Player(Color.Black);
            CurrentPlayerIndex = 0; // White starts
            IsGameOver = false;
        }

        public void InitializeGame()
        {
            Board.SetupBoard();
        }

        public void SwitchPlayer()
        {
            CurrentPlayerIndex = 1 - CurrentPlayerIndex;
        }

        public void MakeMove(Move move)
        {
            if (Board.MovePiece(move.From, move.To, Players[CurrentPlayerIndex]))
            {
                SwitchPlayer();
            }
        }

        //This is not needed as i would be creating it API based, API will call make move.
        public void StartGame()
        {
            InitializeGame();
            while (!IsGameOver)
            {
                Console.WriteLine("Current board:");
                Board.PrintBoard();

                Console.WriteLine($"{Players[CurrentPlayerIndex].Color}'s move");
                // This could be more complex with real user input or AI integration
                // Here it is just a simulation with one move choice (you can extend this part)
                MakeMove(new Move(new Cell(6, 0), new Cell(5, 0))); // Example move (Pawn)

                // You would include conditions to check for check, checkmate, and stalemate here
                IsGameOver = true; // Simplified to just stop the game
            }
        }
    }

    // Class representing the chess board
    public class ChessBoard
    {
        public Cell[,] Cells { get; private set; }

        public ChessBoard()
        {
            Cells = new Cell[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Cells[i, j] = new Cell(i, j);
                }
            }
        }

        public void SetupBoard()
        {
            // Simplified setup: only pawns and kings for demonstration
            for (int i = 0; i < 8; i++)
            {
                Cells[1, i].Piece = new Pawn(Color.Black, new List<IMoveStrategy>() { new StraightMoveStrategy() });
                Cells[6, i].Piece = new Pawn(Color.White, new List<IMoveStrategy>() { new StraightMoveStrategy() });
            }
            Cells[0, 4].Piece = new King(Color.Black, new List<IMoveStrategy>() { new StraightMoveStrategy() });
            Cells[7, 4].Piece = new King(Color.White, new List<IMoveStrategy>() { new StraightMoveStrategy() });
        }

        public bool MovePiece(Cell from, Cell to, Player player)
        {
            //Validate if from Cell is in the board has piece, if yes of players color
            //Validate if to Cell is in the board and is blank and does not have player's color piece.

            Piece currPiece = from.Piece;
            IList<IMoveStrategy> possibleMoves = currPiece.PossibleMoves;
            bool isPieceMovable = false;
            foreach(var pMove in possibleMoves)
            {
                if (pMove.CanMove(from, to, this))
                {
                    isPieceMovable = true;
                    break;
                }
            }

            if (isPieceMovable)
            {
                // If there is any piece existing? move it to the knock out stack, i.e that piece is not more in the game.
                to.Piece = from.Piece;
            }

            return isPieceMovable;
        }

        public void PrintBoard()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Console.Write(Cells[i, j].Piece == null ? ". " : Cells[i, j].Piece.GetSymbol() + " ");
                }
                Console.WriteLine();
            }
        }
    }

    // Class representing a Cell on the board
    public class Cell
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public Piece Piece { get; set; }

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
            Piece = null;
        }
    }

    // Abstract class for chess pieces
    public abstract class Piece
    {
        public Color Color { get; private set; }
        public IList<IMoveStrategy> PossibleMoves { get; set; }

        protected Piece(Color color, IList<IMoveStrategy> possibleMoves)
        {
            Color = color;
            PossibleMoves = possibleMoves;
        }

        public abstract string GetSymbol();
    }

    // Specific piece classes
    public class Pawn : Piece
    {
        public Pawn(Color color, IList<IMoveStrategy> possibleMoves) : base(color, possibleMoves) { }

        public override string GetSymbol()
        {
            return Color == Color.White ? "P" : "p";
        }
    }

    public class King : Piece
    {
        public King(Color color, IList<IMoveStrategy> possibleMoves) : base(color, possibleMoves) { }

        public override string GetSymbol()
        {
            return Color == Color.White ? "K" : "k";
        }
    }

    // Class representing a player
    public class Player
    {
        public Color Color { get; private set; }
        public Player(Color color)
        {
            Color = color;
        }
    }

    // Class representing a move
    public class Move
    {
        public Cell From { get; private set; }
        public Cell To { get; private set; }

        public Move(Cell from, Cell to)
        {
            From = from;
            To = to;
        }
    }

    
    // Move strategy
    public interface IMoveStrategy
    {
        public bool CanMove(Cell cellSource, Cell cellDest, ChessBoard board);
    }

    //Straight Move Strategy
    public class StraightMoveStrategy : IMoveStrategy
    {

        public bool CanMove(Cell cellSource, Cell cellDest, ChessBoard board)
        {
            throw new NotImplementedException();
        }
    }

}



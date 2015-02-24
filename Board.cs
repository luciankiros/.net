using System.Collections.Generic;

namespace Chess
{
    internal class Board
    {
        private const int WHITE_PAWN_INITIAL_RANK = 1;
        private const int BLACK_PAWN_INITIAL_RANK = 6;

        private readonly SquareInfo[,] _board = new SquareInfo[8, 8];
        private readonly Dictionary<Piece, RuleManager> _ruleManagers;

        public static Board CreateBoard()
        {
            Board board = new Board();
            board.SetUp();
            return board;
        }

        private void SetUp()
        {
            for (int i = 0; i < 8; i++)
            {
                _board[i, WHITE_PAWN_INITIAL_RANK] = new SquareInfo { Piece = Piece.Pawn, PieceColor = PieceColor.White };
                _board[i, BLACK_PAWN_INITIAL_RANK] = new SquareInfo { Piece = Piece.Pawn, PieceColor = PieceColor.Black };
            }
        }

        private Board()
        {
            _ruleManagers = new Dictionary<Piece, RuleManager>()
            {
                {Piece.Pawn, new PawnRuleManager(_board)},
                {Piece.Knight, new KnightRuleManager(_board)}
            };
        }

        public void Play(string move)
        {
            int originFile = move[0] - 'A';
            int originRank = move[1] - '1';
            int targetFile = move[2] - 'A';
            int targetRank = move[3] - '1';

            Piece movingPiece = _board[originFile, originRank].Piece;
            var ruleManager = _ruleManagers[movingPiece];

            ruleManager.ApplyMove(originFile, originRank, targetFile, targetRank);
        }



        public SquareInfo GetSquareInfo(string position)
        {
            int file = position[0] - 'A';
            int rank = position[1] - '1';
            return _board[file, rank];
        }

        public void PlacePiece(Piece piece, PieceColor pieceColor, string position)
        {
            int file = position[0] - 'A';
            int rank = position[1] - '1';
            _board[file, rank].Piece = piece;
            _board[file, rank].PieceColor = pieceColor;
        }
    }
}
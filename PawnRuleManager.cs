using System;

namespace Chess
{
    internal class PawnRuleManager
    {
        private readonly SquareInfo[,] _board;

        public PawnRuleManager(SquareInfo[,] board)
        {
            _board = board;
        }


        private bool IsLegalMove(int originFile, int originRank, int targetFile, int targetRank)
        {
            int direction = (int)_board[originFile, originRank].PieceColor;
            int deltaRank = direction * Math.Abs(targetRank - originRank);
            int deltaFile = Math.Abs(targetFile - originFile);

            bool canMove2Squares = originRank == 1 || originRank == 6;
            bool isLegalMove = deltaFile == 0 &&
                               ((Math.Abs(deltaRank) == 2 && canMove2Squares) ||
                                (Math.Abs(deltaRank) == 1 && originRank + deltaRank == targetRank));

            bool canCaptureOnDiagonal = deltaFile == 1 && Math.Abs(deltaRank) == 1 &&
                                        originRank + deltaRank == targetRank;

            return isLegalMove || canCaptureOnDiagonal;
        }
        internal void ApplyMove(int originFile, int originRank, int targetFile, int targetRank)
        {

            if (IsLegalMove(originFile, originRank, targetFile, targetRank))
            {
                Move(originFile, originRank, targetFile, targetRank);
            }
          
        }

        private void Move(int originFile, int originRank, int targetFile, int targetRank)
        {
            var originalSquare = _board[originFile, originRank];

            _board[originFile, originRank].Piece = Piece.Empty;
            _board[originFile, originRank].PieceColor = PieceColor.None;

            _board[targetFile, targetRank].Piece = originalSquare.Piece;
            _board[targetFile, targetRank].PieceColor = originalSquare.PieceColor;
        }
    }
}
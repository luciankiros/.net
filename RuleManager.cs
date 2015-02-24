namespace Chess
{
    internal abstract class RuleManager
    {
        protected readonly SquareInfo[,] _board;

        protected RuleManager(SquareInfo[,] board)
        {
            _board = board;
        }

        internal void ApplyMove(int originFile, int originRank, int targetFile, int targetRank)
        {

            if (IsLegalMove(originFile, originRank, targetFile, targetRank))
            {
                Move(originFile, originRank, targetFile, targetRank);
            }

        }

        protected abstract bool IsLegalMove(int originFile, int originRank, int targetFile, int targetRank);

        private void Move(int originFile, int originRank, int targetFile, int targetRank)
        {
            var originalSquare = _board[originFile, originRank];

            _board[originFile, originRank].Piece = Piece.None;
            _board[originFile, originRank].PieceColor = PieceColor.None;

            _board[targetFile, targetRank].Piece = originalSquare.Piece;
            _board[targetFile, targetRank].PieceColor = originalSquare.PieceColor;
        }
    }
}
using System;

namespace Chess
{
    internal class PawnRuleManager : RuleManager
    {

        public PawnRuleManager(SquareInfo[,] board)
            : base(board)
        {
        }


        protected override bool IsLegalMove(int originFile, int originRank, int targetFile, int targetRank)
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

    }
}
using System;

namespace Chess
{
    internal class KnightRuleManager : RuleManager
    {
        public KnightRuleManager(SquareInfo[,] board) : base(board)
        {
        }

        protected override bool IsLegalMove(int originFile, int originRank, int targetFile, int targetRank)
        {
            int deltaRank = Math.Abs(targetRank - originRank);
            int deltaFile = Math.Abs(targetFile - originFile);

            bool isLegalMove = Math.Abs(deltaRank - deltaFile) == 1 && deltaFile + deltaRank == 3;
            return isLegalMove;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Chess
{
    [TestFixture]
    class ChessTests
    {

        [Category("Initialization")]
        [TestCase("E5")]
        [TestCase("A6")]
        [TestCase("H6")]
        [TestCase("A3")]
        [TestCase("H3")]
        public void BoardInitialization_NoMoves_PositionEmptyAt(string position)
        {
            Board board = new Board();
            SquareInfo emptySquare = board.GetSquareInfo(position);
            Assert.AreEqual(emptySquare.Piece, Piece.Empty);
        }

        [Category("Initialization")]
        [TestCase("A2")]
        [TestCase("B2")]
        [TestCase("C2")]
        [TestCase("D2")]
        [TestCase("E2")]
        [TestCase("F2")]
        [TestCase("G2")]
        [TestCase("H2")]
        [TestCase("A7")]
        [TestCase("B7")]
        [TestCase("C7")]
        [TestCase("D7")]
        [TestCase("E7")]
        [TestCase("F7")]
        [TestCase("G7")]
        [TestCase("H7")]
        public void BoardInitialization_NoMoves_PawnFoundAt(string position)
        {
            Board board = new Board();

            SquareInfo emptySquare = board.GetSquareInfo(position);
            Assert.AreEqual(emptySquare.Piece, Piece.Pawn);
        }

        [Category("Pawn Rule")]
        [Test]
        public void Play_PawnA2A3_PawnFoundAtA3NotFoundAtA2()
        {
            Board board = new Board();
            board.Play(Piece.Pawn, "A2A3");
            SquareInfo a3 = board.GetSquareInfo("A3");
            SquareInfo a2 = board.GetSquareInfo("A2");

            Assert.AreEqual(Piece.Pawn, a3.Piece);
            Assert.AreEqual(Piece.Empty, a2.Piece);
        }


        [Category("Pawn Rule")]
        [Test]
        public void Play_PawnA2A4_PawnFoundAtA4NotFoundAtA2()
        {
            Board board = new Board();
            board.Play(Piece.Pawn, "A2A4");
            SquareInfo a4 = board.GetSquareInfo("A4");
            SquareInfo a2 = board.GetSquareInfo("A2");

            Assert.AreEqual(Piece.Pawn, a4.Piece);
            Assert.AreEqual(Piece.Empty, a2.Piece);
        } 
        
        [Category("Pawn Rule")]
        [Test]
        public void Play_PawnA4A2_PawnRemainsAtA4()
        {
            Board board = new Board();
            board.Play(Piece.Pawn, "A4A2");
            SquareInfo a4 = board.GetSquareInfo("A4");

            Assert.AreEqual(Piece.Pawn, a4.Piece);
        }

        [Category("Pawn Rule")]
        [Test]
        public void Play_Pawn3Squares_MovePawnBackToItsOriginalPosition()
        {

            Board board = new Board();
            board.Play(Piece.Pawn, "A2A5");
            SquareInfo a2 = board.GetSquareInfo("A2");

            Assert.AreEqual(Piece.Pawn, a2.Piece);
        }

        [Category("Pawn Rule")]
        [Test]
        public void Play_PawnMovedToAdiacentFile_DestinationSquareRemainsEmpty()
        {

            Board board = new Board();
            board.Play(Piece.Pawn, "A2B4");
            SquareInfo b4 = board.GetSquareInfo("B4");

            Assert.AreEqual(Piece.Empty, b4.Piece);
        }

        [Test]
        public void Play_PawnMovedToAdiacentFile_PawnRemainsOnItsOriginalPosition()
        {

            Board board = new Board();
            board.Play(Piece.Pawn, "A2B4");
            SquareInfo a2 = board.GetSquareInfo("A2");

            Assert.AreEqual(Piece.Pawn, a2.Piece);
        }



        [Category("Pawn Rule")]
        [TestCase("A2","B3")]
        [TestCase("C2","B3")]
        [TestCase("D4","E3")]
        [TestCase("D4","C3")]
        public void Play_PawnCapturesPieceDiagonaly_OponentPieceIsReplacedByPawn(string origin, string target)
        {

            Board board = new Board();
            board.PlacePiece(Piece.Pawn, target);
            board.Play(Piece.Pawn, origin + target);
            SquareInfo originSquare = board.GetSquareInfo(origin);
            SquareInfo targetSquare = board.GetSquareInfo(target);


            Assert.AreEqual(Piece.Empty, originSquare.Piece);
            Assert.AreEqual(Piece.Pawn, targetSquare.Piece);
        }
    }

    internal struct SquareInfo
    {
        public Piece Piece { get; set; }
    }

    internal enum Piece
    {
        Empty,
        Pawn
    }

    internal class Board
    {
        private readonly SquareInfo[,] _board = new SquareInfo[8, 8];

        public Board()
        {
            for (int i = 0; i < 8; i++)
            {
                _board[i, 1] = new SquareInfo { Piece = Piece.Pawn };
                _board[i, 6] = new SquareInfo { Piece = Piece.Pawn };
            }
        }
        public void Play(Piece piece, string move)
        {
            int originFile = move[0] - 'A';
            int originRank = move[1] - '1';
            int targetFile = move[2] - 'A';
            int targetRank = move[3] - '1';

            if (_board[targetFile, targetRank].Piece == Piece.Empty)
            {


                if (targetRank - originRank > 0 && targetRank - originRank > 2)
                {
                    return;
                }

                if (originFile != targetFile)
                {
                    return;
                }
            }

            _board[originFile, originRank].Piece = Piece.Empty;
            _board[targetFile, targetRank].Piece = Piece.Pawn;
        }

        public SquareInfo GetSquareInfo(string position)
        {
            int file = position[0] - 'A';
            int rank = position[1] - '1';
            return _board[file, rank];
        }

        public void PlacePiece(Piece piece, string position)
        {
            int file = position[0] - 'A';
            int rank = position[1] - '1';
            _board[file, rank].Piece = piece;
        }
    }
}

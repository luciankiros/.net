using System;
using System.Collections.Generic;
using System.Dynamic;
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
        [TestCase("A7")]
        [TestCase("B7")]
        [TestCase("C7")]
        [TestCase("D7")]
        [TestCase("E7")]
        [TestCase("F7")]
        [TestCase("G7")]
        [TestCase("H7")]
        public void BoardInitialization_NoMoves_BlackPawnAt(string position)
        {
            Board board = new Board();
            SquareInfo blackPawn = board.GetSquareInfo(position);
            Assert.AreEqual(PieceColor.Black, blackPawn.PieceColor);
        }

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
            board.Play("A2A3");
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
            board.Play("A2A4");
            SquareInfo a4 = board.GetSquareInfo("A4");
            SquareInfo a2 = board.GetSquareInfo("A2");

            Assert.AreEqual(Piece.Pawn, a4.Piece);
            Assert.AreEqual(Piece.Empty, a2.Piece);
        }



        [Category("Pawn Rule")]
        [Test]
        public void Play_PawnA4A2_PawnRemainsAtA4AndA2RemainsEmpty()
        {
            Board board = new Board();
            board.PlacePiece(Piece.Pawn, PieceColor.White, "A4");
            board.PlacePiece(Piece.Empty, PieceColor.None, "A2");
            board.Play("A4A2");
            SquareInfo a4 = board.GetSquareInfo("A4");
            SquareInfo a2 = board.GetSquareInfo("A2");

            Assert.AreEqual(Piece.Pawn, a4.Piece);
            Assert.AreEqual(Piece.Empty, a2.Piece);
        }

        [Category("Pawn Rule")]
        [Test]
        public void Play_PawnA2A5_PawnRemainsAtA2()
        {

            Board board = new Board();
            board.Play("A2A5");
            SquareInfo a2 = board.GetSquareInfo("A2");

            Assert.AreEqual(Piece.Pawn, a2.Piece);
        }



        [Test]
        public void Play_WhitePawnA2B4_WhitePawnRemainsAtA2()
        {

            Board board = new Board();
            board.Play("A2B4");
            SquareInfo a2 = board.GetSquareInfo("A2");

            Assert.AreEqual(Piece.Pawn, a2.Piece);
            Assert.AreEqual(PieceColor.White, a2.PieceColor);
        }



        [Category("Pawn Rule")]
        [Test]
        public void Play_WhitePawnCapturesA2B3_WhitePawnAtB3()
        {

            Board board = new Board();
            board.PlacePiece(Piece.Pawn, PieceColor.Black, "B3");
            board.Play("A2B3");
            SquareInfo b3 = board.GetSquareInfo("B3");

            Assert.AreEqual(Piece.Pawn, b3.Piece);
            Assert.AreEqual(PieceColor.White, b3.PieceColor);
        }

        [Category("Pawn Rule")]
        [Test]
        public void Play_WhitePawnCapturesA3B2_NotAllowedWhitePawnRemainsAtA3()
        {

            Board board = new Board();
            board.PlacePiece(Piece.Pawn, PieceColor.White, "A3");
            board.PlacePiece(Piece.Pawn, PieceColor.Black, "B2");
            board.Play("A3B2");
            SquareInfo a3 = board.GetSquareInfo("A3");
            SquareInfo b2 = board.GetSquareInfo("B2");

            Assert.AreEqual(Piece.Pawn, a3.Piece);
            Assert.AreEqual(PieceColor.White, a3.PieceColor);

            Assert.AreEqual(Piece.Pawn, b2.Piece);
            Assert.AreEqual(PieceColor.Black, b2.PieceColor);
        }

        [Category("Pawn Rule")]
        [Test]
        public void Play_BlackPawnCapturesA3B4_NotAllowedBlackPawnRemainsAtA3()
        {

            Board board = new Board();
            board.PlacePiece(Piece.Pawn, PieceColor.Black, "A3");
            board.PlacePiece(Piece.Pawn, PieceColor.White, "B4");
            board.Play("A3B4");
            SquareInfo a3 = board.GetSquareInfo("A3");
            SquareInfo b4 = board.GetSquareInfo("B4");

            Assert.AreEqual(Piece.Pawn, a3.Piece);
            Assert.AreEqual(PieceColor.Black, a3.PieceColor);

            Assert.AreEqual(Piece.Pawn, b4.Piece);
            Assert.AreEqual(PieceColor.White, b4.PieceColor);
        }

        [Category("Pawn Rule")]
        [Test]
        public void Play_BlackPawnCapturesA5C3_NotAllowedBlackPawnRemainsAtA3()
        {

            Board board = new Board();
            board.PlacePiece(Piece.Pawn, PieceColor.Black, "A5");
            board.PlacePiece(Piece.Pawn, PieceColor.White, "C3");
            board.Play("A5C3");
            SquareInfo a5 = board.GetSquareInfo("A5");
            SquareInfo c3 = board.GetSquareInfo("C3");

            Assert.AreEqual(Piece.Pawn, a5.Piece);
            Assert.AreEqual(PieceColor.Black, a5.PieceColor);

            Assert.AreEqual(Piece.Pawn, c3.Piece);
            Assert.AreEqual(PieceColor.White, c3.PieceColor);
        }



        [Category("Pawn Rule")]
        [Test]
        public void Play_BlackPawnCapturesA5B4_BlackPawnAtB4()
        {

            Board board = new Board();
            board.PlacePiece(Piece.Pawn, PieceColor.White, "B4");
            board.PlacePiece(Piece.Pawn, PieceColor.Black, "A5");
            board.Play("A5B4");
            SquareInfo b4 = board.GetSquareInfo("B4");

            Assert.AreEqual(Piece.Pawn, b4.Piece);
            Assert.AreEqual(PieceColor.Black, b4.PieceColor);
        }
    }

    internal struct SquareInfo
    {
        public Piece Piece { get; set; }
        public PieceColor PieceColor { get; set; }
    }

    internal enum PieceColor
    {
        White = 1,
        Black = -1,
        None = 0
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
                _board[i, 1] = new SquareInfo { Piece = Piece.Pawn, PieceColor = PieceColor.White };
                _board[i, 6] = new SquareInfo { Piece = Piece.Pawn, PieceColor = PieceColor.Black };
            }
        }

        public void Play(string move)
        {
            int originFile = move[0] - 'A';
            int originRank = move[1] - '1';
            int targetFile = move[2] - 'A';
            int targetRank = move[3] - '1';

            if (IsLegalMove(targetFile, targetRank, originRank, originFile))
            {
                var originalSquare = _board[originFile, originRank];

                _board[originFile, originRank].Piece = Piece.Empty;
                _board[originFile, originRank].PieceColor = PieceColor.None;

                _board[targetFile, targetRank].Piece = originalSquare.Piece;
                _board[targetFile, targetRank].PieceColor = originalSquare.PieceColor;

            }
        }

        private bool IsLegalMove(int targetFile, int targetRank, int originRank, int originFile)
        {
            int direction = (int)_board[originFile, originRank].PieceColor;
            int deltaRank = direction * Math.Abs(targetRank - originRank);
            int deltaFile = Math.Abs(targetFile - originFile);

            bool canMove2Squares = originRank == 1 || originRank == 6;
            bool isLegalMove = deltaFile == 0 &&
                               ((Math.Abs(deltaRank) == 2 && canMove2Squares) ||
                               (Math.Abs(deltaRank) == 1 && originRank + deltaRank == targetRank));

            bool canCaptureOnDiagonal = deltaFile == 1 && Math.Abs(deltaRank) == 1 && originRank + deltaRank == targetRank;

            return isLegalMove || canCaptureOnDiagonal;
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

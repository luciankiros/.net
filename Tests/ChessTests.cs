using NUnit.Framework;

namespace Chess.Tests
{
    [TestFixture]
    public class ChessTests
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
            Board board = Board.CreateBoard();
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
            Board board = Board.CreateBoard();
            SquareInfo emptySquare = board.GetSquareInfo(position);
            Assert.AreEqual(emptySquare.Piece, Piece.None);
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
            Board board = Board.CreateBoard();

            SquareInfo emptySquare = board.GetSquareInfo(position);
            Assert.AreEqual(emptySquare.Piece, Piece.Pawn);
        }

        [Category("Pawn Rule")]
        [Test]
        public void Play_PawnA2A3_PawnFoundAtA3NotFoundAtA2()
        {
            Board board = Board.CreateBoard();
            board.Play("A2A3");
            SquareInfo a3 = board.GetSquareInfo("A3");
            SquareInfo a2 = board.GetSquareInfo("A2");

            Assert.AreEqual(Piece.Pawn, a3.Piece);
            Assert.AreEqual(Piece.None, a2.Piece);
        }


        [Category("Pawn Rule")]
        [Test]
        public void Play_PawnA2A4_PawnFoundAtA4NotFoundAtA2()
        {
            Board board = Board.CreateBoard();
            board.Play("A2A4");
            SquareInfo a4 = board.GetSquareInfo("A4");
            SquareInfo a2 = board.GetSquareInfo("A2");

            Assert.AreEqual(Piece.Pawn, a4.Piece);
            Assert.AreEqual(Piece.None, a2.Piece);
        }



        [Category("Pawn Rule")]
        [Test]
        public void Play_PawnA4A2_PawnRemainsAtA4AndA2RemainsEmpty()
        {
            Board board = Board.CreateBoard();
            board.PlacePiece(Piece.Pawn, PieceColor.White, "A4");
            board.PlacePiece(Piece.None, PieceColor.None, "A2");
            board.Play("A4A2");
            SquareInfo a4 = board.GetSquareInfo("A4");
            SquareInfo a2 = board.GetSquareInfo("A2");

            Assert.AreEqual(Piece.Pawn, a4.Piece);
            Assert.AreEqual(Piece.None, a2.Piece);
        }

        [Category("Pawn Rule")]
        [Test]
        public void Play_PawnA2A5_PawnRemainsAtA2()
        {

            Board board = Board.CreateBoard();
            board.Play("A2A5");
            SquareInfo a2 = board.GetSquareInfo("A2");

            Assert.AreEqual(Piece.Pawn, a2.Piece);
        }



        [Category("Pawn Rule")]
        [Test]
        public void Play_WhitePawnA2B4_WhitePawnRemainsAtA2()
        {

            Board board = Board.CreateBoard();
            board.Play("A2B4");
            SquareInfo a2 = board.GetSquareInfo("A2");

            Assert.AreEqual(Piece.Pawn, a2.Piece);
            Assert.AreEqual(PieceColor.White, a2.PieceColor);
        }



        [Category("Pawn Rule")]
        [Test]
        public void Play_WhitePawnCapturesA2B3_WhitePawnAtB3()
        {

            Board board = Board.CreateBoard();
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

            Board board = Board.CreateBoard();
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

            Board board = Board.CreateBoard();
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

            Board board = Board.CreateBoard();
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

            Board board = Board.CreateBoard();
            board.PlacePiece(Piece.Pawn, PieceColor.White, "B4");
            board.PlacePiece(Piece.Pawn, PieceColor.Black, "A5");
            board.Play("A5B4");
            SquareInfo b4 = board.GetSquareInfo("B4");

            Assert.AreEqual(Piece.Pawn, b4.Piece);
            Assert.AreEqual(PieceColor.Black, b4.PieceColor);
        }

        [Category("Knight Rule")]
        [Test]
        public void Play_KnightD5E7_KnightFoundAtE7AndD5RemainsEmpty()
        {
            Board board = Board.CreateBoard();
            board.PlacePiece(Piece.Knight, PieceColor.White, "D5");
            board.Play("D5E7");
            SquareInfo e7 = board.GetSquareInfo("E7");
            SquareInfo d5 = board.GetSquareInfo("D5");

            Assert.AreEqual(Piece.Knight, e7.Piece);
            Assert.AreEqual(PieceColor.White, e7.PieceColor);
            Assert.AreEqual(PieceColor.None, d5.PieceColor);
        }

        [Category("Knight Rule")]
        [Test]
        public void Play_KnightD5C7_KnightFoundAtC7AndD5RemainsEmpty()
        {
            Board board = Board.CreateBoard();
            board.PlacePiece(Piece.Knight, PieceColor.White, "D5");
            board.Play("D5C7");
            SquareInfo c7 = board.GetSquareInfo("C7");
            SquareInfo d5 = board.GetSquareInfo("D5");

            Assert.AreEqual(Piece.Knight, c7.Piece);
            Assert.AreEqual(PieceColor.White, c7.PieceColor);
            Assert.AreEqual(PieceColor.None, d5.PieceColor);
        } 

        [Category("Knight Rule")]
        [Test]
        public void Play_KnightD5B6_KnightFoundAtB6AndD5RemainsEmpty()
        {
            Board board = Board.CreateBoard();
            board.PlacePiece(Piece.Knight, PieceColor.White, "D5");
            board.Play("D5B6");
            SquareInfo b6 = board.GetSquareInfo("B6");
            SquareInfo d5 = board.GetSquareInfo("D5");

            Assert.AreEqual(Piece.Knight, b6.Piece);
            Assert.AreEqual(PieceColor.White, b6.PieceColor);
            Assert.AreEqual(PieceColor.None, d5.PieceColor);
        }

        [Category("Knight Rule")]
        [Test]
        public void Play_KnightD5B4_KnightFoundAtB4AndD5RemainsEmpty()
        {
            Board board = Board.CreateBoard();
            board.PlacePiece(Piece.Knight, PieceColor.White, "D5");
            board.Play("D5B4");
            SquareInfo b4 = board.GetSquareInfo("B4");
            SquareInfo d5 = board.GetSquareInfo("D5");

            Assert.AreEqual(Piece.Knight, b4.Piece);
            Assert.AreEqual(PieceColor.White, b4.PieceColor);
            Assert.AreEqual(PieceColor.None, d5.PieceColor);
        }

        [Category("Knight Rule")]
        [Test]
        public void Play_KnightD5C3_KnightFoundAtC3AndD5RemainsEmpty()
        {
            Board board = Board.CreateBoard();
            board.PlacePiece(Piece.Knight, PieceColor.White, "D5");
            board.Play("D5C3");
            SquareInfo c3 = board.GetSquareInfo("C3");
            SquareInfo d5 = board.GetSquareInfo("D5");

            Assert.AreEqual(Piece.Knight, c3.Piece);
            Assert.AreEqual(PieceColor.White, c3.PieceColor);
            Assert.AreEqual(PieceColor.None, d5.PieceColor);
        }

        [Category("Knight Rule")]
        [Test]
        public void Play_KnightD5E3_KnightFoundAtE3AndD5RemainsEmpty()
        {
            Board board = Board.CreateBoard();
            board.PlacePiece(Piece.Knight, PieceColor.White, "D5");
            board.Play("D5E3");
            SquareInfo e4 = board.GetSquareInfo("E3");
            SquareInfo d5 = board.GetSquareInfo("D5");

            Assert.AreEqual(Piece.Knight, e4.Piece);
            Assert.AreEqual(PieceColor.White, e4.PieceColor);
            Assert.AreEqual(PieceColor.None, d5.PieceColor);
        }

        [Category("Knight Rule")]
        [Test]
        public void Play_KnightD5F4_KnightFoundAtF4AndD5RemainsEmpty()
        {
            Board board = Board.CreateBoard();
            board.PlacePiece(Piece.Knight, PieceColor.White, "D5");
            board.Play("D5F4");
            SquareInfo f4 = board.GetSquareInfo("F4");
            SquareInfo d5 = board.GetSquareInfo("D5");

            Assert.AreEqual(Piece.Knight, f4.Piece);
            Assert.AreEqual(PieceColor.White, f4.PieceColor);
            Assert.AreEqual(PieceColor.None, d5.PieceColor);
        }

        [Category("Knight Rule")]
        [Test]
        public void Play_KnightD5F6_KnightFoundAtF6AndD5RemainsEmpty()
        {
            Board board = Board.CreateBoard();
            board.PlacePiece(Piece.Knight, PieceColor.White, "D5");
            board.Play("D5F6");
            SquareInfo f6 = board.GetSquareInfo("F6");
            SquareInfo d5 = board.GetSquareInfo("D5");

            Assert.AreEqual(Piece.Knight, f6.Piece);
            Assert.AreEqual(PieceColor.White, f6.PieceColor);
            Assert.AreEqual(PieceColor.None, d5.PieceColor);
        }
        
        [Category("Knight Rule")]
        [Test]
        public void Play_KnightD5D6_NotAllowedKnightRemainsAtD5()
        {
            Board board = Board.CreateBoard();
            board.PlacePiece(Piece.Knight, PieceColor.White, "D5");
            board.Play("D5D6");
            SquareInfo d6 = board.GetSquareInfo("D6");
            SquareInfo d5 = board.GetSquareInfo("D5");

            Assert.AreEqual(Piece.None, d6.Piece);
            Assert.AreEqual(PieceColor.White, d5.PieceColor);
            Assert.AreEqual(Piece.Knight, d5.Piece);
        }
    }
}

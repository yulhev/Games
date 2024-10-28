using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApp2;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestInitialize()
        {
            int size = 4;
            var playerSymbols = new List<char> { 'X', 'O' };
            var board = new Board(size, playerSymbols);
            board.Initialize();

            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    Assert.AreEqual(' ', board.GetGridValue(row, col));
                }
            }
        }

        [TestMethod]
        public void TestCanDropPiece()
        {
            int size = 4;
            var playerSymbols = new List<char> { 'X', 'O' };
            var board = new Board(size, playerSymbols);
            board.Initialize();

            Assert.IsTrue(board.CanDropPiece(0));
            board.DropPiece(0, 'X');
            Assert.IsTrue(board.CanDropPiece(0));
            board.DropPiece(0, 'O');
            board.DropPiece(0, 'X');
            board.DropPiece(0, 'O');
            Assert.IsFalse(board.CanDropPiece(0));
        }

        [TestMethod]
        public void TestDropPiece()
        {
            int size = 4;
            var playerSymbols = new List<char> { 'X', 'O' };
            var board = new Board(size, playerSymbols);
            board.Initialize();

            Assert.IsTrue(board.DropPiece(0, 'X'));
            Assert.AreEqual('X', board.GetGridValue(3, 0));
        }

        [TestMethod]
        public void TestRemovePiece()
        {
            int size = 4;
            var playerSymbols = new List<char> { 'X', 'O' };
            var board = new Board(size, playerSymbols);
            board.Initialize();

            board.DropPiece(0, 'X');
            board.RemovePiece(0);
            Assert.AreEqual(' ', board.GetGridValue(3, 0));
        }

        [TestMethod]
        public void TestIsFull()
        {
            int size = 4;
            var playerSymbols = new List<char> { 'X', 'O' };
            var board = new Board(size, playerSymbols);
            board.Initialize();

            for (int col = 0; col < size; col++)
            {
                for (int row = 0; row < size; row++)
                {
                    board.DropPiece(col, 'X');
                }
            }

            Assert.IsTrue(board.IsFull());
        }

        [TestMethod]
        public void TestCheckWin()
        {
            int size = 4;
            var playerSymbols = new List<char> { 'X', 'O' };
            var board = new Board(size, playerSymbols);
            board.Initialize();

            for (int i = 0; i < 4; i++)
            {
                board.DropPiece(i, 'X');
            }

            Assert.IsTrue(board.CheckWin('X', 4));
        }
    }

    [TestClass]
    public class AIPlayerTests
    {
        [TestMethod]
        public void TestChooseMove_BlockWin()
        {
            int size = 4;
            var playerSymbols = new List<char> { 'X', 'O' };
            var board = new Board(size, playerSymbols);
            board.Initialize();
            var aiPlayer = new AIPlayer("AI", 'O');

            for (int i = 0; i < 3; i++)
            {
                board.DropPiece(i, 'X');
            }

            int move = aiPlayer.ChooseMove(board, 4);
            Assert.AreEqual(3, move);
        }

        [TestMethod]
        public void TestChooseMove_WinMove()
        {
            int size = 4;
            var playerSymbols = new List<char> { 'X', 'O' };
            var board = new Board(size, playerSymbols);
            board.Initialize();
            var aiPlayer = new AIPlayer("AI", 'O');

            for (int i = 0; i < 3; i++)
            {
                board.DropPiece(i, 'O');
            }

            int move = aiPlayer.ChooseMove(board, 4);
            Assert.AreEqual(3, move);
        }

        [TestMethod]
        public void TestChooseMove_RandomMove()
        {
            int size = 4;
            var playerSymbols = new List<char> { 'X', 'O' };
            var board = new Board(size, playerSymbols);
            board.Initialize();
            var aiPlayer = new AIPlayer("AI", 'O');

            int move = aiPlayer.ChooseMove(board, 4);
            Assert.IsTrue(move >= 0 && move < size);
        }
    }
}
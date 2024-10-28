using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class AIPlayer : Player
    {
        public AIPlayer(string name, char symbol) : base(name, symbol)
        {
        }

        public int ChooseMove(Board board, int winCondition)
        {
            int size = board.Size;

            for (int col = 0; col < size; col++)
            {
                if (board.CanDropPiece(col))
                {
                    board.DropPiece(col, this.Symbol);
                    if (board.CheckWin(this.Symbol, winCondition))
                    {
                        board.RemovePiece(col);
                        return col;
                    }
                    board.RemovePiece(col);
                }
            }

            foreach (char opponentSymbol in board.PlayerSymbols)
            {
                if (opponentSymbol == this.Symbol) continue;

                for (int col = 0; col < size; col++)
                {
                    if (board.CanDropPiece(col))
                    {
                        board.DropPiece(col, opponentSymbol);
                        if (board.CheckWin(opponentSymbol, winCondition))
                        {
                            board.RemovePiece(col);
                            return col;
                        }
                        board.RemovePiece(col);
                    }
                }
            }

            List<int> validMoves = new List<int>();
            for (int col = 0; col < size; col++)
            {
                if (board.CanDropPiece(col))
                {
                    validMoves.Add(col);
                }
            }

            Random random = new Random();
            int randomMove = validMoves[random.Next(validMoves.Count)];
            return randomMove;
        }
    }
}


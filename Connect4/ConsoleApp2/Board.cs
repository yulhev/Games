using System.Drawing;

namespace ConsoleApp2
{
    public class Board
    {
        public int Size { get; }
        private char[,] grid;
        public List<char> PlayerSymbols { get; }

        public Board(int size, List<char> playerSymbols)
        {
            Size = size;
            PlayerSymbols = playerSymbols;
            grid = new char[size, size];
        }

        public void Initialize()
        {
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    grid[row, col] = ' ';
                }
            }
        }

        public bool CanDropPiece(int column)
        {
            return grid[0, column] == ' ';
        }

        public bool DropPiece(int column, char symbol)
        {
            for (int row = Size - 1; row >= 0; row--)
            {
                if (grid[row, column] == ' ')
                {
                    grid[row, column] = symbol;
                    return true;
                }
            }
            return false;
        }

        public void RemovePiece(int column)
        {
            for (int row = 0; row < Size; row++)
            {
                if (grid[row, column] != ' ')
                {
                    grid[row, column] = ' ';
                    break;
                }
            }
        }

        public bool IsFull()
        {
            for (int col = 0; col < Size; col++)
            {
                if (CanDropPiece(col))
                {
                    return false;
                }
            }
            return true;
        }

        public bool CheckWin(char symbol, int winCondition)
        {
            
            return CheckHorizontalWin(symbol, winCondition) ||
                   CheckVerticalWin(symbol, winCondition) ||
                   CheckDiagonalWin(symbol, winCondition);
        }

        private bool CheckHorizontalWin(char symbol, int winCondition)
        {
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col <= Size - winCondition; col++)
                {
                    bool win = true;
                    for (int k = 0; k < winCondition; k++)
                    {
                        if (grid[row, col + k] != symbol)
                        {
                            win = false;
                            break;
                        }
                    }
                    if (win) return true;
                }
            }
            return false;
        }

        private bool CheckVerticalWin(char symbol, int winCondition)
        {
            for (int col = 0; col < Size; col++)
            {
                for (int row = 0; row <= Size - winCondition; row++)
                {
                    bool win = true;
                    for (int k = 0; k < winCondition; k++)
                    {
                        if (grid[row + k, col] != symbol)
                        {
                            win = false;
                            break;
                        }
                    }
                    if (win) return true;
                }
            }
            return false;
        }

        private bool CheckDiagonalWin(char symbol, int winCondition)
        {
            
            for (int row = 0; row <= Size - winCondition; row++)
            {
                for (int col = 0; col <= Size - winCondition; col++)
                {
                    bool win = true;
                    for (int k = 0; k < winCondition; k++)
                    {
                        if (grid[row + k, col + k] != symbol)
                        {
                            win = false;
                            break;
                        }
                    }
                    if (win) return true;
                }
            }

            for (int row = winCondition - 1; row < Size; row++)
            {
                for (int col = 0; col <= Size - winCondition; col++)
                {
                    bool win = true;
                    for (int k = 0; k < winCondition; k++)
                    {
                        if (grid[row - k, col + k] != symbol)
                        {
                            win = false;
                            break;
                        }
                    }
                    if (win) return true;
                }
            }
            return false;
        }

        public void Display()
        {
            Console.Write("  ");  
            for (int i = 1; i <= Size; i++)
            {
                Console.Write(" " + i);
            }
            Console.WriteLine();

            for (int row = 0; row < Size; row++)
            {
                Console.Write("  ");

                for (int col = 0; col < Size; col++)
                {
                    Console.Write("|" + grid[row, col]);
                }
                Console.WriteLine("|");

                Console.Write("  ");
                for (int col = 0; col < Size; col++)
                {
                    Console.Write("--");
                }
                Console.WriteLine("-");
            }
        }
        public char GetGridValue(int row, int column)
        {
            return grid[row, column];
        }
    }
}

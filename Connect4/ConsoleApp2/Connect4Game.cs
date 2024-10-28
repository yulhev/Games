using System;
using System.Collections.Generic;
using System.Configuration;

namespace ConsoleApp2
{
    class Connect4Game
    {
        private int size;
        private int winCondition;

        private Board board;
        private List<Player> players;
        private Random random;

        public Connect4Game()
        {
            winCondition = GetWinConditionFromConfig();
            size = GetBoardSizeFromUser();
            List<char> playerSymbols = new List<char>();
            players = new List<Player>();
            random = new Random();

            InitializePlayers(playerSymbols);
            board = new Board(size, playerSymbols);
        }

        private int GetBoardSizeFromUser()
        {
            int size;
            Console.Write("Enter the size of the board (number of rows and columns): ");
            while (!int.TryParse(Console.ReadLine(), out size) || size <= 0)
            {
                Console.Write("Invalid input. Please enter a positive number: ");
            }
            return size;
        }

        private int GetWinConditionFromConfig()
        {
            int winCondition;
            if (!int.TryParse(ConfigurationManager.AppSettings["WinCondition"], out winCondition) || winCondition <= 0)
            {
                Console.WriteLine("Invalid or missing win condition in config. Using default win condition of 4.");
                winCondition = 4;
            }
            return winCondition;
        }

        public void Start()
        {
            board.Initialize();
            int currentPlayerIndex = random.Next(0, players.Count);
            bool gameWon = false;

            Console.WriteLine($"The dimensions of the board are {size}x{size}. \n{players[currentPlayerIndex].Name} starts the game!");

            while (!gameWon && !board.IsFull())
            {
                board.Display();
                Player currentPlayer = players[currentPlayerIndex];

                if (currentPlayer is AIPlayer ai)
                {
                    int column = ai.ChooseMove(board, winCondition);
                    Console.WriteLine($"{ai.Name} (AI) chooses column {column + 1}");
                    board.DropPiece(column, ai.Symbol);
                }
                else
                {
                    Console.WriteLine($"{currentPlayer.Name}'s turn ({currentPlayer.Symbol}).");
                    int column = InputHandler.GetColumnInput(size);
                    if (!board.DropPiece(column - 1, currentPlayer.Symbol)) 
                    {
                        continue;
                    }
                }

                gameWon = board.CheckWin(players[currentPlayerIndex].Symbol, winCondition);
                if (gameWon)
                {
                    board.Display();
                    Console.WriteLine($"{players[currentPlayerIndex].Name} wins!");
                }

                currentPlayerIndex = (currentPlayerIndex + 1) % players.Count; 
            }

            if (!gameWon)
            {
                board.Display();
                Console.WriteLine("It's a draw!");
            }
        }

        private void InitializePlayers(List<char> playerSymbols)
        {
            Console.Write("Do you want to play against AI or another player? (Enter 'AI' or 'Player'): ");
            string opponentType = Console.ReadLine();

            if (opponentType.Equals("AI", StringComparison.OrdinalIgnoreCase))
            {
                AddPlayer(1, playerSymbols);
                AddAIPlayer(playerSymbols);
            }
            else
            {
                Console.Write("Enter the number of players: ");
                int numPlayers;
                while (!int.TryParse(Console.ReadLine(), out numPlayers) || numPlayers < 2)
                {
                    Console.Write("Invalid input. Enter a number greater than 1: ");
                }

                for (int i = 0; i < numPlayers; i++)
                {
                    AddPlayer(i + 1, playerSymbols);
                }
            }
        }

        private void AddPlayer(int playerNumber, List<char> playerSymbols)
        {
            string name;
            char symbol;

            while (true)
            {
                Console.Write($"Enter name for Player {playerNumber}: ");
                name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Name cannot be empty. Please enter a valid name.");
                    continue;
                }

                symbol = char.ToUpper(name[0]);

                if (playerSymbols.Contains(symbol))
                {
                    Console.WriteLine($"The symbol '{symbol}' is already taken. Please choose a different name.");
                }
                else
                {
                    playerSymbols.Add(symbol);
                    break;
                }
            }

            players.Add(new Player(name, symbol));
        }

        private void AddAIPlayer(List<char> playerSymbols)
        {
            string name = "AI";
            char symbol = 'A'; 

            if (playerSymbols.Contains(symbol))
            {
                Console.WriteLine($"The symbol '{symbol}' is already taken. AI will use the next available letter.");
                symbol = (char)(symbol + 1);
            }

            playerSymbols.Add(symbol);
            players.Add(new AIPlayer(name, symbol));
        }
    }
}

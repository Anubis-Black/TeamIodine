namespace BalloonsPop
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public class Engine
    {
        private const int TotalRows = 5;
        private const int TotalColumns = 10;

        private readonly char[,] gameField = new char[TotalRows, TotalColumns];
        private readonly SortedDictionary<int, string> statistics = new SortedDictionary<int, string>();

        private int currentBalloons;
        private int userMovesCount = 0;
        private int clearedCellsCount = 0;
        private string userInput;

        public void Start()
        {
            Console.WriteLine("Welcome to “Balloons Pops” game. Please try to pop the balloons. Use 'top' to view the top scoreboard, 'restart' to start a new game and 'exit' to quit the game.");
            this.currentBalloons = TotalRows * TotalColumns;
            this.userMovesCount = 0;
            this.clearedCellsCount = 0;
            this.InitialiseGameField();
            this.PrintTable();
            this.GameLogic(this.userInput);
        }

        public void InitialiseGameField()
        {
            for (int row = 0; row < TotalRows; row++)
            {
                for (int column = 0; column < TotalColumns; column++)
                {
                    this.gameField[row, column] = (char)('0' + RandomNumberGenerator.Instance.Next(1, 5));
                }
            }
        }

        public void PrintTable()
        {
            Console.WriteLine("    0 1 2 3 4 5 6 7 8 9");
            Console.WriteLine("   ---------------------");

            for (int row = 0; row < TotalRows; row++)
            {
                Console.Write(row + " | ");

                for (int column = 0; column < TotalColumns; column++)
                {
                    Console.Write(this.gameField[row, column] + " ");
                }

                Console.Write("| ");
                Console.WriteLine();
            }

            Console.WriteLine("   ---------------------");
        }

        public void GameLogic(string userInput)
        {
            this.PlayGame();
            this.userMovesCount++;
            this.userInput = string.Empty;
            this.GameLogic(this.userInput);
        }

        private bool IsLegalMove(int row, int column)
        {
            bool isInsideRowBounds = row >= 0 && row < TotalRows;
            bool isInsideColumnBounds = column >= 0 && column < TotalColumns;

            bool isInsideGameFieldBounds = isInsideRowBounds && isInsideColumnBounds;

            bool isLegalMove = true;

            if (!isInsideGameFieldBounds)
            {
                isLegalMove = false;
            }
            else
            {
                isLegalMove = this.gameField[row, column] != '.';
            }

            return isLegalMove;
        }

        private void InvalidInput()
        {
            Console.WriteLine("Invalid move or command");
            this.userInput = string.Empty;
            this.GameLogic(this.userInput);
        }

        private void InvalidMove()
        {
            Console.WriteLine("Illegal move: cannot pop missing ballon!");
            this.userInput = string.Empty;
            this.GameLogic(this.userInput);
        }

        private void ShowStatistics()
        {
            this.PrintTheScoreBoard();
        }

        private void Exit()
        {
            Console.WriteLine("Good Bye");
            Thread.Sleep(1000);
            Console.WriteLine(this.userMovesCount);
            Console.WriteLine(this.currentBalloons);
            Environment.Exit(0);
        }

        private void Restart()
        {
            this.Start();
        }

        private void ReadTheIput()
        {
            if (!this.IsFinished())
            {
                Console.Write("Enter a row and column: ");
                this.userInput = Console.ReadLine();
            }
            else
            {
                Console.Write("opal;aaaaaaaa! You popped all baloons in " + this.userMovesCount + " moves."
                                 + "Please enter your name for the top scoreboard:");
                this.userInput = Console.ReadLine();
                this.statistics.Add(this.userMovesCount, this.userInput);
                this.PrintTheScoreBoard();
                this.userInput = string.Empty;
                this.Start();
            }
        }

        private void PrintTheScoreBoard()
        {
            int p = 0;

            Console.WriteLine("Scoreboard:");
            foreach (KeyValuePair<int, string> s in this.statistics)
            {
                if (p == 4)
                {
                    break;
                }
                else
                {
                    p++;
                    Console.WriteLine("{0}. {1} --> {2} moves", p, s.Value, s.Key);
                }
            }
        }

        private void PlayGame()
        {
            int i = -1;
            int j = -1;

        Play: 
            this.ReadTheIput();

            switch (this.userInput)
            {
                case "":
                    this.InvalidInput();
                    break;
                case "top":
                    this.ShowStatistics();
                    this.userInput = string.Empty;
                    goto Play;
                case "restart":
                    this.userInput = string.Empty; 
                    this.Restart();
                    break;
                case "exit":
                    this.Exit();
                    break;
                default:
                    // Unknown command exception
                    break;
            } 

            char activeCell;
            string modifiedUserInput = this.userInput.Replace(" ", string.Empty);
            try
            {
                i = int.Parse(modifiedUserInput) / 10;
                j = int.Parse(modifiedUserInput) % 10;
            }
            catch (Exception)
            {
                this.InvalidInput();
            }

            if (this.IsLegalMove(i, j))
            {
                activeCell = this.gameField[i, j];
                this.RemoveAllBaloons(i, j, activeCell);
            }
            else
            {
                this.InvalidMove();
            }
            
            this.ClearEmptyCells(); 
            this.PrintTable();
        }   

        private void RemoveAllBaloons(int i, int j, char activeCell)
        {
            if ((i >= 0) && (i <= 4) && (j <= 9) && (j >= 0) && (this.gameField[i, j] == activeCell))
            {
                this.gameField[i, j] = '.';
                this.clearedCellsCount++;

                // Up
                this.RemoveAllBaloons(i - 1, j, activeCell);

                // Down
                this.RemoveAllBaloons(i + 1, j, activeCell);

                // Left
                this.RemoveAllBaloons(i, j + 1, activeCell);

                // Right
                this.RemoveAllBaloons(i, j - 1, activeCell);
            }
            else
            {
                this.currentBalloons -= this.clearedCellsCount;
                this.clearedCellsCount = 0;
                return;
            }
        }

        private void ClearEmptyCells()
        {
            int i;
            int j;
            Queue<char> temp = new Queue<char>();
            for (j = TotalColumns - 1; j >= 0; j--)
            {
                for (i = TotalRows - 1; i >= 0; i--)
                {
                    if (this.gameField[i, j] != '.')
                    {
                        temp.Enqueue(this.gameField[i, j]);
                        this.gameField[i, j] = '.';
                    }
                }

                i = 4;
                while (temp.Count > 0)
                {
                    this.gameField[i, j] = temp.Dequeue();
                    i--;
                }

                temp.Clear();
            }
        }

        private bool IsFinished()
        {
            return this.currentBalloons == 0;
        }
    }
}

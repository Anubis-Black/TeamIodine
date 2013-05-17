namespace BalloonsPop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Engine
    {
        public const int TotalRows = 5;
        public const int TotalColumns = 10;

        private const int TopFive = 5;

        private readonly char[,] gameField = new char[TotalRows, TotalColumns];
        private readonly SortedDictionary<int, string> highScores = new SortedDictionary<int, string>();

        private IList<IRenderable> gameObjects;
        private IList<Balloon> balloons;

        private int userMovesCount = 0;

        public Engine(IRenderer renderer)
        {
            this.Renderer = renderer;
            this.gameObjects = new List<IRenderable>();
            this.balloons = new List<Balloon>();
        }

        public IRenderer Renderer { get; private set; }

        public bool ShouldEndGame { get; private set; }

        public void Start()
        {
            Console.WriteLine("Welcome to “Balloons Pops” game. Please try to pop the balloons. Use 'top' to view the top scoreboard, 'restart' to start a new game and 'exit' to quit the game.");
            this.userMovesCount = 0;

            this.InitialiseGameField();            

            this.Renderer.RenderObjects(this.gameObjects);

            this.ShouldEndGame = false;

            do
            {
                ICommand command = ParseUserInput();

                switch (command.Type)
                {
                    case CommandType.AttemptPop:
                        this.AttemptPop(command);
                        break;
                    case CommandType.Restart:
                        this.Restart();
                        break;
                    case CommandType.Top:
                        this.Top();
                        break;
                    case CommandType.Exit:
                        this.Exit();
                        break;
                    default:
                        throw new ArgumentException("Unknown command type.");
                }

                foreach (GameObject gameObject in this.gameObjects)
                {
                    if (gameObject.IsDestroyed == true)
                    {
                        this.AmendObjectsAbove(gameObject);
                    }
                }

                List<Balloon> balloons = (List<Balloon>)this.balloons;

                balloons.RemoveAll(balloon => balloon.IsDestroyed);

                this.balloons = balloons;

                List<GameObject> gameObjects = new List<GameObject>();

                foreach (GameObject gameObject in this.gameObjects)
                {
                    gameObjects.Add(gameObject);
                }

                IEnumerable<GameObject> remainingObjects = gameObjects.Where(gameObject => !gameObject.IsDestroyed);

                this.gameObjects = new List<IRenderable>();

                foreach (GameObject gameObject in remainingObjects)
                {
                    this.gameObjects.Add(gameObject);
                }

                this.Renderer.RenderObjects(this.gameObjects);
            }
            while (!this.ShouldEndGame);

            this.EnterHighScore();
        }
  
        private ICommand ParseUserInput()
        {
            Console.SetCursorPosition(0, 20);

            Console.Write("Enter a row and a column: ");

            string userInput = Console.ReadLine();

            ICommand command = new Command(userInput);
            return command;
        }

        private void AmendObjectsAbove(GameObject originObject)
        {
            Position originPosition = originObject.Position;

            for (int index = originPosition.Y - 1; index >= 0; index--)
            {
                Position position = new Position(originPosition.X, index);

                foreach (GameObject gameObject in this.gameObjects)
                {
                    if (gameObject.Position == position)
                    {
                        if (gameObject is Balloon)
                        {
                            Balloon balloon = gameObject as Balloon;

                            balloon.UpdatePosition(new Position(0, 2));
                        }
                    }
                }
            }
        }

        private void InitialiseGameField()
        {
            IFactory balloonFactory = new BalloonFactory();

            var balloons = balloonFactory.CreateObjects();

            foreach (Balloon balloon in balloons)
            {
                this.gameObjects.Add(balloon);
                this.balloons.Add(balloon);
            }

            IFactory immovableObjectFactory = new ImmovableObjectFactory();

            var immovableObjects = immovableObjectFactory.CreateObjects();

            foreach (ImmovableObject immovableObject in immovableObjects)
            {
                this.gameObjects.Add(immovableObject);
            }
        }
  
        private void AttemptPop(ICommand command)
        {
            if (command.Parameters.Length != 2)
            {
                this.InvalidCommand(command);
            }
            else
            {
                string firstParameter = command.Parameters[0];
                string secondParameter = command.Parameters[1];

                int row;
                int column;

                if (!int.TryParse(firstParameter, out row) || !int.TryParse(secondParameter, out column))
                {
                    this.InvalidCommand(command);
                }
                else if (!this.IsLegalMove(row, column))
                {
                    this.InvalidMove(row, column);
                }
                else
                {
                    row <<= 1;
                    column <<= 1;

                    Position popPosition = new Position(BalloonFactory.XOffset + column, BalloonFactory.YOffset + row);

                    try
                    {       
                        this.PopBalloon(popPosition);
                        this.userMovesCount++;
                    }
                    catch (InvalidOperationException exception)
                    {
                        Console.WriteLine(exception.Message);
                    }

                    this.IsFinished();
                }
            }
        }

        private void IsFinished()
        {
            if (this.balloons.Count == 0)
            {
                this.ShouldEndGame = true;
            }
        }

        private void InvalidMove(int row, int column)
        {
            Console.WriteLine("There is no balloon at ({0}, {1}).", row, column);
        }

        private void InvalidCommand(ICommand command)
        {
            Console.WriteLine("'{0}' is not a valid command.", command.OriginalForm);
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

            return isLegalMove;
        }

        private void Top()
        {
            Console.WriteLine("Scoreboard:");

            KeyValuePair<int, string>[] playerRankings = this.highScores.ToArray();

            for (int index = 0; index < TopFive; index++)
            {
                KeyValuePair<int, string> player = playerRankings[index];

                Console.WriteLine("{0}. {1} --> {2} moves", index, player.Value, player.Key);                
            }
        }

        private void Exit()
        {
            this.ShouldEndGame = true;
        }

        private void Restart()
        {
            this.Start();
        }

        private void EnterHighScore()
        {
            Console.Write("You popped all baloons in {0} moves. Please enter your name for the top scoreboard: ", this.userMovesCount);

            string userNickName = Console.ReadLine();

            this.highScores.Add(this.userMovesCount, userNickName);

            this.Top();

            this.Start();            
        }

        private void PopBalloon(Position popPosition)
        {
            bool balloonFound = false;
            char balloonVisualisation = '0';

            foreach (Balloon balloon in this.balloons)
            {
                if (balloon.Position == popPosition)
                {
                    balloonFound = true;
                    balloonVisualisation = balloon.Visualisation;
                    balloon.RespondToInteraction();
                    break;
                }
            }

            if (!balloonFound)
            {
                throw new InvalidOperationException("Balloon is already popped.");
            }
            else
            {
                // Left
                for (int index = popPosition.X - 1; index >= 0; index--)
                {
                    Position collateralPosition = new Position(index, popPosition.Y);

                    this.PopCollateral(collateralPosition, balloonVisualisation);
                }

                // Right
                for (int index = popPosition.X + 1; index < TotalColumns; index++)
                {
                    Position collateralPosition = new Position(index, popPosition.Y);

                    this.PopCollateral(collateralPosition, balloonVisualisation);
                }

                // Up
                for (int index = popPosition.Y - 1; index >= 0; index--)
                {
                    Position collateralPosition = new Position(popPosition.X, index);

                    this.PopCollateral(collateralPosition, balloonVisualisation);
                }

                // Down
                for (int index = popPosition.X + 1; index < TotalRows; index++)
                {
                    Position collateralPosition = new Position(popPosition.X, index);

                    this.PopCollateral(collateralPosition, balloonVisualisation);
                }
            }            
        }

        private void PopCollateral(Position popPosition, char balloonVisualisation)
        {
            foreach (Balloon balloon in this.balloons)
            {
                if (balloon.Position == popPosition)
                {
                    balloon.RespondToInteraction();
                    break;
                }
            }
        }
    }
}

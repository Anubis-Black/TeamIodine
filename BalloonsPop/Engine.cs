namespace BalloonsPop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Engine
    {
        private const int TopFive = 5;

        private readonly SortedDictionary<int, string> highScores = new SortedDictionary<int, string>();
        private readonly IList<IRenderable> gameObjects;

        private IList<Balloon> balloons;

        private int userMovesCount = 0;

        public Engine(IRenderer renderer)
        {
            this.Renderer = renderer;
            this.gameObjects = new List<IRenderable>();
            this.balloons = new List<Balloon>();
        }

        public IRenderer Renderer { get; private set; }

        public bool GameCompleted { get; private set; }

        public bool QuitRequested { get; set; }

        public void Start()
        {
            Console.WriteLine("Welcome to “Balloons Pops” game. Please try to pop the balloons. Use 'top' to view the top scoreboard, 'restart' to start a new game and 'exit' to quit the game.");
            Console.ReadLine();
            this.userMovesCount = 0;

            this.InitialiseGameObjects();            

            this.Renderer.RenderObjects(this.gameObjects);

            this.GameCompleted = false;
            this.QuitRequested = false;

            do
            {
                ICommand command = this.ParseUserInput();

                PerformCommand(command);

                if (!this.QuitRequested)
                {
                    this.Update();

                    this.IsFinished();                    
                }
            }
            while (!this.GameCompleted && !this.QuitRequested);

            if (this.balloons.Count == 0)
            {
                this.EnterHighScore();
            }            
        }

        private void InitialiseGameObjects()
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
  
        private ICommand ParseUserInput()
        {
            Console.SetCursorPosition(0, 20);

            Console.Write("Enter a row and a column: ");

            string userInput = Console.ReadLine();

            ICommand command = new Command(userInput);
            return command;
        }

        private void PerformCommand(ICommand command)
        {
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
                    row *= Constants.IterationStep;
                    column *= Constants.IterationStep;

                    Position popPosition = new Position(Constants.XOffset + column, Constants.YOffset + row);

                    try
                    {
                        this.PopBalloon(popPosition);
                        this.userMovesCount++;
                    }
                    catch (InvalidOperationException exception)
                    {
                        Console.WriteLine(exception.Message);
                    }
                }
            }
        }

        private void InvalidCommand(ICommand command)
        {
            Console.WriteLine("'{0}' is not a valid command.", command.OriginalForm);
            Console.ReadLine();
        }

        private bool IsLegalMove(int row, int column)
        {
            bool isInsideRowBounds = row >= 0 && row < Constants.TotalRows;
            bool isInsideColumnBounds = column >= 0 && column < Constants.TotalColumns;

            bool isLegalMove = isInsideRowBounds && isInsideColumnBounds;

            return isLegalMove;
        }

        private void InvalidMove(int row, int column)
        {
            Console.WriteLine("There is no balloon at ({0}, {1}).", row, column);
            Console.ReadLine();
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
                for (int index = popPosition.X - Constants.IterationStep; index >= Constants.XOffset; index -= Constants.IterationStep)
                {
                    Position collateralPosition = new Position(index, popPosition.Y);

                    if (!this.CanPopCollateral(collateralPosition, balloonVisualisation))
                    {
                        break;
                    }
                }

                // Right
                for (int index = popPosition.X + Constants.IterationStep; index < Constants.XOffset + Constants.MaximalXPosition; index += Constants.IterationStep)
                {
                    Position collateralPosition = new Position(index, popPosition.Y);

                    if (!this.CanPopCollateral(collateralPosition, balloonVisualisation))
                    {
                        break;
                    }
                }

                // Up
                for (int index = popPosition.Y - Constants.IterationStep; index >= Constants.YOffset; index -= Constants.IterationStep)
                {
                    Position collateralPosition = new Position(popPosition.X, index);

                    if (!this.CanPopCollateral(collateralPosition, balloonVisualisation))
                    {
                        break;
                    }
                }

                // Down
                for (int index = popPosition.Y + Constants.IterationStep; index < Constants.YOffset + Constants.MaximalYPosition; index += Constants.IterationStep)
                {
                    Position collateralPosition = new Position(popPosition.X, index);

                    if (!this.CanPopCollateral(collateralPosition, balloonVisualisation))
                    {
                        break;
                    }
                }
            }
        }

        private bool CanPopCollateral(Position collateralPosition, char balloonVisualisation)
        {
            bool canPopCollateral = false;

            Balloon collateralBalloon = null;

            foreach (Balloon balloon in this.balloons)
            {
                if (balloon.Position == collateralPosition)
                {
                    collateralBalloon = balloon;
                    break;
                }
            }

            if (collateralBalloon != null && collateralBalloon.Visualisation == balloonVisualisation)
            {
                canPopCollateral = true;
                collateralBalloon.RespondToInteraction();
            }

            return canPopCollateral;
        }

        private void Restart()
        {
            this.Start();
        }

        private void Top()
        {
            Console.WriteLine("Scoreboard:");

            KeyValuePair<int, string>[] playerRankings = this.highScores.ToArray();

            int endIndex = Math.Min(playerRankings.Length, TopFive);

            for (int index = 0; index < endIndex; index++)
            {
                KeyValuePair<int, string> player = playerRankings[index];

                Console.WriteLine("{0}. {1} --> {2} moves", index + 1, player.Value, player.Key);
            }

            Console.ReadLine();
        }

        private void Exit()
        {
            Console.SetCursorPosition(0, 20);

            Console.WriteLine("Thank you for playing Balloons Pop! Have a great day!");

            this.QuitRequested = true;
        }

        private void Update()
        {
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

            var remainingObjects = gameObjects.Where(gameObject => !gameObject.IsDestroyed);

            this.gameObjects.Clear();

            foreach (GameObject gameObject in remainingObjects)
            {
                this.gameObjects.Add(gameObject);
            }

            this.Renderer.RenderObjects(this.gameObjects);
        }

        private void AmendObjectsAbove(GameObject originObject)
        {
            Position originPosition = originObject.Position;

            for (int index = originPosition.Y - Constants.IterationStep; index >= 0; index -= Constants.IterationStep)
            {
                Position position = new Position(originPosition.X, index);

                foreach (GameObject gameObject in this.gameObjects)
                {
                    if (gameObject.Position == position && gameObject is Balloon)
                    {
                        Balloon balloon = gameObject as Balloon;

                        Position vectorChange = new Position(0, Constants.IterationStep);

                        balloon.UpdatePosition(vectorChange);                        
                    }
                }
            }
        }

        private void IsFinished()
        {
            if (this.balloons.Count == 0)
            {
                this.GameCompleted = true;
            }
        }

        private void EnterHighScore()
        {
            Console.SetCursorPosition(0, 20);

            Console.Write("You popped all baloons in {0} moves. Please enter your name for the top scoreboard: ", this.userMovesCount);

            string userNickName = Console.ReadLine();

            this.highScores.Add(this.userMovesCount, userNickName);

            this.Top();

            this.Start();            
        }
    }
}

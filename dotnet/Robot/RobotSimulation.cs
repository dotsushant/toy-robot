using System;
using System.Collections.Generic;

namespace Robot
{
    public class RobotSimulation
    {
        private IRobot _robot;
        private ITerrain _terrain;

        public void Simulate()
        {
            DisplayOptions();

            while (true)
            {
                var keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.P:
                        Console.Write($"{Resource.PlaceCommand} ");

                        var x = PromptForNumber();
                        Console.Write($"{x},");

                        var y = PromptForNumber();
                        Console.Write($"{y},");

                        var direction = PromptForDirection();
                        Console.Write($"{direction}");

                        Console.WriteLine();

                        _robot.Place(_terrain, new Location(x, y), direction);
                        break;

                    case ConsoleKey.M:
                        Console.WriteLine(Resource.MoveCommand);
                        _robot.Move();
                        break;

                    case ConsoleKey.L:
                        Console.WriteLine(Resource.TurnLeftCommand);
                        _robot.TurnLeft();
                        break;

                    case ConsoleKey.R:
                        Console.WriteLine(Resource.TurnRightCommand);
                        _robot.TurnRight();
                        break;

                    case ConsoleKey.S:
                        _robot.ReportStatus(orientation =>
                        {
                            Console.WriteLine($"{Resource.ReportStatusCommand}");
                            Console.WriteLine($"Output: {orientation.Location},{orientation.Direction}");
                        });
                        break;

                    case ConsoleKey.C:
                        DisplayOptions();
                        break;

                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;
                }
            }
        }

        public RobotSimulation AddRobot()
        {
            _robot = new Robot();
            _robot.SetMotor(new Motor(Direction.NORTH));
            _robot.AddSkill<SingleStepSkill>();
            _robot.AddSkill<MultiStepSkill>();
            _robot.AddSkill<LeftTurningSkill>();
            _robot.AddSkill<RightTurningSkill>();
            return this;
        }

        public RobotSimulation BuildTerrain(int rows, int columns)
        {
            _terrain = new Terrain(rows, columns);
            return this;
        }

        private void DisplayOptions()
        {
            Console.Clear();
            Draw('*');
            Console.WriteLine(Resource.WelcomeMessage);
            Draw('*');
            Console.WriteLine($"{Resource.PlaceCommandShortcut}: {Resource.PlaceCommand}");
            Console.WriteLine($"{Resource.MoveCommandShortcut}: {Resource.MoveCommand}");
            Console.WriteLine($"{Resource.TurnLeftCommandShortcut}: {Resource.TurnLeftCommand}");
            Console.WriteLine($"{Resource.TurnRightCommandShorcut}: {Resource.TurnRightCommand}");
            Console.WriteLine($"{Resource.ReportStatusCommandShortcut}: {Resource.ReportStatusCommand}");
            Console.WriteLine($"{Resource.ClearConsoleCommandShortcut}: {Resource.ClearConsoleCommand}");
            Console.WriteLine($"{Resource.ExitMessage}");
            Draw('*');
        }

        private int PromptForNumber()
        {
            var value = 0;
            var shouldBreak = false;

            while (!shouldBreak)
            {
                var keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }

                if (char.IsDigit(keyInfo.KeyChar))
                {
                    value = int.Parse(keyInfo.KeyChar.ToString());
                    shouldBreak = true;
                }
                else
                {
                    shouldBreak = false;
                }
            }

            return value;
        }

        private Direction PromptForDirection()
        {
            var value = "";
            var shouldBreak = false;
            var keyMap = new Dictionary<string, Direction>();

            keyMap.Add("N", Direction.NORTH);
            keyMap.Add("E", Direction.EAST);
            keyMap.Add("S", Direction.SOUTH);
            keyMap.Add("W", Direction.WEST);

            while (!shouldBreak)
            {
                var keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }

                if (char.IsLetter(keyInfo.KeyChar))
                {
                    value = keyInfo.KeyChar.ToString().ToUpper();
                    shouldBreak = keyMap.ContainsKey(value);
                }
                else
                {
                    shouldBreak = false;
                }
            }

            return keyMap[value];
        }

        private void Draw(char character)
        {
            Console.WriteLine(new string(character, 40));
        }
    }
}
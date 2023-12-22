using Xunit;

namespace Robot.Tests
{
    public class RobotTest
    {
        private readonly IRobot _robot;

        public RobotTest()
        {
            _robot = new Robot();
            _robot.SetMotor(new Motor());
            _robot.AddSkill<SingleStepSkill>();
            _robot.AddSkill<MultiStepSkill>();
            _robot.AddSkill<LeftTurningSkill>();
            _robot.AddSkill<RightTurningSkill>();
        }

        [Fact]
        public void Robot_Should_NotBeOriented_IfMotorIsNotAttached()
        {
            var terrain = new Terrain(5, 5);
            var desiredLocation = new Location(1, 2);
            var desiredDirection = Direction.SOUTH;

            _robot.SetMotor(null); // Remove the motor
            _robot.Place(terrain, desiredLocation, desiredDirection);

            Assert.Null(_robot.Orientation);
        }

        [Fact]
        public void Robot_Should_NotBeOriented_IfNotPlaced()
        {
            Assert.Null(_robot.Orientation);
        }

        [Fact]
        public void Robot_Should_NotBeOriented_IfFailedToBePlaced()
        {
            var terrain = new Terrain(5, 5);
            var location = new Location(7, 9); // Out of bound

            _robot.Place(terrain, location, Direction.SOUTH);

            Assert.Null(_robot.Orientation);
        }

        [Fact]
        public void Robot_Should_BeOriented_IfPlaced()
        {
            var terrain = new Terrain(5, 5);
            var desiredLocation = new Location(1, 2);
            var desiredDirection = Direction.SOUTH;

            _robot.Place(terrain, desiredLocation, desiredDirection);

            Assert.NotNull(_robot.Orientation);
            Assert.NotNull(_robot.Orientation.Location);
            Assert.Equal(desiredDirection, _robot.Orientation.Direction);
            Assert.Equal(desiredLocation.X, _robot.Orientation.Location.X);
            Assert.Equal(desiredLocation.Y, _robot.Orientation.Location.Y);
        }

        [Fact]
        public void Robot_Should_IgnoreMoveCommand_IfNotPlaced()
        {
            _robot.Move();

            Assert.Null(_robot.Orientation);
        }

        [Fact]
        public void Robot_Should_AcceptMoveCommand_IfPlaced()
        {
            var terrain = new Terrain(5, 5);
            var desiredLocation = new Location(1, 2);
            var desiredDirection = Direction.SOUTH;

            _robot.Place(terrain, desiredLocation, desiredDirection);
            _robot.Move();

            Assert.NotNull(_robot.Orientation);
            Assert.NotNull(_robot.Orientation.Location);
            Assert.Equal(desiredDirection, _robot.Orientation.Direction);
            Assert.Equal(desiredLocation.X, _robot.Orientation.Location.X);
            Assert.Equal(1, _robot.Orientation.Location.Y);
        }

        [Fact]
        public void Robot_Should_IgnoreMoveCommand_IfNextLocationIsNotWithinBounds()
        {
            var terrain = new Terrain(5, 5);
            var desiredLocation = new Location(1, 2);
            var desiredDirection = Direction.SOUTH;

            _robot.Place(terrain, desiredLocation, desiredDirection);

            _robot.Move(); // (1, 1)
            _robot.Move(); // (1, 0)
            _robot.Move(); // Will still be (1, 0)

            Assert.NotNull(_robot.Orientation);
            Assert.NotNull(_robot.Orientation.Location);
            Assert.Equal(desiredDirection, _robot.Orientation.Direction);
            Assert.Equal(desiredLocation.X, _robot.Orientation.Location.X);
            Assert.Equal(0, _robot.Orientation.Location.Y);
        }

        [Fact]
        public void Robot_Should_IgnoreLeftTurnCommand_IfNotPlaced()
        {
            _robot.TurnLeft();

            Assert.Null(_robot.Orientation);
        }

        [Fact]
        public void Robot_Should_AcceptLeftTurnCommand_IfPlaced()
        {
            var terrain = new Terrain(5, 5);
            var desiredLocation = new Location(1, 2);
            var desiredDirection = Direction.SOUTH;

            _robot.Place(terrain, desiredLocation, desiredDirection);
            _robot.TurnLeft();

            Assert.NotNull(_robot.Orientation);
            Assert.NotNull(_robot.Orientation.Location);
            Assert.Equal(Direction.EAST, _robot.Orientation.Direction);
            Assert.Equal(desiredLocation.X, _robot.Orientation.Location.X);
            Assert.Equal(desiredLocation.Y, _robot.Orientation.Location.Y);
        }

        [Fact]
        public void Robot_Should_IgnoreRightTurnCommand_IfNotPlaced()
        {
            _robot.TurnRight();

            Assert.Null(_robot.Orientation);
        }

        [Fact]
        public void Robot_Should_AcceptRightTurnCommand_IfPlaced()
        {
            var terrain = new Terrain(5, 5);
            var desiredLocation = new Location(1, 2);
            var desiredDirection = Direction.SOUTH;

            _robot.Place(terrain, desiredLocation, desiredDirection);
            _robot.TurnRight();

            Assert.NotNull(_robot.Orientation);
            Assert.NotNull(_robot.Orientation.Location);
            Assert.Equal(Direction.WEST, _robot.Orientation.Direction);
            Assert.Equal(desiredLocation.X, _robot.Orientation.Location.X);
            Assert.Equal(desiredLocation.Y, _robot.Orientation.Location.Y);
        }
    }
}
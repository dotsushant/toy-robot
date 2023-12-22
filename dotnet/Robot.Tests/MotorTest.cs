using Xunit;

namespace Robot.Tests
{
    public class MotorTest
    {
        [Theory]
        [MemberData(nameof(MotorTestData.AllDirections), MemberType = typeof(MotorTestData))]
        public void Motor_Should_PointInRightDirection_When_Initialized(Direction direction)
        {
            var motor = new Motor(direction);
            Assert.Equal(direction, motor.Direction);
        }

        [Theory]
        [MemberData(nameof(MotorTestData.LeftTurnCombination), MemberType = typeof(MotorTestData))]
        public void Motor_Should_PointInRightDirection_When_TurnedLeft(Direction initialDirection, Direction finalDirection)
        {
            var motor = new Motor(initialDirection);
            motor.TurnLeft();
            Assert.Equal(finalDirection, motor.Direction);
        }

        [Theory]
        [MemberData(nameof(MotorTestData.RightTurnCombination), MemberType = typeof(MotorTestData))]
        public void Motor_Should_PointInRightDirection_When_TurnedRight(Direction initialDirection, Direction finalDirection)
        {
            var motor = new Motor(initialDirection);
            motor.TurnRight();
            Assert.Equal(finalDirection, motor.Direction);
        }

        [Theory]
        [MemberData(nameof(MotorTestData.CustomTurnCombination), MemberType = typeof(MotorTestData))]
        public void Motor_Should_PointInRightDirection_When_TurnedToACustomDirection(Direction initialDirection, Direction finalDirection)
        {
            var motor = new Motor(initialDirection);
            motor.Turn(finalDirection);
            Assert.Equal(finalDirection, motor.Direction);
        }
    }
}
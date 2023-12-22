using System.Collections.Generic;

namespace Robot.Tests
{
    public class MotorTestData
    {
        public static IEnumerable<object[]> AllDirections
        {
            get
            {
                yield return new object[] { Direction.EAST };
                yield return new object[] { Direction.WEST };
                yield return new object[] { Direction.NORTH };
                yield return new object[] { Direction.SOUTH };
            }
        }

        public static IEnumerable<object[]> LeftTurnCombination
        {
            get
            {
                yield return new object[] { Direction.NORTH, Direction.WEST };
                yield return new object[] { Direction.WEST, Direction.SOUTH };
                yield return new object[] { Direction.SOUTH, Direction.EAST };
                yield return new object[] { Direction.EAST, Direction.NORTH };
            }
        }

        public static IEnumerable<object[]> RightTurnCombination
        {
            get
            {
                yield return new object[] { Direction.NORTH, Direction.EAST };
                yield return new object[] { Direction.EAST, Direction.SOUTH };
                yield return new object[] { Direction.SOUTH, Direction.WEST };
                yield return new object[] { Direction.WEST, Direction.NORTH };
            }
        }

        public static IEnumerable<object[]> CustomTurnCombination
        {
            get
            {
                yield return new object[] { Direction.NORTH, Direction.NORTH };
                yield return new object[] { Direction.NORTH, Direction.EAST };
                yield return new object[] { Direction.NORTH, Direction.SOUTH };
                yield return new object[] { Direction.NORTH, Direction.WEST };
                yield return new object[] { Direction.EAST, Direction.NORTH };
                yield return new object[] { Direction.EAST, Direction.EAST };
                yield return new object[] { Direction.EAST, Direction.SOUTH };
                yield return new object[] { Direction.EAST, Direction.WEST };
                yield return new object[] { Direction.SOUTH, Direction.NORTH };
                yield return new object[] { Direction.SOUTH, Direction.EAST };
                yield return new object[] { Direction.SOUTH, Direction.SOUTH };
                yield return new object[] { Direction.SOUTH, Direction.WEST };
                yield return new object[] { Direction.WEST, Direction.NORTH };
                yield return new object[] { Direction.WEST, Direction.EAST };
                yield return new object[] { Direction.WEST, Direction.SOUTH };
                yield return new object[] { Direction.WEST, Direction.WEST };
            }
        }
    }
}
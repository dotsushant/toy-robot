using System;

namespace Robot
{
    public class SingleStepSkill : IRobotSkill
    {
        public void Apply(Action<Orientation> orientationPredicate, params IParameter[] parameters)
        {
            var coreParameter = parameters?.Get<CoreParameter>();

            var motor = coreParameter?.Motor;
            var currentOrientation = coreParameter?.CurrentOrientation;

            if (motor != null && currentOrientation.IsValid())
            {
                var nextLocation = currentOrientation.Location.PeekNext(currentOrientation.Direction);

                if (currentOrientation.Terrain.Contains(nextLocation))
                {
                    orientationPredicate(new Orientation(currentOrientation.Terrain, nextLocation, currentOrientation.Direction));
                }
            }
        }
    }

    public class MultiStepSkill : IRobotSkill
    {
        public void Apply(Action<Orientation> orientationPredicate, params IParameter[] parameters)
        {
            var coreParameter = parameters?.Get<CoreParameter>();
            var placementParameter = parameters?.Get<PlacementParameter>();

            var currentOrientation = coreParameter?.CurrentOrientation;
            var desiredOrientation = placementParameter?.DesiredOrientation;

            var motor = coreParameter?.Motor;
            var terrain = (currentOrientation.IsValid()) ? currentOrientation?.Terrain : desiredOrientation?.Terrain;

            var nextLocation = default(Location);

            // must meet criteria before we proceed further
            if (motor != null && terrain != null && desiredOrientation.IsValid())
            {
                if (!currentOrientation.IsValid())
                {
                    nextLocation = Move(motor, terrain, new Location(0, 0), desiredOrientation.Location);
                }
                else
                {
                    nextLocation = Move(motor, terrain, currentOrientation.Location, desiredOrientation.Location);
                }

                motor.Turn(desiredOrientation.Direction); // finally turn the motor in the desired direction
                orientationPredicate(new Orientation(terrain, nextLocation, motor.Direction));
            }
        }

        /// <summary>
        /// Moves the robot from one location to the next
        /// </summary>
        /// <param name="motor">Motor used for navigation</param>
        /// <param name="terrain">Terrain on which the robot has to move</param>
        /// <param name="currentLocation">Current location of the robot</param>
        /// <param name="desiredLocation">Desired location of the robot</param>
        /// <returns></returns>
        private Location Move(IMotor motor, ITerrain terrain, Location currentLocation, Location desiredLocation)
        {
            while (true)
            {
                var activeDirection = default(Direction);
                var verticalDistance = desiredLocation.Y - currentLocation.Y;
                var horizontalDistance = desiredLocation.X - currentLocation.X;

                if (horizontalDistance == 0 && verticalDistance == 0)
                {
                    break;
                }

                if (verticalDistance != 0)
                {
                    activeDirection = verticalDistance < 0 ? Direction.SOUTH : Direction.NORTH;
                }
                else
                {
                    activeDirection = horizontalDistance < 0 ? Direction.WEST : Direction.EAST;
                }

                motor.Turn(activeDirection);
                var nextLocation = currentLocation.PeekNext(activeDirection);

                if (terrain.Contains(nextLocation))
                {
                    currentLocation = nextLocation;
                }
            }

            return currentLocation;
        }
    }

    public class LeftTurningSkill : IRobotSkill
    {
        public void Apply(Action<Orientation> orientationPredicate, params IParameter[] parameters)
        {
            var coreParameter = parameters?.Get<CoreParameter>();

            var motor = coreParameter?.Motor;
            var currentOrientation = coreParameter?.CurrentOrientation;

            if (motor != null && currentOrientation.IsValid())
            {
                motor.TurnLeft();
                orientationPredicate(new Orientation(currentOrientation.Terrain, currentOrientation.Location, motor.Direction));
            }
        }
    }

    public class RightTurningSkill : IRobotSkill
    {
        public void Apply(Action<Orientation> predicate, params IParameter[] parameters)
        {
            var coreParameter = parameters?.Get<CoreParameter>();

            var motor = coreParameter?.Motor;
            var currentOrientation = coreParameter?.CurrentOrientation;

            if (motor != null && currentOrientation.IsValid())
            {
                motor.TurnRight();
                predicate(new Orientation(currentOrientation.Terrain, currentOrientation.Location, motor.Direction));
            }
        }
    }
}
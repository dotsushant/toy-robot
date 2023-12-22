using System;
using System.Linq;

namespace Robot
{
    public static class RobotExtensions
    {
        /// <summary>
        /// Utility function to place the robot in a terrain at specific location and direction
        /// </summary>
        /// <param name="robot">Robot under consideration</param>
        /// <param name="terrain">Robot terrain</param>
        /// <param name="location">Location where to place the robot</param>
        /// <param name="direction">Direction which the robot should face to</param>
        public static void Place(this IRobot robot, ITerrain terrain, Location location, Direction direction)
        {
            robot.ApplySkill<MultiStepSkill>(new PlacementParameter(new Orientation(terrain, location, direction)));
        }

        /// <summary>
        /// Move the robot one unit at a time
        /// </summary>
        /// <remarks>If the location is invalid, the command is ignored</remarks>
        /// <param name="robot">Robot under consideration</param>
        public static void Move(this IRobot robot)
        {
            robot.ApplySkill<SingleStepSkill>();
        }

        /// <summary>
        /// Turns the robot left by 90 degrees
        /// </summary>
        /// <param name="robot">Robot under consideration</param>
        public static void TurnLeft(this IRobot robot)
        {
            robot.ApplySkill<LeftTurningSkill>();
        }

        /// <summary>
        /// Turns the robot right by 90 degrees
        /// </summary>
        /// <param name="robot">Robot under consideration</param>
        public static void TurnRight(this IRobot robot)
        {
            robot.ApplySkill<RightTurningSkill>();
        }

        /// <summary>
        /// Reports the whereabouts of the robot
        /// </summary>
        /// <param name="robot">Robot under consideration</param>
        /// <param name="orientationPredicate">Callback to report the whereabouts of the robot</param>
        public static void ReportStatus(this IRobot robot, Action<Orientation> orientationPredicate)
        {
            if (robot.Orientation.IsValid())
            {
                orientationPredicate?.Invoke(robot.Orientation);
            }
        }
    }

    public static class LocationExtensions
    {
        /// <summary>
        /// Gets the next logical location if moving ahead in a given direction
        /// </summary>
        /// <param name="direction">Direction in which to peek for the next logical location</param>
        /// <returns>Next logical location</returns>
        public static Location PeekNext(this Location location, Direction direction)
        {
            if (direction == Direction.NORTH || direction == Direction.SOUTH)
            {
                return new Location(location.X, location.Y + (direction == Direction.NORTH ? 1 : -1));
            }
            else
            {
                return new Location(location.X + (direction == Direction.EAST ? 1 : -1), location.Y);
            }
        }
    }

    public static class OrientationExtensions
    {
        public static bool IsValid(this Orientation orientation)
        {
            return orientation?.Terrain != null && orientation?.Location != null && orientation.Terrain.Contains(orientation.Location);
        }
    }

    public static class ParameterExtensions
    {
        /// <summary>
        /// Checks if the parameter is a part of parameter list
        /// </summary>
        /// <param name="parameters">Parameter list</param>
        /// <returns>True if parameter exists</returns>
        public static bool Has<T>(this IParameter[] parameters) where T : IParameter
        {
            return parameters?.FirstOrDefault(p => p.GetType() == typeof(T)) != null;
        }

        /// <summary>
        /// Gets the parameter that is a part of parameter list
        /// </summary>
        /// <param name="parameters">Parameter list</param>
        /// <returns>Parameter type requested</returns>
        public static T Get<T>(this IParameter[] parameters) where T : IParameter
        {
            var parameter = parameters?.FirstOrDefault(p => p.GetType() == typeof(T));

            if(parameter is T)
            {
                return (T)parameter;
            }

            return default(T);
        }
    }
}
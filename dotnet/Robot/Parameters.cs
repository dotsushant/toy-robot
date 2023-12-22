namespace Robot
{
    /// <summary>
    /// Core parameter required for exercisiing skill
    /// </summary>
    public class CoreParameter : IParameter
    {
        public CoreParameter(IMotor motor, Orientation currentOrientation)
        {
            Motor = motor;
            CurrentOrientation = currentOrientation;
        }

        /// <summary>
        /// Attached motor
        /// </summary>
        public IMotor Motor { get; }

        /// <summary>
        /// Current orientation
        /// </summary>
        public Orientation CurrentOrientation { get; }
    }

    /// <summary>
    /// Placement parameter required while placing the robot in a terrain
    /// </summary>
    public class PlacementParameter : IParameter
    {
        public PlacementParameter(Orientation orientation)
        {
            DesiredOrientation = orientation;
        }

        /// <summary>
        /// Desired orientation of the robot within the terrain
        /// </summary>
        public Orientation DesiredOrientation { get; }
    }
}
namespace Robot
{
    /// <summary>
    /// Constraint put on a location
    /// </summary>
    public class LocationConstraint
    {
        private readonly Location _lowerBound;
        private readonly Location _upperBound;

        public LocationConstraint(Location lowerBound, Location upperBound)
        {
            _lowerBound = lowerBound;
            _upperBound = upperBound;
        }

        /// <summary>
        /// Indicates if the given location is within the bounds
        /// </summary>
        /// <param name="location">Location to be checked</param>
        /// <returns>True if the location is within the bounds</returns>
        public bool Contains(Location location)
        {
            var lower = _lowerBound.Y <= location.Y && _lowerBound.X <= location.X;
            var upper = _upperBound.Y > location.Y && _upperBound.X > location.X;
            return lower && upper;
        }

        public override string ToString()
        {
            return $"({_lowerBound})-(${_upperBound})";
        }
    }
}
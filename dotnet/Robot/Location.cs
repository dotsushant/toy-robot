namespace Robot
{
    /// <summary>
    /// Position in a 2D space
    /// </summary>
    public class Location
    {
        public Location(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Horizontal displacement wrt (0,0)
        /// </summary>
        public int X { get; }

        /// <summary>
        /// Vertical displacement wrt (0,0)
        /// </summary>
        public int Y { get; }

        public override string ToString()
        {
            return $"{X},{Y}";
        }
    }
}
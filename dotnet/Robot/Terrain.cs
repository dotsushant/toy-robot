using System;

namespace Robot
{
    public class Terrain : ITerrain
    {
        private readonly LocationConstraint _locationConstraint;

        public Terrain(int rows, int columns)
        {
            _locationConstraint = new LocationConstraint(new Location(0, 0), new Location(Math.Abs(rows), Math.Abs(columns)));
        }

        public bool Contains(Location location)
        {
            return _locationConstraint.Contains(location);
        }
    }
}
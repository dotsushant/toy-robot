namespace Robot
{
    public class Orientation
    {
        public Orientation(ITerrain terrain, Location location, Direction direction)
        {
            Terrain = terrain;
            Location = location;
            Direction = direction;
        }

        public ITerrain Terrain { get; }
        public Location Location { get; }
        public Direction Direction { get; }

        public override string ToString()
        {
            return $"{Location},{Direction}";
        }
    }
}